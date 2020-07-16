using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
   
   private  AudioSource audioBounce;
   [SerializeField] private float  bounceForce = 3000f;

    // Start is called before the first frame update
    void Start()
    {
         audioBounce =  GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 /*&& col.gameObject.transform.position.y > transform.position.y*/  && col.gameObject.tag == "Player" )
		{
		  col.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * bounceForce);
          audioBounce.Play(0);
		}
	}
}
