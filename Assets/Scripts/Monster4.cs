using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster4 : MonoBehaviour
{   private SpriteRenderer m_SpriteRenderer;
    private ButtonManager  buttonManager;
    public AudioSource AudioClip,audio;
    private BoxCollider2D boxcollider2d;
    private Rigidbody2D rd;
    public  GameObject[] particle;
    private GameObject smoke,hurt;
    private float Speed = 4f;
    private bool moveRight,isExcuted,isHurt;
    private int life;

    // Start is called before the first frame update
    void Start()
    {
      buttonManager = GameObject.FindWithTag("Button Management").GetComponent<ButtonManager>();
      boxcollider2d = gameObject.GetComponent<BoxCollider2D>();
      m_SpriteRenderer = GetComponent<SpriteRenderer>();
      rd = gameObject.GetComponent<Rigidbody2D>();
      AudioClip.Play(); 
      Direction(); 
      isExcuted=false;
      isHurt = false;
      life = 20;
    }

    // Update is called once per frame
    void Update()
    {
      if(buttonManager.gamePause == true)
      {
          AudioClip.Pause();
          audio.Pause();
	  }
      else if(buttonManager.gamePause == false) 
      {
          AudioClip.UnPause();
          audio.UnPause();
	    }



      if(life <= 0f && !isExcuted)
      {
       m_SpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
       Destroy(gameObject);
       isExcuted = true;
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

    void OnCollisionEnter2D(Collision2D col) 
    {
      if(col.gameObject.tag!="Platform"&&col.gameObject.tag!="Player")
      {
         Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
      }
      else if(col.gameObject.CompareTag("Platform"))
      {
        if(gameObject.GetComponent<Collider2D>().bounds.center.y<col.gameObject.GetComponent<Collider2D>().bounds.center.y)
        {
          Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
        rd.constraints = RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation;
      
      }

    }
    
    void OnCollisionStay2D(Collision2D col)
    {
      if(col.gameObject.CompareTag("Platform"))
      {
        rd.constraints = RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation;
      }
    }

    void OnTriggerEnter2D(Collider2D col) 
  {
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
      yield return  new WaitForSeconds(0.6f);
      gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
      isHurt = false; 
	}
    private void Direction()
    {  
      
      if(transform.position.x>=0f)
      {
         moveRight = true;
         m_SpriteRenderer.flipX = true;
	  }
      else if(transform.position.x<0f)
      {
         moveRight = false;
         m_SpriteRenderer.flipX = false;
      }
         
    }
}
