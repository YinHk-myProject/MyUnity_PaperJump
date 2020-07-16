using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster6 : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;
    private ButtonManager  buttonManager;
    public Animator animator;
    public AudioSource AudioClip,audio;
    private BoxCollider2D boxcollider2d;
    private Vector2 mPosition,kPosition;
    private Rigidbody2D rd;
    public  GameObject[] particle;
    private GameObject smoke,hurt;
    private float speed;
    private int life;
    private bool moveRight,isExcuted,isHurt;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
      buttonManager = GameObject.FindWithTag("Button Management").GetComponent<ButtonManager>();
      boxcollider2d = gameObject.GetComponent<BoxCollider2D>();
      m_SpriteRenderer = GetComponent<SpriteRenderer>();
      player = GameObject.FindWithTag("Player");
      rd = gameObject.GetComponent<Rigidbody2D>();
      speed = Random.Range(2f,3.5f);
      AudioClip.Play();
      life = 30;
    }

    // Update is called once per frame
    void Update()
    {

      transform.Rotate(0, 0, 0);

      if(life <= 0f && !isExcuted)
      {
       m_SpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
       Destroy(gameObject,1f);
       isExcuted = true;
      }

      if(buttonManager.gamePause == true || boxcollider2d.enabled == false)
      {
         AudioClip.Pause();
         audio.Pause();
      }
      else if(buttonManager.gamePause == false) 
      {
          AudioClip.UnPause();
          audio.UnPause();
	    }


      if(transform.position.x > 12.5f)
      {
        transform.position = new Vector2(12.5f,transform.position.y);
      }
      else if(transform.position.x < -12.5f)
      {
        transform.position = new Vector2(-12.5f,transform.position.y);
      }
      


      if(player.transform.position.x>transform.position.x)
      {
        m_SpriteRenderer.flipX = true;  
      }
      else if(player.transform.position.x<=transform.position.x)
      {
        m_SpriteRenderer.flipX = false;    
      }

    
      mPosition = new Vector2(player.transform.position.x,0f);
      kPosition = new Vector2(transform.position.x,0f); 

      if(life>0 && boxcollider2d.enabled == true && buttonManager.gamePause == false)
      {
        if(Mathf.Abs(transform.position.y-player.transform.position.y)<5f)
        {
          if(Mathf.Abs(kPosition.x-mPosition.x)>1f)
          {
            rd.velocity = (mPosition-kPosition).normalized*speed;
            animator.SetBool("walk", true);
            AudioClip.UnPause();
          }
          else if(Mathf.Abs(kPosition.x-mPosition.x)<=1f)
          {
            rd.velocity = new Vector2(0f,0f);
            animator.SetBool("walk", false);
            AudioClip.Pause();
          }
        }
        else
        {
          rd.velocity = new Vector2(0f,0f);
          animator.SetBool("walk", false);
          AudioClip.Pause();
        }
      } 
      
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
      if(col.gameObject.tag!="Platform"&&col.gameObject.tag!="Player")
      {
         Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
      }
      else if(col.gameObject.CompareTag("Platform"))
      {
        if(boxcollider2d.bounds.center.y<col.gameObject.GetComponent<Collider2D>().bounds.center.y)
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
        rd.constraints = RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation;;
      }
    }

    void OnTriggerEnter2D(Collider2D col) 
    {
     if(col.gameObject.CompareTag("Shooting")&&!isHurt&&life>0)
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



}
