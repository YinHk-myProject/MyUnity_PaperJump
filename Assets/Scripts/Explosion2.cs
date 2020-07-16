using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion2 : MonoBehaviour
{
     //public float time;
    public GameObject explode;
    public GameObject bombFire;
    public AudioClip clip;
    private ButtonManager  buttonManager;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    { 
      audioSource = GetComponent<AudioSource>();
      buttonManager = GameObject.FindWithTag("Button Management").GetComponent<ButtonManager>();
      bombFire.GetComponent<ParticleSystem>().Play();
      explode.SetActive(false);
      StartCoroutine(CountdownTimer(Random.Range(6f,10f)));
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
    }
    
    void OnCollisionEnter2D(Collision2D col) 
    {
      if(col.gameObject.tag !="Player" || col.gameObject.tag !="Platform" || col.gameObject.tag !="MovingPlatform")
      {
        Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
      }


      if(col.gameObject.CompareTag("Player"))  
      {
        bombFire.GetComponent<ParticleSystem>().Stop();
        explode.SetActive(true);
        AudioSource.PlayClipAtPoint(clip, new Vector3(transform.position.x,transform.position.y,0f));
        explode.GetComponent<ParticleSystem>().Play(); 
        Destroy(gameObject,0.5f);
      }

    }

    IEnumerator  CountdownTimer(float timer)
	{
	  while(timer > 0)
	  {
	  yield return new WaitForSeconds(1);
	  timer --;
      }
      bombFire.GetComponent<ParticleSystem>().Stop();
      explode.SetActive(true);
      AudioSource.PlayClipAtPoint(clip, new Vector3(transform.position.x,transform.position.y,0f));
      explode.GetComponent<ParticleSystem>().Play(); 
      Destroy(gameObject,0.5f);
	}

}
