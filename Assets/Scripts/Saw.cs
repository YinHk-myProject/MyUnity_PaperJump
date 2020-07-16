using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public AudioSource AudioClip;
    private ButtonManager  buttonManager;

    // Start is called before the first frame update
    void Start()
    {
       buttonManager = GameObject.FindWithTag("Button Management").GetComponent<ButtonManager>();
       AudioClip.Play(); 
    }

     void Update()
    {
       if(buttonManager.gamePause == true)
      {
          AudioClip.Pause();
	  }
      else if(buttonManager.gamePause == false) 
       {
           AudioClip.UnPause();
	   }
     
    }

    
}
