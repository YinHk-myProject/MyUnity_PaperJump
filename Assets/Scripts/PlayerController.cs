using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{  
    
    [SerializeField] private float moveSpeed = 35f;
    [SerializeField] private float hyperJump = 10f;
    [SerializeField] private LayerMask platform,enemyObject;
    public float jumpSpeed = 30f;
    private float move;
    AudioSource audioSource;
    public Sprite[] sprites; 
    private SpriteRenderer spriteR;
    private BoxCollider2D boxcollider2d;
    private Rigidbody2D rigidbody2d;
    public bool hasPowerup = false,isDead,isHurt,isShooting,isAttacking,getItem,getItem2; 
    private bool isProtected,isDemage1,isDemage2,isDemage3,isDemage4,isDemage5,isPlayed,isPlayed2,isReloading;
    private  int stepOfJump,maxOfbullet;
    private ButtonManager  buttonManager;
    public int numOfBullet;
    private float bestScore=0f, BestScore,newEnergy,demage;
    public float score,boundary = 14f;
    public TextMeshProUGUI scoreText,bullets;
    private Scene loadscene;
    public Slider healthBar;
    public float energy;
    public Joystick joystick;
    private RaycastHit2D Hit;
    public GameObject hitLogo,hurt,gameover,note,bomb,bang,great,hole,cam;
    private GameObject HitLogo,Hurt,gameoverLogo,Bomb,Bang,Great,Hole;
    public AudioSource[] AudioClips;
    public  GameObject[] explosion,bullet;
    private GameObject[] enemy,missile,particles;
    private  IEnumerator coroutine;
    private float lerpSpeed = 0.8f;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {   
        coroutine = Pollision();
        boxcollider2d = gameObject.GetComponent<BoxCollider2D>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        spriteR = gameObject.GetComponent<SpriteRenderer>(); 
        audioSource = GetComponent<AudioSource>();
        buttonManager = GameObject.FindWithTag("Button Management").GetComponent<ButtonManager>();
        loadscene = GameObject.Find("Scene Management").GetComponent<Scene>();
        HitLogo = Instantiate(hitLogo, new Vector2(transform.position.x, transform.position.y - 2.5f),Quaternion.identity) as GameObject;
        Hurt = Instantiate(hurt, new Vector2(transform.position.x, transform.position.y - 2.5f),Quaternion.Euler(0, 0, 18)) as GameObject;
        gameoverLogo = Instantiate(gameover, new Vector2(0f,note.transform.position.y + 5f ),Quaternion.identity) as GameObject;
        Bomb = Instantiate(bomb, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, 20)) as GameObject;
        Bang = Instantiate(bang, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, 20)) as GameObject;
        Great = Instantiate(great, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, 20)) as GameObject;
        Hole = Instantiate(hole, new Vector2(transform.position.x, transform.position.y),Quaternion.identity) as GameObject;
        gameoverLogo.SetActive(false);
        Hole.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        Bomb.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        Bang.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        HitLogo.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        Great.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        scoreText.text = "Score: " + ( Mathf.Round(score)*10).ToString(); 
        bullets.text = "Bullet: " + numOfBullet.ToString();
        BestScore = PlayerPrefs.GetFloat("BestScore", 0f);
        rigidbody2d.velocity = Vector2.up * jumpSpeed;
        energy = healthBar.maxValue;
        maxOfbullet = 99;
        numOfBullet = maxOfbullet;
        isProtected = false;isDead = false;
        isDemage1 = false;isDemage2 = false;isDemage3 = false;isDemage4 = false;
        isPlayed = false;isPlayed2 = false;
        isShooting = false;
        isReloading = false;
        isAttacking = false;
        getItem = false;
        getItem2 = false;
        isHurt = false;
        score = 0f;
        
        particles = new GameObject [explosion.Length];
        for(int i =0; i < explosion.Length; i++)
        {
          particles [i]  = Instantiate(explosion [i] ,transform.position, Quaternion.identity) as GameObject;
          
	      }
        
    }

    // Update is called once per frame
    void  FixedUpdate()
    {
     healthBar.value = energy;
     particles[1].transform.position = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y-1.3f);
     particles[2].transform.position = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y);
     particles[3].transform.position = transform.position;
     particles[4].transform.position = transform.position;
     particles[5].transform.position = transform.position;
     particles[6].transform.position = transform.position;
     particles[8].transform.position = transform.position;

     Hit = Physics2D.BoxCast( boxcollider2d.bounds.center,boxcollider2d.bounds.size, 0f, Vector2.down, 1f, enemyObject);
    
     gameoverLogo.transform.position = new Vector2(note.transform.position.x,note.transform.position.y + 5f);
	 
     if(buttonManager.gamePause == true)
      {
          AudioListener.pause = true;
	  }
      else if(buttonManager.gamePause == false) 
       {
           AudioListener.pause = false;
	   }

       //move = Input.GetAxis("Horizontal");
       move = joystick.Horizontal;
       rigidbody2d.AddForce(Vector2.right * moveSpeed  * move );

        if(rigidbody2d.velocity.y >0 && transform.position.y > score)
        {
              score = transform.position.y;
              UpdateScore();
              CheckForBestScore();
        }


       if(isDead)
       {
          StartCoroutine(Gameover());
	   }
       
       ChackDirection();
       CheckStep();
       CheckEnergy();
       AnimationOfPlayer();
       isGround();
       UpdateBullet();
       //Shoot();

       if(isDemage1 == true)
       {
          Demage(demage);
          if(energy == newEnergy)
          {
            isDemage1 = false;
		  }
	   }
       else if(isDemage2 == true)
       {
          Demage(demage);
          if(energy == newEnergy)
          {
            isDemage2 = false;
		  }
	   }
        else if(isDemage3 == true)
        {
          Demage(demage);
          if(energy == newEnergy)
          {
            isDemage3 = false;
		  }
      }
        else if(isDemage4 == true)
        {
          Demage(demage);
          if(energy == newEnergy)
          {
            isDemage4 = false;
      }
      }
        else if(isDemage5 == true)
        {
          Demage(demage);
          if(energy == newEnergy)
          {
            isDemage5 = false;
      }
      }
     
     }
    
    void Update()
    {
      if(buttonManager.gamePause == true)
      {
        AudioListener.pause = true;
	    }
      else if(buttonManager.gamePause == false) 
      {
        AudioListener.pause = false;
	    }

      if(/*Input.GetMouseButton(0)*/joystick.Vertical > 0.5f)
      {   
        stepOfJump--;
        if(isGround())
        {
          if(rigidbody2d.velocity.y == 0)
          {
            rigidbody2d.velocity = Vector2.up * jumpSpeed;
            AudioClips[0].Play();
          }
        }
        else 
        {    
         if(/*Input.GetMouseButton(0)*/joystick.Vertical > 0.5f)
         {
           if(stepOfJump==1/*&&rigidbody2d.velocity.y>=0f*/)
           {
             rigidbody2d.velocity = Vector2.up * jumpSpeed;
             AudioClips[0].Play();
             stepOfJump--;
           }
         }
        }
      }
    }


    void CheckStep()
    {
      if(isGround())
      {
        stepOfJump = 2;
      }
	  }

   //Checking whether player is grounded
   public bool isGround()
   {
     
     RaycastHit2D hit = Physics2D.BoxCast( boxcollider2d.bounds.center,boxcollider2d.bounds.size, 0f, Vector2.down, 1f, platform);

     if( hit.collider != null && hit.collider.tag != "Enemy" && rigidbody2d.velocity.y==0f)
     {
      
      return true;
	   }
     else 
     return false;
   
   }

    void ChackDirection()
    {
      if (move >0 && !isDead)
       {
          gameObject.GetComponent<SpriteRenderer>().flipX = true;
	   }
       else
           gameObject.GetComponent<SpriteRenderer>().flipX = false;
	  }

   

     void AnimationOfPlayer()
      {
         if(isGround())
         { 
           if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_idle")||animator.GetCurrentAnimatorStateInfo(0).IsName("Player_walk"))
           {
             animator.SetFloat("Speed",Mathf.Abs(move));
             animator.SetInteger("isGround",0);
             animator.SetInteger("isJumping",0);
             animator.SetInteger("isShooting",0);
             animator.SetInteger("noShooting",0);
           }

           if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_jump")&&rigidbody2d.velocity.y == 0)
           {
             animator.SetFloat("Speed",0);
             animator.SetInteger("isJumping",0);
             animator.SetInteger("isGround",1);
             animator.SetInteger("isShooting",0);
             animator.SetInteger("noShooting",0);
           }

           if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_doublejump")&&rigidbody2d.velocity.y == 0)
           {
             animator.SetFloat("Speed",0);
             animator.SetInteger("isJumping",0);
             animator.SetInteger("isGround",2);
             animator.SetInteger("isShooting",0);
             animator.SetInteger("noShooting",0);
           }

           if(isShooting)
           { 
             animator.SetFloat("Speed",0);
             animator.SetInteger("isJumping",0);
             animator.SetInteger("isGround",0);
             animator.SetInteger("noShooting",0);
             if(move==0f)
             {
               animator.SetInteger("isShooting",1);
             }
             else
               animator.SetInteger("isShooting",2);
           }
           else if(!isShooting&&(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_shoot1")||animator.GetCurrentAnimatorStateInfo(0).IsName("Player_idle shoot2")||animator.GetCurrentAnimatorStateInfo(0).IsName("Player_jump_shoot")))
           { 
             animator.SetFloat("Speed",0);
             animator.SetInteger("isJumping",0);
             animator.SetInteger("isGround",0);
             animator.SetInteger("isShooting",0);
             if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_shoot1"))
             {
               animator.SetInteger("noShooting",1);
             }
             else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_idle shoot2"))
             {
               animator.SetInteger("noShooting",2);
             }
             else if(!isShooting&&animator.GetCurrentAnimatorStateInfo(0).IsName("Player_jump_shoot"))
             {
               animator.SetInteger("noShooting",3);
             }
 
           }
           

         }
         else if(!isGround())
         {
           if(!isShooting)
           {
             if(stepOfJump != 0)
             {
               animator.SetFloat("Speed",0);
               animator.SetInteger("isJumping",1);
               animator.SetInteger("isGround",0);
               animator.SetInteger("isShooting",0);
               animator.SetInteger("noShooting",0);
             }
             else if(stepOfJump == 0)
             {
               animator.SetFloat("Speed",0);
               animator.SetInteger("isJumping",2);
               animator.SetInteger("isGround",0);
               animator.SetInteger("isShooting",0);
               animator.SetInteger("noShooting",0);
             }
             
           }
           else if(isShooting)
           {
             if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_jump"))
             {
               animator.SetFloat("Speed",0);
               animator.SetInteger("isJumping",0);
               animator.SetInteger("isGround",0);
               animator.SetInteger("isShooting",3);
               animator.SetInteger("noShooting",0);
             }
             else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_doublejump"))
             {
               animator.SetFloat("Speed",0);
               animator.SetInteger("isJumping",0);
               animator.SetInteger("isGround",0);
               animator.SetInteger("isShooting",4);
               animator.SetInteger("noShooting",0);
             }
             
           }
           
         }

      
	  }

    public void Shoot()
      {
         //if(Input.GetMouseButton(1))
         //{
            if(numOfBullet > 0)
            {
              if(!isGround())
              {
                StartCoroutine(Shooting());
                if(move>0f && !isReloading)
                {
                  StartCoroutine(Reloading());
                  StartCoroutine(IconChange3());
                  Instantiate(bullet[2], new Vector2(transform.position.x+1.92f,transform.position.y-0.41f), Quaternion.identity);
                }
                else if(move<=0f && !isReloading)
                {
                  StartCoroutine(Reloading());
                  StartCoroutine(IconChange3());
                  Instantiate(bullet[0], new Vector2(transform.position.x-1.92f,transform.position.y-0.41f), Quaternion.identity);
                }
              }
              else if(isGround())
              {
                
                if(move==0f && !isReloading)
                {
                  StartCoroutine(Reloading());
                  StartCoroutine(IconChange3());
                  Instantiate(bullet[1], new Vector2(transform.position.x+0.09f,transform.position.y+2.73f), Quaternion.identity);
                }
                else if(move<0f && !isReloading)
                {
                  StartCoroutine(Reloading());
                  StartCoroutine(IconChange3());
                  Instantiate(bullet[0], new Vector2(transform.position.x-1.92f,transform.position.y-0.41f), Quaternion.identity);
                }
                else if(move>0f && !isReloading)
                {
                  StartCoroutine(Reloading());
                  StartCoroutine(IconChange3());
                  Instantiate(bullet[2], new Vector2(transform.position.x+1.92f,transform.position.y-0.41f), Quaternion.identity);
                }
                StartCoroutine(Shooting());
                rigidbody2d.velocity = new Vector2(0f,0f);

              }
              
			      }
            else
               AudioClips[4].Play();   
		    //}

	   }

     void UpdateScore()
      {        
         scoreText.text = "score: " + ( Mathf.Round(score)*10).ToString();
         PlayerPrefs.SetFloat("YourScore" ,  Mathf.Round(score)*10);
	  }

    void CheckForBestScore()
      {
         if(Mathf.Round(score)*10 > BestScore)
         {
               bestScore =  score;
               PlayerPrefs.SetFloat("BestScore",  Mathf.Round(bestScore)*10);
               
		 }

	  }

    void CheckEnergy()
    {
       if(energy <= 0f)
        {
          gameObject.GetComponent<BoxCollider2D>().enabled = false;
          isDead = true; 
          if(isPlayed == false)
          {
            soundOfdeath();
		  }
		}
	}

    void soundOfdeath()
    {
       AudioClips[12].Play();
       isPlayed = true;
	}

    void UpdateBullet()
      {
       bullets.text = "Bullet: " + numOfBullet.ToString();
	  }

    void OnCollisionEnter2D(Collision2D col)
    {   

          if (col.gameObject.tag == "MovingPlatform")
          {  
              this.transform.parent = col.transform ;          
		      }
         
          
          if(col.gameObject.CompareTag("Monster"))
          {
            if(Hit.collider != null && rigidbody2d.velocity.y <=0f && transform.position.y > col.gameObject.transform.position.y)
            {
               AudioClips[11].Play();
               particles[7].transform.position = new Vector2(col.gameObject.transform.position.x,col.gameObject.transform.position.y);
               particles[7].GetComponent<ParticleSystem>().Play();
               HitLogo.transform.position = new Vector2(transform.position.x,transform.position.y-5f);
               HitLogo.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
               HitLogo.GetComponent<FadeOut>().StartFadeOut();
               HitLogo.GetComponent<Scale>().ScaleStart();
               rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x , 15f);
               col.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
               col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
               Destroy(col.gameObject,1.5f);
			      }
            else if(gameObject.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 1f) && !isProtected)
            {
              if(energy > 0f)
              {
               AudioClips[5].Play();
               Hurt.transform.position = new Vector2(transform.position.x,transform.position.y);
               particles[2].GetComponent<ParticleSystem>().Play();
     
               if(rigidbody2d.velocity.y != 0f)
               {
                 gameObject.GetComponent<Rigidbody2D>().velocity *= -1f;
                 Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                 Hurt.GetComponent<FadeOut>().StartFadeOut();
                 Hurt.GetComponent<Scale>().ScaleStart();
			         }
               else if(rigidbody2d.velocity.y == 0f)
               {
                 if(transform.position.x >= col.gameObject.transform.position.x)
                 {
                   gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(25f,rigidbody2d.velocity.y);
                   Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                   Hurt.GetComponent<FadeOut>().StartFadeOut();
                   Hurt.GetComponent<Scale>().ScaleStart();
				         }
                 else if(transform.position.x < col.gameObject.transform.position.x)
                 {
                   gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-25f,rigidbody2d.velocity.y);
                   Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                   Hurt.GetComponent<FadeOut>().StartFadeOut();
                   Hurt.GetComponent<Scale>().ScaleStart();
				         }
                 
			          }
                StartCoroutine(ColorOfPlayer());
                isDemage1 = true;
                demage = 25f;
                newEnergy = energy - demage;
			        }
            }
          }
 
	  }

  
    void OnCollisionExit2D(Collision2D col)
      {
           this.transform.parent = null;
	    }


    void OnCollisionStay2D(Collision2D col)
    {
       if(col.gameObject.CompareTag("Monster") && gameObject.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 1f) && !isProtected)
       {  
          if(rigidbody2d.velocity.y == 0f)
               { 
                 Hurt.transform.position = new Vector2(transform.position.x,transform.position.y);
                 if(transform.position.x >= col.gameObject.transform.position.x)
                 {
                   gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(25f,rigidbody2d.velocity.y);
                   Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                   Hurt.GetComponent<FadeOut>().StartFadeOut();
                   Hurt.GetComponent<Scale>().ScaleStart();
				         }
                 else if(transform.position.x < col.gameObject.transform.position.x)
                 {
                   gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-25f,rigidbody2d.velocity.y);
                   Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                   Hurt.GetComponent<FadeOut>().StartFadeOut();
                   Hurt.GetComponent<Scale>().ScaleStart();
				         }
                 
			         }
               else 
               {
                  gameObject.GetComponent<Rigidbody2D>().velocity *= -1f;
               }

               AudioClips[5].Play();
               particles[2].GetComponent<ParticleSystem>().Play();
               StartCoroutine(ColorOfPlayer());
               isDemage1 = true;
               demage = 25f;
               newEnergy = energy - demage;
	   }
     else if(col.gameObject.CompareTag("Platform"))
     {
       if(boxcollider2d.bounds.center.y<col.gameObject.GetComponent<Collider2D>().bounds.center.y)
       {
          col.gameObject.GetComponent<Collider2D>().isTrigger = true;
       }
       else
          col.gameObject.GetComponent<Collider2D>().isTrigger = false;
      
     }

    }
    

      void OnTriggerExit2D(Collider2D col)
      {
        if(col.gameObject.CompareTag("Gas"))
        {
          StopCoroutine(coroutine);
          isPlayed2 = false;
        }
        else if(col.gameObject.CompareTag("Platform"))
        {
         
          col.gameObject.GetComponent<Collider2D>().isTrigger = false;
       
        }
         
      }


    void  OnTriggerStay2D(Collider2D col)
    {
       if(col.gameObject.CompareTag("Monster") && gameObject.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 1f) && !isProtected)
       {  
          if(rigidbody2d.velocity.y == 0f)
               { 
                 Hurt.transform.position = new Vector2(transform.position.x,transform.position.y);
                 if(transform.position.x >= col.gameObject.transform.position.x)
                 {
                   gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(25f,rigidbody2d.velocity.y);
                   Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                   Hurt.GetComponent<FadeOut>().StartFadeOut();
                   Hurt.GetComponent<Scale>().ScaleStart();
				 }
                 else if(transform.position.x < col.gameObject.transform.position.x)
                 {
                   gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-25f,rigidbody2d.velocity.y);
                   Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                   Hurt.GetComponent<FadeOut>().StartFadeOut();
                   Hurt.GetComponent<Scale>().ScaleStart();
				 }
                 
			   }

               AudioClips[5].Play();
               particles[2].GetComponent<ParticleSystem>().Play();
               StartCoroutine(ColorOfPlayer());
               isDemage1 = true;
               demage = 25f;
               newEnergy = energy - demage;
	   }
        else if(col.gameObject.CompareTag("Gas") && !isProtected)
         {
            if (energy > 0f)
            {
               particles[3].GetComponent<ParticleSystem>().Play();
               if(isPlayed2 == false)
               {
                 StartCoroutine(coroutine);
                 isPlayed2 = true;
               }

               isDemage5 = true;
               demage = 10f;
               newEnergy = energy - demage;
            }
         }

	}


    void  OnTriggerEnter2D(Collider2D col)
     {
        if (col.gameObject.CompareTag("Powerup") && !hasPowerup)
        {
          particles[8].GetComponent<ParticleSystem>().Play();
          AudioClips[7].Play();
          StartCoroutine(PowerupCountdown());
          StartCoroutine(IconChange2());
          Destroy(col.gameObject);
          Great.transform.position = new Vector2(transform.position.x,transform.position.y+3f);
          Great.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
          Great.GetComponent<FadeOut>().StartFadeOut();
          Great.GetComponent<Scale>().ScaleStart();  
		}
        else  if (col.gameObject.CompareTag("Bullet") && numOfBullet!=maxOfbullet)
        {   
            Great.transform.position = new Vector2(transform.position.x,transform.position.y+3f);
            Great.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            Great.GetComponent<FadeOut>().StartFadeOut();
            Great.GetComponent<Scale>().ScaleStart();
            StartCoroutine(IconChange());
            AudioClips[2].Play();
            if(numOfBullet<=(maxOfbullet-20)){
              numOfBullet += 20;
            }
            else{
              numOfBullet = maxOfbullet;
            }
            Destroy(col.gameObject);
        }
        else if(col.gameObject.CompareTag("Monster"))
        {
          if(Hit.collider != null && rigidbody2d.velocity.y <=0f && transform.position.y > col.gameObject.transform.position.y)
            {
               AudioClips[11].Play();
               particles[7].transform.position = new Vector2(col.gameObject.transform.position.x,col.gameObject.transform.position.y);
               particles[7].GetComponent<ParticleSystem>().Play();
               HitLogo.transform.position = new Vector2(transform.position.x,transform.position.y-5f);
               HitLogo.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
               HitLogo.GetComponent<FadeOut>().StartFadeOut();
               HitLogo.GetComponent<Scale>().ScaleStart();
               rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x , 15f);
               col.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
               col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
               Destroy(col.gameObject,1.5f);
			      }
        }
        else  if((col.gameObject.CompareTag("Monster")||col.gameObject.CompareTag("Enemy")) && gameObject.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 1f) && !isProtected)
        {
            if(energy > 0f)
            {
               AudioClips[5].Play();
               Hurt.transform.position = new Vector2(transform.position.x,transform.position.y);
               particles[2].GetComponent<ParticleSystem>().Play();
               if(rigidbody2d.velocity.y != 0f)
               {
                 gameObject.GetComponent<Rigidbody2D>().velocity *= -1f;
                 Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                 Hurt.GetComponent<FadeOut>().StartFadeOut();
                 Hurt.GetComponent<Scale>().ScaleStart();
			   }
               else if(rigidbody2d.velocity.y == 0f)
               {
                 if(transform.position.x >= col.gameObject.transform.position.x)
                 {
                   gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(25f,rigidbody2d.velocity.y);
                   Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                   Hurt.GetComponent<FadeOut>().StartFadeOut();
                   Hurt.GetComponent<Scale>().ScaleStart();
				 }
                 else if(transform.position.x < col.gameObject.transform.position.x)
                 {
                   gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-25f,rigidbody2d.velocity.y);
                   Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                   Hurt.GetComponent<FadeOut>().StartFadeOut();
                   Hurt.GetComponent<Scale>().ScaleStart();
				 }
                 
			   }
               StartCoroutine(ColorOfPlayer());
               isDemage1 = true;
               demage = 25f;
               newEnergy = energy - demage;
			}
		 }

         else if(col.gameObject.CompareTag("Monster") && isProtected)
         {
          if(Hit.collider != null && rigidbody2d.velocity.y <=0f && transform.position.y > col.gameObject.transform.position.y)
            {
               AudioClips[11].Play();
               particles[7].transform.position = new Vector2(col.gameObject.transform.position.x,col.gameObject.transform.position.y);
               particles[7].GetComponent<ParticleSystem>().Play();
               HitLogo.transform.position = new Vector2(transform.position.x,transform.position.y-5f);
               HitLogo.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
               HitLogo.GetComponent<FadeOut>().StartFadeOut();
               HitLogo.GetComponent<Scale>().ScaleStart();
               rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x , 15f);
               col.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
               col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
               Destroy(col.gameObject,1.5f);
			}
         }

         else if (col.gameObject.CompareTag("Missile") && gameObject.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 1f) && !isProtected)
         {
            if (energy > 0f)
            {  
               Bomb.transform.position = new Vector2(transform.position.x,transform.position.y+2.5f);
               Bomb.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
               Bomb.GetComponent<FadeOut>().StartFadeOut();
               particles[0].transform.position = col.gameObject.transform.position;
               particles[0].GetComponent<ParticleSystem>().Play();
               particles[1].GetComponent<ParticleSystem>().Play();
               AudioClips[6].Play(); 
               gameObject.GetComponent<Rigidbody2D>().velocity *= -1f;
               StartCoroutine(ColorOfPlayer());
               isDemage2 = true;
               demage = 55f;
               newEnergy = energy - demage;
			}
		 }

         else if(col.gameObject.CompareTag("Bomb") && gameObject.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 1f) && !isProtected)
         {
               particles[1].GetComponent<ParticleSystem>().Play();
               particles[5].GetComponent<ParticleSystem>().Play();
               Bomb.transform.position = new Vector2(transform.position.x,transform.position.y+2.5f);
               Bomb.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
               Bomb.GetComponent<FadeOut>().StartFadeOut();
               Bomb.GetComponent<Scale>().ScaleStart();
               AudioClips[5].Play();
               if(transform.position.x >= col.gameObject.transform.position.x)
               {
                 gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(8f,rigidbody2d.velocity.y);
               }
               else if(transform.position.x < col.gameObject.transform.position.x)
               {
                 gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-8f,rigidbody2d.velocity.y);
               }
               StartCoroutine(ColorOfPlayer());
               isDemage4 = true;
               demage = 35f;
               newEnergy = energy - demage;

     }

         else if(col.gameObject.CompareTag("Sting") && gameObject.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 1f) && !isProtected)
         {
            if (energy > 0f)
            {  
               AudioClips[13].Play();
               Hurt.transform.position = new Vector2(transform.position.x,transform.position.y+2.5f);
               particles[2].GetComponent<ParticleSystem>().Play();
               particles[5].GetComponent<ParticleSystem>().Play();
               if(rigidbody2d.velocity.y != 0f)
               {
                 gameObject.GetComponent<Rigidbody2D>().velocity *= -0.7f;
                 Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                 Hurt.GetComponent<FadeOut>().StartFadeOut();
                 Hurt.GetComponent<Scale>().ScaleStart();
			   }
               else if(rigidbody2d.velocity.y == 0f)
               {
                 if(transform.position.x >= col.gameObject.transform.position.x)
                 {
                   gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(15f,rigidbody2d.velocity.y);
                   Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                   Hurt.GetComponent<FadeOut>().StartFadeOut();
                   Hurt.GetComponent<Scale>().ScaleStart();
				 }
                 else if(transform.position.x < col.gameObject.transform.position.x)
                 {
                   gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-15f,rigidbody2d.velocity.y);
                   Hurt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                   Hurt.GetComponent<FadeOut>().StartFadeOut();
                   Hurt.GetComponent<Scale>().ScaleStart();
				 }
                 
			   }
               StartCoroutine(ColorOfPlayer());
               isDemage3 = true;
               demage = 10f;
               newEnergy = energy - demage;
               Destroy(col.gameObject);
			}
     }

         else if(col.gameObject.CompareTag("Gas") && gameObject.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 1f) && !isProtected)
         {
            if (energy > 0f)
            {
               particles[3].GetComponent<ParticleSystem>().Play();
               isDemage5 = true;
               demage = 5f;
               newEnergy = energy - demage;
            }
         }
         else if(col.gameObject.CompareTag("Fire") && gameObject.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 1f) && !isProtected)
         {
            if (energy > 0f)
            { 
               particles[4].GetComponent<ParticleSystem>().Play();
               particles[5].GetComponent<ParticleSystem>().Play();
               gameObject.GetComponent<Rigidbody2D>().velocity *= -0.7f;
               StartCoroutine(ColorOfPlayer());
               isDemage5 = true;
               demage = 5f;
               newEnergy = energy - demage;
               AudioClips[13].Play();
               Destroy(col.gameObject);
      }
     }
          else if(col.gameObject.CompareTag("Projectile") && gameObject.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 1f) && !isProtected)
         {
            if (energy > 0f)
            { 
               particles[4].GetComponent<ParticleSystem>().Play();
               particles[5].GetComponent<ParticleSystem>().Play();
               particles[9].transform.position = new Vector2(col.transform.position.x,col.transform.position.y);
               particles[9].GetComponent<ParticleSystem>().Play();
               gameObject.GetComponent<Rigidbody2D>().velocity *= -0.7f;
               StartCoroutine(ColorOfPlayer());
               isDemage5 = true;
               demage = 5f;
               newEnergy = energy - demage;
               AudioClips[13].Play();
               Destroy(col.gameObject);
      }
     }

         else if (col.gameObject.tag =="Health" &&  energy != healthBar.maxValue )
         {  
            if(energy > 0f)
            {  
              Great.transform.position = new Vector2(transform.position.x,transform.position.y+3f);
              Great.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
              Great.GetComponent<FadeOut>().StartFadeOut();
              Great.GetComponent<Scale>().ScaleStart();
              energy = healthBar.maxValue;
              isDemage1 = false;
              isDemage2 = false;
              isDemage3 = false;
              isDemage4 = false;
              isDemage5 = false;
              AudioClips[7].Play(); 
              StartCoroutine(IconChange());
              Destroy(col.gameObject);
			}
		 }
          else if (col.gameObject.tag =="Protect" && !isProtected)
          {    
               Great.transform.position = new Vector2(transform.position.x,transform.position.y+3f);
               Great.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
               Great.GetComponent<FadeOut>().StartFadeOut();
               Great.GetComponent<Scale>().ScaleStart();
               particles[6].GetComponent<ParticleSystem>().Play();
               AudioClips[7].Play();
               StartCoroutine(ShieldCountdown());
               StartCoroutine(IconChange2());
               Destroy(col.gameObject);
		  }

      }
    

    void Demage(float demage)
     {
          energy = Mathf.Lerp(energy, newEnergy , Time.deltaTime * lerpSpeed);
	 }
    

    IEnumerator IconChange()
    {
      getItem = true;
      yield return new WaitForSeconds(2f);
      getItem = false;
    }

    IEnumerator IconChange2()
    {
      getItem2 = true;
      yield return new WaitForSeconds(2f);
      getItem2 = false;
    }

    IEnumerator IconChange3()
    {
      isAttacking = true;
      yield return new WaitForSeconds(1f);
      isAttacking = false;
    }

    IEnumerator Shooting()
    {
      isShooting = true;
      yield return new WaitForSeconds(0.5f);
      isShooting = false;
    }
    
    IEnumerator Reloading()
    { 
      numOfBullet--;
      AudioClips[3].Play();
      Hole.transform.position = new Vector2(Random.Range(-5f,5f),cam.transform.position.y+Random.Range(-8f,8f));
      Hole.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
      Hole.GetComponent<FadeOut>().StartFadeOut();
      Bang.transform.position = new Vector2(Random.Range(-5f,5f),cam.transform.position.y+Random.Range(-8f,8f));
      Bang.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
      Bang.GetComponent<Scale>().ScaleStart();
      Bang.GetComponent<FadeOut>().StartFadeOut();
      isReloading = true;
      yield return new WaitForSeconds(0.6f);
      isReloading = false;
    }

    IEnumerator  Pollision()
     {
        while(true)
        { 
          particles[3].GetComponent<ParticleSystem>().Play();
          yield return new WaitForSeconds(2.5f);
        }
     }


    IEnumerator  ColorOfPlayer()
     {
      gameObject.GetComponent<SpriteRenderer>().color = new Color(0.3018868f, 0.3018868f, 0.3018868f, 1f);
      isHurt = true;
      yield return  new WaitForSeconds(1.8f);
      gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
      isHurt = false; 
	 }

    IEnumerator  ShieldCountdown()
    {
       isProtected = true;
       yield return  new WaitForSeconds(Random.Range(28,41));
       particles[6].GetComponent<ParticleSystem>().Stop();
       isProtected = false;
	}

    IEnumerator  PowerupCountdown()
     {
       jumpSpeed += hyperJump;
       hasPowerup = true;
       //particles[8].GetComponent<ParticleSystem>().Play();
       yield return  new WaitForSeconds(Random.Range(15,21));
       particles[8].GetComponent<ParticleSystem>().Stop();
       hasPowerup = false;
       jumpSpeed -= hyperJump;
      }

    IEnumerator Gameover()
     {
       enemy = GameObject.FindGameObjectsWithTag("Enemy");
       missile = GameObject.FindGameObjectsWithTag("Missile");
       gameoverLogo.SetActive(true);
       for(int i = 0 ; i < enemy.Length ; i ++)
      {
         Destroy(enemy [i],1f);
      }
      for(int k=0; k<missile.Length ; k++)
      {
         Destroy(missile [k],1f);
	  }
       yield return  new WaitForSeconds(1.5f);
       loadscene.GameOverScene();
	 }

}
