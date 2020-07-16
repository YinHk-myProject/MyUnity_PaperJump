using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private ButtonManager  buttonManager;
    private Rigidbody2D rb;
    public float cameraSpeed;
    private float point;

    // Start is called before the first frame update
    void Start()
    {
        buttonManager = GameObject.FindWithTag("Button Management").GetComponent<ButtonManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        cameraSpeed = 0.8f;
    }


    void FixedUpdate()
    {
      rb.velocity =  Vector2.up * cameraSpeed;
    }

    // Update is called once per frame
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


      //rb.velocity =  Vector2.up * cameraSpeed * Time.deltaTime;
      point = Mathf.Round(player.GetComponent<PlayerController>().score)*10;

      if(player.transform.position.y - transform.position.y >0)
      {
         transform.position = new Vector2( transform.position.x,player.transform.position.y) ;
	  }

      if(point > 1000 && point <= 20000 )
      { 
          cameraSpeed = 1f;
	  }
      else if(point > 20000 && point <= 30000 )
      { 
          cameraSpeed = 1.2f;
	  }
      else if(point > 30000 && point <= 40000)
      {
          cameraSpeed = 1.4f;
	  }
      else if (point > 40000 && point <= 50000)
      {
          cameraSpeed = 1.5f;
	  }
      else if (point > 50000 && point <= 60000)
      {
          cameraSpeed = 1.8f;
	  }
      else if(point > 70000)
      {
          cameraSpeed = 2f;
	  }
   
    }
}
