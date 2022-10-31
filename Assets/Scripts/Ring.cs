using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private Transform player;
    public GameObject[] childRings;

    float radius = 100f;
    float force = 500f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
       
        if(transform.position.y > player.position.y + 0.1f)
        {
            FindObjectOfType<AudioManager>().Play("woosh");
            GameManager.numberOfPassedRings++;
            GameManager.score++;
            for (int i = 0; i < childRings.Length; i++)
            {
               childRings[i].GetComponent<Rigidbody>().isKinematic = false;
               childRings[i].GetComponent<Rigidbody>().useGravity = true;

                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
               
                foreach(Collider newCollider in colliders)
                {
                    Rigidbody rb = newCollider.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(force, transform.position, radius);
                    }
                }
                childRings[i].transform.parent = null;
                Destroy(childRings[i].gameObject, 2f);
                Destroy(this.gameObject, 5f);
            }
            this.enabled = false;
               
            Destroy(gameObject);
        }
    }
}
