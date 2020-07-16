using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sting : MonoBehaviour
{   
    public float speed = 10f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {     
          rb=gameObject.GetComponent<Rigidbody2D>();
         
       
    }
        void Update()
    {
         
        if(GameObject.Find("monster virus(Clone)")==null)
         {
           Destroy(gameObject);
         }
         else
         rb.velocity=(transform.position-GameObject.Find("monster virus(Clone)").transform.position).normalized*speed;
        
    }
   
}
