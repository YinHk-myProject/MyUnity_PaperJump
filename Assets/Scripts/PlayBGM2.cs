using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM2 : MonoBehaviour
{
    public AudioSource bgm;
    private ButtonManager  buttonManager;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine(PlaySound());
      buttonManager = GameObject.FindWithTag("Button Management").GetComponent<ButtonManager>(); 
      player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()   
    {
      if(buttonManager.gamePause == true)
      {
        bgm.Pause();
	  }
      else if(buttonManager.gamePause == false) 
      { 
        bgm.UnPause();
	  }
      else if(player.GetComponent<PlayerController>().energy<=0f)
      {
        bgm.Stop();  
      } 



    }

    IEnumerator PlaySound()
    {
      yield return  new WaitForSeconds(2.5f);
      bgm.Play();
    }
}
