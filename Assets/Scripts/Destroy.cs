using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private PlayerController player;
    private int rand1,rand2,rand3,rand4;
    private float randx1,randx2,randx3,pos_x,pos_y;
    public AudioSource gameOver;
    public  GameObject cam;
    public  GameObject []  enemy,powerUpOb,platform, otherPlatform,movingPlatform;
    
    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.Find("Player").GetComponent<PlayerController>();
      gameOver =  GetComponent<AudioSource>();
    }

    void Update()
    {
      pos_y = cam.transform.position.y + Random.Range(22.3f,24.5f);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
      if(col.gameObject.tag =="Platform" ||  col.gameObject.tag =="MovingPlatform" )
      {
        Destroy(col.gameObject);

	  }

      else if(col.gameObject.tag == "Powerup" || col.gameObject.tag == "Bullet" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Monster")
      { 
        Destroy(col.gameObject);
	  }
      else if(col.gameObject.tag == "Health" || col.gameObject.tag == "Protect" || col.gameObject.tag == "Robot" || col.gameObject.tag == "Bomb" || col.gameObject.tag == "Gas")
      {
        Destroy(col.gameObject);
	  }
      else if(col.gameObject.tag == "Missile" )
      {
         Destroy(col.gameObject,2f);
	  }
      else if(col.gameObject.tag == "Sting" || col.gameObject.tag == "Fire" ||col.gameObject.tag == "Projectile" || col.gameObject.tag == "Shooting")
      {
         Destroy(col.gameObject);
    }
      else if(col.gameObject.tag == "Player")
      {
         cam.transform.parent = col.transform;
         cam.transform.position = new Vector2(cam.transform.position.x, col.transform.position.y - 2f);
         player.isDead = true;
         gameOver.Play();
	  }
	}

}
