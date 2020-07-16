using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{   
      
      private Rigidbody2D rb;
      private BoxCollider2D boxCollider;
      public float fallingTime = 1.5f;
     

   void Start()
   {
      rb = gameObject.GetComponent<Rigidbody2D>();
      boxCollider = gameObject.GetComponent<BoxCollider2D>();
   }
   

    void OnCollisionEnter2D(Collision2D col)
      {   

          if (col.gameObject.tag == "Player" && col.gameObject.transform.position.y > transform.position.y && col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 )
          {  
              Invoke("fallingPlatform",fallingTime);
           
           }  

            if (col.gameObject.tag != "Player")
          {
              Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), boxCollider);
		  }
         
	  }
  
    private void fallingPlatform()
     {
            rb.isKinematic =  false;
	 }
}
