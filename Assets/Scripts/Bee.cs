using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bee : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;
    private ButtonManager  buttonManager;
    public AudioSource AudioClip;
    private BoxCollider2D boxcollider2d;
    private float Speed = 5f;
    private bool moveRight; private int ran; 
    

    // Start is called before the first frame update
    void Start()
    {
         buttonManager = GameObject.FindWithTag("Button Management").GetComponent<ButtonManager>();
         boxcollider2d = gameObject.GetComponent<BoxCollider2D>();
         m_SpriteRenderer = GetComponent<SpriteRenderer>();
         m_SpriteRenderer.flipY = true;
         AudioClip.Play(); 
         Direction(); 
    }

    // Update is called once per frame
    void Update()
    {
       
      if(buttonManager.gamePause == true)
      {
          AudioClip.Pause();
	  }
     else if(buttonManager.gamePause == false) 
       {
           
           AudioClip.UnPause();
	   }
          
       Vector2 rightMovement =  new Vector2( transform.position.x  + Speed * Time.deltaTime, transform.position.y );
       Vector2  leftMovement =  new Vector2( transform.position.x  - Speed * Time.deltaTime, transform.position.y );

       if (transform.position.x > 12f )
        {
           moveRight = false;
           m_SpriteRenderer.flipX = false;
		}

        if (transform.position.x < -12f )
        {
           moveRight = true;
           m_SpriteRenderer.flipX = true;
		}

        if (moveRight)
        {
            transform.position = rightMovement;
		}

        else
             transform.position = leftMovement;
	}



  void Direction()
  {  
      ran = Random.Range(1,3);
      if(ran == 1)
      {
         moveRight = true;
	  }
      else
         moveRight = false;
  }

   
}
