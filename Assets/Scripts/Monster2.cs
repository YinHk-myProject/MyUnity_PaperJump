using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster2 : MonoBehaviour
{

    public Sprite[] sprites;  
    private SpriteRenderer spriteR;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxcollider2d;
    [SerializeField] private LayerMask platform;
    public AudioSource AudioClip,AudioClip2,audio;
    private ButtonManager  buttonManager;
    public  GameObject[] particle;
    private GameObject smoke,hurt;
    private bool isHurt,isExcuted;
    private int life;
    
    // Start is called before the first frame update
    void Start()
    {
         buttonManager = GameObject.FindWithTag("Button Management").GetComponent<ButtonManager>();
         spriteR = gameObject.GetComponent<SpriteRenderer>(); 
         rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
         boxcollider2d = gameObject.GetComponent<BoxCollider2D>();
         StartCoroutine(PlaySound());
         isExcuted = false;
         isHurt = false;
         life = 20;
    }

      void Update()
    {

      if(life <= 0f && !isExcuted)
      {
       spriteR.color = new Color(1f, 1f, 1f, 0f);
       Destroy(gameObject);
       isExcuted = true;
      }


      if(boxcollider2d.enabled == false)
      {
         spriteR.sprite = sprites[2];
	  }

      
      if(buttonManager.gamePause == true)
      {
          AudioClip.Pause();
          AudioClip2.Pause();
          audio.Pause();
	    }
      else if(buttonManager.gamePause == false) 
      {
           AudioClip.UnPause();
           AudioClip2.UnPause();
           audio.UnPause();
	    }


       if (isGround() && boxcollider2d.enabled == true)
       {
          spriteR.sprite = sprites[0];
          particle[2].GetComponent<ParticleSystem>().Play();
	     }
       else if(!isGround() && boxcollider2d.enabled == true)
       {
          spriteR.sprite = sprites[1];
	     }
          


	}

    void OnTriggerEnter2D(Collider2D col)

    {

       if(col.gameObject.tag == "Platform" || col.gameObject.tag == "MovingPlatform")
       {          
          
         rigidbody2d.velocity = Vector2.up * 10f;
         //AudioClip.Play();
                  
	     }

       if ( col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Powerup"))
       {
          Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
		   }

       if(col.gameObject.CompareTag("Shooting")&&!isHurt)
       {
         life-=10;
         smoke = Instantiate(particle[1], new Vector2(transform.position.x, transform.position.y),Quaternion.identity) as GameObject;
         hurt = Instantiate(particle[0], new Vector2(transform.position.x, transform.position.y),Quaternion.identity) as GameObject;
         smoke.GetComponent<ParticleSystem>().Play();
         hurt.GetComponent<ParticleSystem>().Play();
         audio.Play();
         if(life>0)
         {
         StartCoroutine(ColorOfEnemy());
         }
         Destroy(col.gameObject);
       }
 

    }

    IEnumerator  ColorOfEnemy()
    {
      gameObject.GetComponent<SpriteRenderer>().color = new Color(0.3018868f, 0.3018868f, 0.3018868f, 1f);
      isHurt = true;
      yield return  new WaitForSeconds(0.8f);
      gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
      isHurt = false; 
	  }


    private bool isGround( )
    {

     //RaycastHit2D hit = Physics2D.BoxCast(boxcollider2d.bounds.center,boxcollider2d.bounds.size, 0f, Vector2.down, 1f, platform);
     float xOrigin =boxcollider2d.bounds.center.x;
     float yOrigin =boxcollider2d.bounds.center.y-0.85f;
     RaycastHit2D hit = Physics2D.BoxCast(new Vector2(xOrigin,yOrigin),new Vector2(2.8f,1.7f), 0f, Vector2.down, 0.2f, platform);
     
     if( hit.collider != null)
     {
      return true;
	   }
     else 
     return false;
   
    }

    IEnumerator PlaySound()
    {
      while(true)
      {  
        AudioClip2.Play(); 
        AudioClip.Play();
        yield return new WaitForSeconds(3f);
      }
      
    }


}
