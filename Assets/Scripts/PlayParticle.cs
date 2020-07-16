using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticle : MonoBehaviour
{


    public GameObject shinning;
    // Start is called before the first frame update
    void Start()
    {
        shinning.GetComponent<ParticleSystem>().Play();
    }

   void  FixedUpdate()
   {
       shinning.transform.position = gameObject.transform.position;
   }
}
