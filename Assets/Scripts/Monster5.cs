using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster5 : MonoBehaviour
{   
    public GameObject[] projectile;
    private GameObject player;
    private float time;
    public Animator animator;
    private bool isExcuted1,isExcuted2,isExcuted,isHurt;
    private  IEnumerator coroutine1,coroutine2;
    public SpriteRenderer m_SpriteRenderer;
    private ButtonManager  buttonManager;
    public AudioSource AudioClip,audio;
    public  GameObject[] particle;
    private GameObject smoke,hurt;
    private Rigidbody2D rd;
    private int life;

    // Start is called before the first frame update
    void Start()
    {
      isExcuted1 = false;
      isExcuted2 = false;
      isExcuted=false;
      isHurt = false;
      life = 20;
      coroutine1 = LeftProjectile();
      coroutine2 = RightProjectile();
      player = GameObject.FindWithTag("Player");
      rd = gameObject.GetComponent<Rigidbody2D>();
      m_SpriteRenderer = GetComponent<SpriteRenderer>();
      buttonManager = GameObject.FindWithTag("Button Management").GetComponent<ButtonManager>();  
    }

    // Update is called once per frame
    void Update()
    {
      if(life <= 0f && !isExcuted)
      {
        m_SpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        Destroy(gameObject,1f);
        isExcuted = true;
      }


      if(buttonManager.gamePause == true)
      {
          AudioClip.Pause();
          audio.Pause();
	    }
      else if(buttonManager.gamePause == false) 
      {
          AudioClip.UnPause();
          audio.Pause();
	    }

     if(Mathf.Abs(transform.position.y-player.transform.position.y)<20f)
     {
       if(player.transform.position.x>transform.position.x)
       {
          m_SpriteRenderer.flipX = true; 
          if(!isExcuted2)
          {  
            StopCoroutine(coroutine1);
            StartCoroutine(coroutine2);
            isExcuted2 = true;
            isExcuted1 = false;
          }
       }

       else if(player.transform.position.x<=transform.position.x)
       {
           m_SpriteRenderer.flipX = false;
           if(!isExcuted1)
           {  
              StopCoroutine(coroutine2);
              StartCoroutine(coroutine1);
              isExcuted1 = true;
              isExcuted2 = false;
           }  
       }
     }
    else if(Mathf.Abs(transform.position.y-player.transform.position.y)>=20f)
    {
        StopCoroutine(coroutine1);
        StopCoroutine(coroutine2);
        isExcuted1 = false;
        isExcuted2 = false;
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
        if(gameObject.GetComponent<Collider2D>().bounds.center.y<col.gameObject.GetComponent<Collider2D>().bounds.center.y)
        {
          Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
        rd.constraints = RigidbodyConstraints2D.FreezeAll;
      }
    }
    void OnCollisionStay2D(Collision2D col)
    {
      if(col.gameObject.CompareTag("Platform"))
      {
        rd.constraints = RigidbodyConstraints2D.FreezeAll;
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
      yield return  new WaitForSeconds(0.8f);
      gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
      isHurt = false; 
	}

    IEnumerator RightProjectile()
    { 
      while(true)
      {  
         time = Random.Range(2f,5f);
         animator.SetInteger("state", 1);
         Instantiate(projectile[Random.Range(0,projectile.Length)], new Vector2(transform.position.x+1f,transform.position.y+1.72f),Quaternion.identity);
         AudioClip.Play();
         yield return new WaitForSeconds(0.27f);
         animator.SetInteger("state", 2);
         yield return new WaitForSeconds(time-0.27f);
      }
     

    }

    IEnumerator LeftProjectile()
    { 
      while(true)
      {
         time = Random.Range(2f,5f);
         animator.SetInteger("state", 1);
         Instantiate(projectile[Random.Range(0,projectile.Length)], new Vector2(transform.position.x-1f,transform.position.y+1.72f),Quaternion.identity);
         AudioClip.Play();
         yield return new WaitForSeconds(0.27f);
         animator.SetInteger("state", 2);
         yield return new WaitForSeconds(time-0.27f);
      }
     

    }
}
