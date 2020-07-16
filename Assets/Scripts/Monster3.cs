using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster3 : MonoBehaviour
{
    public Sprite[] sprites;  
    private Rigidbody2D rd;
    private bool isExecuting,isHurt,isExcuted;
    private GameObject player;
    public GameObject[] sting;
    private SpriteRenderer spriteR;
    private BoxCollider2D boxcollider2d;
    public AudioSource[] AudioClip;
    private ButtonManager  buttonManager;
    public  GameObject[] particle;
    private GameObject smoke,hurt;
    private int life;

    // Start is called before the first frame update
    void Start()
    {   
         life = 20;
         isExecuting = false;
         rd = gameObject.GetComponent<Rigidbody2D>();
         spriteR = gameObject.GetComponent<SpriteRenderer>(); 
         boxcollider2d = gameObject.GetComponent<BoxCollider2D>();
         buttonManager = GameObject.FindWithTag("Button Management").GetComponent<ButtonManager>();
         player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       
        if(boxcollider2d.enabled == false)
        {
           rd.isKinematic = false;
        }

        if(Mathf.Abs(transform.position.y - player.transform.position.y) < 12f && Mathf.Abs(transform.position.y - player.transform.position.y) >= 0f)
        {
           if(isExecuting == false)
           {
              StartCoroutine(Attack());
		       }
            isExecuting = true;
		    }
        else if(Mathf.Abs(transform.position.y - player.transform.position.y) >= 12f)
        {
           StopAllCoroutines();
           isExecuting = false;
           spriteR.sprite = sprites[0];

		    }


        if(boxcollider2d.enabled == false)
        {
           Destroy(gameObject,0.8f);
        }


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


         if(life <= 0f && !isExcuted)
         {
           spriteR.color = new Color(1f, 1f, 1f, 0f);
           Destroy(gameObject);
           isExcuted = true;
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
       AudioClip[1].Play();
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

    IEnumerator  Attack()
    {
    while(true)
    {   
      spriteR.sprite = sprites[1];
      AudioClip[0].Play();
      AudioClip[2].Play();
      Instantiate(sting[0],new Vector2(transform.position.x,transform.position.y+2f),Quaternion.identity);
      Instantiate(sting[1],new Vector2(transform.position.x+2f,transform.position.y),Quaternion.identity);
      Instantiate(sting[2],new Vector2(transform.position.x+1.45f,transform.position.y+1.3f),Quaternion.identity); 
      Instantiate(sting[3],new Vector2(transform.position.x,transform.position.y-2f),Quaternion.identity); 
      Instantiate(sting[4],new Vector2(transform.position.x-2f,transform.position.y),Quaternion.identity); 
      Instantiate(sting[5],new Vector2(transform.position.x-1.45f,transform.position.y+1.3f),Quaternion.identity); 
      Instantiate(sting[6],new Vector2(transform.position.x-1.45f,transform.position.y-1.3f),Quaternion.identity); 
      Instantiate(sting[7],new Vector2(transform.position.x+1.45f,transform.position.y-1.3f),Quaternion.identity);   
      yield return  new WaitForSeconds(1.5f);
	    spriteR.sprite = sprites[0] ;
      yield return  new WaitForSeconds(4.5f);
	}
 
	}


}
