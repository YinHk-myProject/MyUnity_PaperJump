using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{

    private bool unhit;
    // Start is called before the first frame update
    void Start()
    {
        unhit = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnCollisionEnter2D(Collision2D col)
     {
       if(col.gameObject.tag == "Player" && col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0f &&  col.gameObject.transform.position.y > transform.position.y && unhit == true)
       { 
          unhit = false;
          if(col.gameObject.transform.position.x > transform.position.x)
          {
             transform.Rotate(0, 0, -20f);
		  }
          else if(col.gameObject.transform.position.x < transform.position.x)
          {
             transform.Rotate(0, 0, 20f);
		  }
	   }

     }
}
