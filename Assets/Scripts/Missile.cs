using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missile : MonoBehaviour
{
   //private GameManagement gamemanager;
   public GameObject smoke;
   //private int Life;


   void Start()
   {
     //gamemanager = GameObject.FindWithTag("Game Manager").GetComponent<GameManagement>();
     //Instantiate(smoke,new Vector2(transform.position.x , transform.position.y),Quaternion.identity);
     smoke.GetComponent<ParticleSystem>().Play();
	}

  void FixedUpdate()
   {
      smoke.transform.position = transform.position;
     
   }

   void OnTriggerEnter2D(Collider2D col)
    {

     if (col.gameObject.tag == "Player")
     {
        Destroy(gameObject);
       // gamemanager.gameOver = true;
      }

    }

   
}



