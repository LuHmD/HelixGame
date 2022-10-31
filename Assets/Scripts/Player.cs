using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Rigidbody playerRb;
    public float bounceForce = 6;

    private AudioManager audioManager;
    //public GameObject splitPrefeb;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    //call when player hits collider
    private void OnCollisionEnter(Collision collision)
    {
        audioManager.Play("bounce");
        playerRb.velocity = new Vector3(playerRb.velocity.x, bounceForce, playerRb.velocity.z);
        string materialName = collision.transform.GetComponent<MeshRenderer>().material.name;

         if (materialName == "Safe (Instance)")
         {
             //The ball hits the safe area

         }else if (materialName == "Unsafe (Instance)")
         {
             //The ball hits the unsafe area
             GameManager.gameOver = true;
             audioManager.Play("game over");

         }else if(materialName == "LastRing (Instance)" && !GameManager.levelCompleted)
         {

             //Completed the Level
             GameManager.levelCompleted = true;
             audioManager.Play("win level");
         }

        //GameObject split = Instantiate(splitPrefeb, new Vector3(transform.position.x, collision.transform.position.y, transform.position.z), transform.rotation);
     }
    
}
