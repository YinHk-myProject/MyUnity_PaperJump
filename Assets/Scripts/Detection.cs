using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
  //public GameObject player; 

  void  OnTriggerEnter2D(Collider2D col)
  {
   if(col.gameObject.CompareTag("Sting")||col.gameObject.CompareTag("Shooting"))
   {
     Destroy(col.gameObject);
   }
   else if(col.gameObject.CompareTag("Projectile"))
   {
     col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-col.gameObject.GetComponent<Rigidbody2D>().velocity.x,col.gameObject.GetComponent<Rigidbody2D>().velocity.y);
   }

   else if(col.gameObject.CompareTag("Player"))
   {
     col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-col.gameObject.GetComponent<Rigidbody2D>().velocity.x,col.gameObject.GetComponent<Rigidbody2D>().velocity.y);
     if(col.gameObject.transform.position.x >= 12.5f)
     {
       col.gameObject.transform.position = new Vector2(12.5f,col.gameObject.transform.position.y);
     }
     else if(col.gameObject.transform.position.x <= -12.5f)
     {
       col.gameObject.transform.position = new Vector2(-12.5f,col.gameObject.transform.position.y);
     }
   }
  }
}
