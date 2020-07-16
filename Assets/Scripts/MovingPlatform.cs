using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{   
     
     private float Speed = 5f;
     private bool moveRight = true;
  
    
    // Update is called once per frame
    void Update()
    {
       Vector2 rightMovement =  new Vector2( transform.position.x  + Speed * Time.deltaTime, transform.position.y );
       Vector2  leftMovement =  new Vector2( transform.position.x  - Speed * Time.deltaTime, transform.position.y );

       if (transform.position.x > 10.5f )
        {
           moveRight = false;
         
		}

        if (transform.position.x < -9.50f )
        {
           moveRight = true;
          
		}

        if (moveRight)
        {
            transform.position = rightMovement;
		}

        else
             transform.position = leftMovement;    
	
    }



     
}
