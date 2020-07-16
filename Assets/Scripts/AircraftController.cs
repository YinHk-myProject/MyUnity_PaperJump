using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AircraftController : MonoBehaviour
{
     public GameObject  missilePrefab,flaming1,flaming2;
     private GameObject Flaming1,Flaming2;
     public AudioSource[] AudioClip; 
     private float Speed = 10f;
     private int life;
     private Rigidbody2D rd;
     public AudioClip clip;
     private bool moveRight,isExcuted,isExcuted2,isExcuted3,isHurt;
     private ButtonManager  buttonManager;
     private SpriteRenderer m_SpriteRenderer;
     public  GameObject[] particle;
     private GameObject smoke,explosion,cam,hit1,hit2;
     

   
    // Start is called before the first frame update
    void Awake()
    {  
       Flaming1 = Instantiate(flaming1, new Vector2(transform.position.x, transform.position.y),Quaternion.identity) as GameObject;
       Flaming1.GetComponent<ParticleSystem>().Play();
       Flaming2 = Instantiate(flaming2, new Vector2(transform.position.x, transform.position.y),Quaternion.identity) as GameObject;
       Flaming2.GetComponent<ParticleSystem>().Play();
       Flaming1.transform.parent = gameObject.transform;
       Flaming2.transform.parent = gameObject.transform;
       InvokeRepeating("LaunchProjectile", 1.0f , 2.3f);
       m_SpriteRenderer = GetComponent<SpriteRenderer>();
       buttonManager = GameObject.FindWithTag("Button Management").GetComponent<ButtonManager>();
       cam = GameObject.FindWithTag("MainCamera");
       smoke = Instantiate(particle[0], new Vector2(transform.position.x, transform.position.y-1f),Quaternion.identity) as GameObject;
       explosion = Instantiate(particle[1], new Vector2(transform.position.x, transform.position.y),Quaternion.identity) as GameObject;
       hit1 = Instantiate(particle[2], new Vector2(transform.position.x, transform.position.y),Quaternion.identity) as GameObject;
       hit2 = Instantiate(particle[3], new Vector2(transform.position.x, transform.position.y),Quaternion.identity) as GameObject;
       smoke.SetActive(false);
       explosion.SetActive(false);
       hit1.SetActive(false);
       hit2.SetActive(false);
       rd = gameObject.GetComponent<Rigidbody2D>();
       AudioClip[0].Play();
       moveRight = true;
       isExcuted = false;
       isExcuted2 = false;
       life = 30;
    }

    // Update is called once per frame
    void Update()
    {
     
      if(buttonManager.gamePause == true)
      {
          AudioClip[0].Pause();
          AudioClip[1].Pause();
          AudioClip[2].Pause();
	    }
      else if(buttonManager.gamePause == false) 
       {
           AudioClip[0].UnPause();
           AudioClip[1].UnPause();
           AudioClip[2].UnPause();
       }
    
       smoke.transform.position = new Vector2(transform.position.x, transform.position.y-1f);  
       explosion.transform.position = new Vector2(transform.position.x, transform.position.y);
       hit1.transform.position = new Vector2(transform.position.x, transform.position.y-1f);
       hit2.transform.position = new Vector2(transform.position.x, transform.position.y-1f);

       if(life <= 15 && !isExcuted)
       {
         smoke.SetActive(true);
         smoke.GetComponent<ParticleSystem>().Play();
         isExcuted = true;
       }
       else if(life <= 0 && !isExcuted2)
       { 
         StartCoroutine(Demage());
       }
         
        
       Vector2 rightMovement =  new Vector2( transform.position.x  + Speed * Time.deltaTime, transform.position.y );
       Vector2 leftMovement =  new Vector2( transform.position.x  - Speed * Time.deltaTime, transform.position.y );
       

        if (transform.position.x > 19f && life > 0f)
        {
           moveRight = false;
           m_SpriteRenderer.flipX = false;
		    }

        if (transform.position.x < -19f && life > 0f )
        {
           moveRight = true;
           m_SpriteRenderer.flipX = true;
		    }

        if (moveRight)
        {
           transform.position = rightMovement;
           Flaming1.SetActive(false);
           Flaming2.SetActive(true);
           Flaming2.transform.position = new Vector2(transform.position.x-3.19f,transform.position.y-0.41f);
		    }

        else if(!moveRight)
        {
           transform.position = leftMovement;
           Flaming1.SetActive(true);
           Flaming2.SetActive(false);
           Flaming1.transform.position = new Vector2(transform.position.x+3.19f,transform.position.y-0.41f);
        }
            
     }

      void OnTriggerEnter2D(Collider2D col) 
     {
      if(col.gameObject.CompareTag("Shooting")&&!isHurt)
      {
       life-=10;
       hit1.SetActive(true);
       hit2.SetActive(true);
       hit1.GetComponent<ParticleSystem>().Play();
       hit2.GetComponent<ParticleSystem>().Play();
       AudioClip[2].Play();
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


     void  LaunchProjectile( )
     {  if(transform.position.x < 12.5f && transform.position.x > -12.5f)
       {
        Instantiate(missilePrefab, new Vector2(transform.position.x, transform.position.y - 2),Quaternion.identity );
        AudioClip[1].Play();
       }
        

      }


     IEnumerator Demage()
     {
       isExcuted2 = true;
       explosion.SetActive(true);
       rd.velocity = new Vector2(0f,0f);
       gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
       explosion.GetComponent<ParticleSystem>().Play();
       AudioSource.PlayClipAtPoint(clip, new Vector3(cam.transform.position.x,cam.transform.position.y,cam.transform.position.z));
       yield return new WaitForSeconds(1f);
       Destroy(gameObject);
       Destroy(smoke);
       yield return new WaitForSeconds(1f);
       Destroy(explosion);
     }

}
