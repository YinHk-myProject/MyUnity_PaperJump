using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUpward : MonoBehaviour
{

     private float Speed = 6f;
     private bool moveUp,unhit;
     private Vector2 pos;
    
  
    void Start()
    {
        pos = new Vector2(transform.position.x,transform.position.y);
        moveUp = false;
        unhit = true;
        
	}
    // Update is called once per frame
    void Update()
    {

       if(moveUp)
       {
          
         if(transform.position.y < pos.y + 20.0f)
         {
           transform.position = new Vector2( transform.position.x, transform.position.y  + Speed * Time.deltaTime ); 
          
		 }
         else
         {
            moveUp = false;
	      }
	   }
       

     }

     void OnCollisionEnter2D(Collision2D col)
     {
       if(col.gameObject.tag == "Player" && col.gameObject.GetComponent<Rigidbody2D>().velocity.y == 0f &&  col.gameObject.transform.position.y > transform.position.y && unhit == true)
       { 
          unhit = false;
          moveUp = true;
	   }

     }

    
}
