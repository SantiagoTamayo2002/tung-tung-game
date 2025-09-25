using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_grito : MonoBehaviour
{
    
    public AudioSource grito;
    void Start()
    {
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            grito.Play();
            Destroy(this);
        }
    }
        
    
}
