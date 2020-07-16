using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int numberOfPlatforms = 18;
    public GameObject [ ]  paperClip,platform,otherPlatform,movingPlatform,monPref,enemyPref,ostaclePref,items;
    public float minY = 2f,maxY = 3f;
    private Vector2 lastPosition,spwanPosition,standPosition;
    private GameObject cam,lastPlatform;
    private GameObject [ ] instantiatedPlat,enemyExist;
    public GameObject player,pin,calculator;
    private float scoreOfplayer,space,Energy;
    private int rand1,rand2,Life,numOfenemy,numOfmonster,numOfHealth,numOfOstacle;
    private bool isOtherPlatform,isStart,isStart2,isStart3;

    // Start is called before the first frame update
    void Awake()
    { 
      numOfenemy = 0;
      numOfHealth = 0;
      numOfmonster = 0;
      numOfOstacle = 0;
      isStart = false;
      isStart2 = false;
      isStart3 = false;
      int rand = Random.Range(1,3);
      cam = GameObject.FindWithTag("MainCamera");

      //Generate paper clip
      if(rand==1)
      {
       for(int k =0; k < paperClip.Length; k++)
       {
        Instantiate(paperClip [k] ,new Vector2(Random.Range(-13f,13f),cam.transform.position.y-Random.Range(15f,19f)), Quaternion.Euler(0,0,Random.Range(0,360)));
       }
      }

      //Generate calculator
      if(rand==2)
      {
        Instantiate(calculator,new Vector2(Random.Range(-10f,10f),cam.transform.position.y-21f), Quaternion.Euler(0,0,Random.Range(-60,61)));
      }



      //Generate base platform
        Instantiate(platform[9],new Vector2(cam.transform.position.x,cam.transform.position.y-16.8f), Quaternion.identity);

      //Generate pin
        Instantiate(pin,new Vector2(cam.transform.position.x-11.91f,cam.transform.position.y+18.96f), Quaternion.identity);

      //Generate initial platform
      Vector2  spwanPosition = new Vector2( );
      spwanPosition.y = transform.position.y;
      instantiatedPlat = new GameObject [numberOfPlatforms];

      for(int i =0; i < numberOfPlatforms; i++)
      {
           spwanPosition.y -= Random.Range(minY,maxY);
           spwanPosition.x = Random.Range(-8.5f,8.5f);
           instantiatedPlat [i]  = Instantiate(platform [Random.Range(2,7)] ,spwanPosition, Quaternion.identity) as GameObject;
	    }
     
     lastPlatform = instantiatedPlat [0];
     lastPosition = instantiatedPlat [0].transform.position;
     standPosition = instantiatedPlat [Random.Range(numberOfPlatforms-4,numberOfPlatforms)].transform.position;
     player.transform.position = new Vector2( standPosition.x, standPosition.y  + 2f);
     isOtherPlatform = false;
   
    }
   
  

    // Update is called once per frame
    void FixedUpdate()
    {
      lastPosition = lastPlatform.transform.position;

      scoreOfplayer = Mathf.Round(player.GetComponent<PlayerController>().score)*10; 
      Energy = player.GetComponent<PlayerController>().energy;
      

      //StartCoroutine for generating enemys and items
      if( scoreOfplayer > 1500 && isStart == false)
      {
         StartCoroutine(GenerateMonster());
         StartCoroutine(GenerateOtherItems());
         isStart = true;
      }
      else if(scoreOfplayer > 3000 && isStart2 == false)
      {
         StartCoroutine(GenerateOstacle());
         StartCoroutine(GenerateHealth());
         isStart2 = true;
      }
      else if(scoreOfplayer > 3500 && isStart3 == false)
      {
         StartCoroutine(GenerateEnemy());
         isStart3 = true;
      }

     CheckNumOfEnemy();
     CheckNumOfMonster();
     CheckNumOfHealth();
     CheckNumOfOstacle();
    }
    

    //Generate ostacle
     IEnumerator GenerateOstacle()
    {
       while(true)
       {
        yield return  new WaitForSeconds(Random.Range(15,26));
        int rand = Random.Range(0,4);
        int randi = Random.Range(1,3);
        if(rand == 0 && !isOtherPlatform && numOfOstacle<3)
        {
          GameObject ostacle = Instantiate(ostaclePref[Random.Range(0,2)],new Vector2(lastPosition.x+Random.Range(-1.5f,1.5f),lastPosition.y+1f), Quaternion.identity) as GameObject;
          ostacle.transform.parent = lastPlatform.transform;
	     	}
        else if(rand == 1 && numOfOstacle<3)
        { 
          if(randi==1)
          {
            Instantiate(ostaclePref[2],new Vector2(Random.Range(-13f,-11.4f),transform.position.y), Quaternion.identity);
          }
          else
            Instantiate(ostaclePref[3],new Vector2(Random.Range(11.4f,13f),transform.position.y), Quaternion.identity);
        }
        
	     } 
	  }

    //Generate enemy 
    IEnumerator GenerateEnemy()
    {
       while(true)
       {
        yield return  new WaitForSeconds(Random.Range(25,41));
        int rand = Random.Range(0,3); 
        if(rand == Random.Range(0,3) && numOfenemy < 1)
        {
           spwanPosition.x = Random.Range(-9f,9f);
           Instantiate(enemyPref[Random.Range(0,enemyPref.Length)],new Vector2(spwanPosition.x,transform.position.y), Quaternion.identity);
	     	}
        
	     } 
	  }

    //Generate monster
    IEnumerator GenerateMonster()
    {
       while(true)
       {
        yield return  new WaitForSeconds(Random.Range(18,31));
        int rand = Random.Range(1,5); 
        if(rand == 1 && numOfmonster < 2)
        {
           spwanPosition.x = Random.Range(-9f,9f);
           if(GameObject.Find("monster virus(Clone)")==null)
           {
             Instantiate(monPref[Random.Range(0,2)],new Vector2(spwanPosition.x,transform.position.y), Quaternion.identity);
           }
           else 
             Instantiate(monPref[0],new Vector2(spwanPosition.x,transform.position.y), Quaternion.identity);
	     	}
        else if(rand == 3 && numOfmonster < 2)
        {
           if(!isOtherPlatform)
           {
             GameObject monster = Instantiate(monPref[Random.Range(2,4)],new Vector2(lastPosition.x+Random.Range(-1.5f,1.5f),lastPosition.y+2.5f), Quaternion.identity) as GameObject;
             //monster.transform.parent = lastPlatform.transform;
           }
           else
             Instantiate(monPref[0],new Vector2(spwanPosition.x,transform.position.y), Quaternion.identity);
        }
        
	     }
       
	  }

    //Generate health item
    IEnumerator GenerateHealth()
    {
      while(true)
      {
           yield return new WaitForSeconds(Random.Range(8,15));
           int rand = Random.Range(0,3);
           int randi = Random.Range(0,2);
           if(Energy <= 30f  && rand == Random.Range(0,3) && numOfHealth < 2)
           {
              Instantiate(items[0],new Vector2(lastPosition.x + Random.Range(-5f,5f),lastPosition.y + Random.Range(-3f,3f)), Quaternion.identity);
		       }
           else if(Energy < 15f && randi == Random.Range(0,2) && numOfHealth < 2)
           {
              Instantiate(items[0],new Vector2(lastPosition.x + Random.Range(-5f,5f),lastPosition.y + Random.Range(-3f,3f)), Quaternion.identity);
		       }
          

      }

	 }

     //Generate other items
     IEnumerator  GenerateOtherItems()
     {
        while(true)
        {
           yield return new WaitForSeconds(Random.Range(20,36));
           int rand = Random.Range(0,3);
           if(rand == Random.Range(0,3))
           {
              Instantiate(items[Random.Range(0,items.Length)],new Vector2(lastPosition.x + Random.Range(-5f,5f),lastPosition.y + Random.Range(-3f,3f)), Quaternion.identity);
		       }
        }

	   }




    //Check for number of monster in the scene
    int CheckNumOfMonster()
    { 
       numOfmonster = GameObject.FindGameObjectsWithTag("Monster").Length;

       return numOfenemy;
	  }


    //Check for number of enemy in the scene
    int CheckNumOfEnemy()
    { 
       numOfenemy = GameObject.FindGameObjectsWithTag("Enemy").Length;

       return numOfenemy;
	  }

    //Check for number of health in the scene
    int CheckNumOfHealth()
    {
       numOfHealth = GameObject.FindGameObjectsWithTag("Health").Length;

       return numOfHealth;
  	}

    //Check for number of ostacle in the scene
    int CheckNumOfOstacle()
    {
       int numOfGas = GameObject.FindGameObjectsWithTag("Gas").Length;
       int numOfBomb = GameObject.FindGameObjectsWithTag("Bomb").Length;
       int numOfRobot = GameObject.FindGameObjectsWithTag("Robot").Length;
       numOfOstacle = numOfGas+numOfBomb+numOfRobot;
       return numOfOstacle;
  	}


   void OnTriggerEnter2D(Collider2D col)
   {
     if(col.gameObject.tag == "Sting" || col.gameObject.tag == "Shooting")
     {
       Destroy(col.gameObject);
     }
     else if(col.gameObject.CompareTag("Projectile"))
     {
       col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x,-col.gameObject.GetComponent<Rigidbody2D>().velocity.y);
     }

   }




   //Generate platform
   void OnTriggerExit2D(Collider2D col)
   {
     if(col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("MovingPlatform"))
     { 
        int rand = Random.Range(0,14);
        

        if(rand==1)
        {
          //space = Random.Range(4.5f,5.5f);
          SpwanOtherPlaform();
        }
        else if(rand==6)
        {
          SpwanMovingPlatform();
        }
        else
           SpwanPlatform();
       
     }
   }

  //Spwan all kind of platform
   void SpwanPlatform()
   {
      int rand = Random.Range(0,9);
      if(rand==1&&scoreOfplayer > 2000)
      {  
        int randi = Random.Range(0,3);
        if(randi==1&&scoreOfplayer > 3000)
        {
          isOtherPlatform = false;
          lastPlatform =  Instantiate(platform [9] ,new Vector2(0f,transform.position.y), Quaternion.identity) as GameObject;
          if(numOfmonster<2)
          {
            Instantiate(monPref[Random.Range(4,monPref.Length)],new Vector2(lastPlatform.transform.position.x+Random.Range(-1.5f,1.5f),lastPlatform.transform.position.y+2.5f), Quaternion.identity);
          }
        }
        else if(randi!=1||scoreOfplayer < 3000) 
        {
          int random = Random.Range(0,5);
          isOtherPlatform = false;
          lastPlatform =  Instantiate(platform [Random.Range(7,9)] ,new Vector2(Random.Range(-6.5f,6.5f),transform.position.y), Quaternion.identity) as GameObject;
          if(random==0 && numOfmonster<2)
          {
            Instantiate(monPref[Random.Range(2,4)],new Vector2(lastPlatform.transform.position.x+Random.Range(-1.5f,1.5f),lastPlatform.transform.position.y+2.5f), Quaternion.identity);
          }
          else if(random==1)
          {
            GameObject ostacle = Instantiate(ostaclePref[Random.Range(0,2)],new Vector2(lastPlatform.transform.position.x+Random.Range(-1.5f,1.5f),lastPlatform.transform.position.y+1f), Quaternion.identity) as GameObject;
            ostacle.transform.parent = lastPlatform.transform;
          }
        }  
      }
      else if(scoreOfplayer < 2000 || rand!=1)
      {  
         int random = Random.Range(0,9);
         if(random==1)
         {
           isOtherPlatform = false;
           lastPlatform =  Instantiate(platform [Random.Range(0,2)] ,new Vector2(Random.Range(-6.5f,6.5f),transform.position.y), Quaternion.identity) as GameObject;
         }
         else if(random==3||random==5)
         {
           isOtherPlatform = false;
           lastPlatform =  Instantiate(platform [Random.Range(3,7)] ,new Vector2(Random.Range(-8.5f,8.5f),transform.position.y), Quaternion.identity) as GameObject;
         }
         else
         {
           isOtherPlatform = false;
           lastPlatform =  Instantiate(platform [2] ,new Vector2(Random.Range(-6.5f,6.5f),transform.position.y), Quaternion.identity) as GameObject;
         }
           
      }
         
   }

   void SpwanMovingPlatform()
   {        
            isOtherPlatform = true;
            spwanPosition.x = Random.Range(-6.5f,6.5f);
            lastPlatform =  Instantiate(movingPlatform [Random.Range(0,movingPlatform.Length)] ,new Vector2(spwanPosition.x,transform.position.y), Quaternion.identity) as GameObject; 
   }

   void SpwanOtherPlaform()
   {        
            isOtherPlatform = true;
            spwanPosition.x = Random.Range(-7f,7f);
            lastPlatform =  Instantiate(otherPlatform [Random.Range(0,otherPlatform.Length)] ,new Vector2(spwanPosition.x,/*lastPosition.y + space*/transform.position.y), Quaternion.identity) as GameObject; 
   }
}
