using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{   
    public Animator animator;
    public GameObject fire;
    private bool ready,isCoroutineStarted,startShooting,isExcuted,isExcuted2,isExcuted3,isHurt,isActive;
    private float speed;
    private int life;
    private Rigidbody2D rd;
    private Vector2 mPosition,kPosition,upVelocity;
    private Vector3 viewPos;
    private  IEnumerator coroutine;
    public AudioClip clip;
    private ButtonManager  buttonManager;
    public AudioSource[] AudioClips;
    public  GameObject[] particle;
    private GameObject can,cam,player,smoke,explosion,flame,hit1,hit2;
    
    // Start is called before the first frame update
    void Start()
    {  
      ready = true;
      speed = 1.8f;
      life = 100;
      isActive = true;
      isExcuted = false;
      isExcuted2 = false;
      isExcuted3 = false;
      startShooting = false;
      isCoroutineStarted = false;
      AudioClips[0].Play();
      coroutine = Fire();
      rd = gameObject.GetComponent<Rigidbody2D>();
      can = GameObject.FindWithTag("Canvas");
      cam = GameObject.FindWithTag("MainCamera");
      player = GameObject.FindWithTag("Player");
      buttonManager = GameObject.FindWithTag("Button Management").GetComponent<ButtonManager>();
      smoke = Instantiate(particle[0], new Vector2(transform.position.x+1.4f, transform.position.y+1f),Quaternion.identity) as GameObject;
      flame = Instantiate(particle[2], new Vector2(transform.position.x+1.4f, transform.position.y+1f),Quaternion.identity) as GameObject;
      smoke.SetActive(false);
      flame.SetActive(false);
      StartCoroutine(countdown());
      transform.parent = can.transform;
    }

    // Update is called once per frame
    void Update()
    {

      if(buttonManager.gamePause == true)
      {
          AudioClips[0].Pause();
          AudioClips[1].Pause();
          AudioClips[2].Pause();
	    }
      else if(buttonManager.gamePause == false) 
       {
           AudioClips[0].UnPause();
           AudioClips[1].UnPause();
           AudioClips[2].UnPause();
       }
     
     if(isActive==true)
     {
       transform.position = new Vector2 (transform.position.x,can.transform.position.y + 16f);
     }
     else 
     {
       rd.velocity = new Vector2(0f,0f);
       transform.parent = null; 
       if(ready == false)
         {
           StartCoroutine(Standby());
           ready = true;
         }       
           StopCoroutine(coroutine); 
     }

     smoke.transform.position = new Vector2(transform.position.x+1.4f, transform.position.y+1f);
     flame.transform.position = new Vector2(transform.position.x+1.4f, transform.position.y+1f);
     flame.transform.parent = gameObject.transform;
     if(life <= 45f && !isExcuted)
     {
       smoke.SetActive(true);
       smoke.GetComponent<ParticleSystem>().Play();
       isExcuted = true;
     }
     else if(life <= 30f && !isExcuted3)
     {
       flame.SetActive(true);
       flame.GetComponent<ParticleSystem>().Play();
       isExcuted3 = true;
     }
     else if(life <= 0f && !isExcuted2)
     { 
       StartCoroutine(Demage());
     }

    
     mPosition = new Vector2(player.transform.position.x,transform.position.y);
     kPosition = new Vector2(transform.position.x,transform.position.y);
     //mPosition = new Vector2(player.transform.position.x,0f);
     //kPosition = new Vector2(transform.position.x,0f);
     
     if(transform.position.y - player.transform.position.y <= 27f && life > 0f)
     {
       if(ready == true && Mathf.Abs(kPosition.x-mPosition.x)<3f)
         {
            StartCoroutine(ReadyShooting());
            ready = false;
         }
       else if(ready == false && Mathf.Abs(kPosition.x-mPosition.x)>3f)
        {
            StartCoroutine(Standby());
            ready = true;
        }

       if(Mathf.Abs(kPosition.x-mPosition.x)>1f)
       {  
         rd.velocity = (mPosition-kPosition).normalized*speed; 
         StopCoroutine(coroutine);
         isCoroutineStarted = false;
       }
       else if(Mathf.Abs(kPosition.x-mPosition.x)<=1f)
       {    
            
            if(isCoroutineStarted == false && rd.velocity.x == 0f && startShooting == true)
            {
              StartCoroutine(coroutine);
              isCoroutineStarted = true;
            }
            
            if(player.GetComponent<Rigidbody2D>().velocity.x==0f)
            {
              rd.velocity = new Vector2(0f,0f); 
            }
   
       }
     }
       
      else if(transform.position.y - player.transform.position.y > 27f)
      {
       if(ready == false)
         {
           StartCoroutine(Standby());
           ready = true;
         }       
           StopCoroutine(coroutine);     
      } 


     if(transform.position.x > 13f)
     {
       transform.position = new Vector2(13f,transform.position.y);
       
     }
     else if(transform.position.x < -13f)
     {
       transform.position = new Vector2(-13f,transform.position.y);
       
     }
    
    }

    
    void OnTriggerEnter2D(Collider2D col) 
   {
    if(col.gameObject.CompareTag("Shooting")&&!isHurt)
    {
      life-=10;
      hit1 = Instantiate(particle[3], new Vector2(transform.position.x, transform.position.y),Quaternion.identity) as GameObject;
      hit2 = Instantiate(particle[4], new Vector2(transform.position.x, transform.position.y),Quaternion.identity) as GameObject;
      hit1.GetComponent<ParticleSystem>().Play();
      hit2.GetComponent<ParticleSystem>().Play();
      AudioClips[2].Play();
      if(life>0f)
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
      yield return  new WaitForSeconds(0.5f);
      gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
      isHurt = false; 
  	}

    IEnumerator  ReadyShooting()
    {
      animator.SetInteger("attack", 1);
      yield return  new WaitForSeconds(0.47f);
      animator.SetInteger("attack", 2);
      startShooting = true;
    }

    IEnumerator Standby()
    {
      animator.SetInteger("attack", 3);
      yield return  new WaitForSeconds(0.47f);
      animator.SetInteger("attack", 4); 
      startShooting = false; 
    }

    IEnumerator  Fire()
    {
        while(true)
        {
          yield return new WaitForSeconds(0.5f);
          Instantiate(fire, new Vector2(transform.position.x, transform.position.y),Quaternion.identity );
          AudioClips[1].Play();
          yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Demage()
    {
      isExcuted2 = true;
      rd.velocity = new Vector2(0f,0f);
      explosion = Instantiate(particle[1], new Vector2(transform.position.x, transform.position.y),Quaternion.identity) as GameObject;
      explosion.GetComponent<ParticleSystem>().Play();
      gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
      AudioSource.PlayClipAtPoint(clip, new Vector3(cam.transform.position.x,cam.transform.position.y,cam.transform.position.z));
      yield return new WaitForSeconds(1f);
      Destroy(gameObject);
      Destroy(smoke);
      Destroy(flame);
      yield return new WaitForSeconds(1f);
      Destroy(explosion);
    }

    IEnumerator countdown()
    {
      yield return new WaitForSeconds(60f);
      isActive = false;
    }

}
