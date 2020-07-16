using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{   
    public Animator animator;
    private GameObject player;
    private PlayerController playerScript;


    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.FindWithTag("Player");
      playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
         if(playerScript.isGround()==false && playerScript.isDead==false)
         {
           if(playerScript.isAttacking==true)
           {
             animator.SetBool("jump",false);
             animator.SetBool("attack",true);
             animator.SetBool("hurt",false);
             animator.SetBool("powerUp1",false);
             animator.SetBool("powerUp2",false);
             animator.SetBool("isDead",false);
           }
           else if(player.GetComponent<SpriteRenderer>().color == new Color(0.3018868f, 0.3018868f, 0.3018868f, 1f))
           {
             animator.SetBool("jump",false);
             animator.SetBool("attack",false);
             animator.SetBool("hurt",true);
             animator.SetBool("powerUp1",false);
             animator.SetBool("powerUp2",false);
             animator.SetBool("isDead",false);
           }
           else if(playerScript.getItem==true)
           {
             animator.SetBool("jump",false);
             animator.SetBool("attack",false);
             animator.SetBool("hurt",false);
             animator.SetBool("powerUp1",false);
             animator.SetBool("powerUp2",true);
             animator.SetBool("isDead",false);
           }
           else if(playerScript.getItem2==true)
           {
             animator.SetBool("jump",false);
             animator.SetBool("attack",false);
             animator.SetBool("hurt",false);
             animator.SetBool("powerUp1",true);
             animator.SetBool("powerUp2",false);
             animator.SetBool("isDead",false);
           }
           else
           {
             animator.SetBool("jump",true);
             animator.SetBool("attack",false);
             animator.SetBool("hurt",false);
             animator.SetBool("powerUp1",false);
             animator.SetBool("powerUp2",false);
             animator.SetBool("isDead",false);
           }  
         }
         else if(playerScript.isGround()==true && playerScript.isDead==false)
         {
           if(playerScript.isAttacking==true)
           {
             animator.SetBool("jump",false);
             animator.SetBool("attack",true);
             animator.SetBool("hurt",false);
             animator.SetBool("powerUp1",false);
             animator.SetBool("powerUp2",false);
             animator.SetBool("isDead",false);
           }
           else if(player.GetComponent<SpriteRenderer>().color == new Color(0.3018868f, 0.3018868f, 0.3018868f, 1f))
           {
             animator.SetBool("jump",false);
             animator.SetBool("attack",false);
             animator.SetBool("hurt",true);
             animator.SetBool("powerUp1",false);
             animator.SetBool("powerUp2",false);
             animator.SetBool("isDead",false);
           }
           else if(playerScript.getItem==true)
           {
             animator.SetBool("jump",false);
             animator.SetBool("attack",false);
             animator.SetBool("hurt",false);
             animator.SetBool("powerUp1",false);
             animator.SetBool("powerUp2",true);
             animator.SetBool("isDead",false);
           }
           else if(playerScript.getItem2==true)
           {
             animator.SetBool("jump",false);
             animator.SetBool("attack",false);
             animator.SetBool("hurt",false);
             animator.SetBool("powerUp1",true);
             animator.SetBool("powerUp2",false);
             animator.SetBool("isDead",false);
           }
           else
           {
             animator.SetBool("jump",false);
             animator.SetBool("attack",false);
             animator.SetBool("hurt",false);
             animator.SetBool("powerUp1",false);
             animator.SetBool("powerUp2",false);
             animator.SetBool("isDead",false);
           }  
         }
         else if(playerScript.isDead==true)
         {
            animator.SetBool("jump",false);
            animator.SetBool("attack",false);
            animator.SetBool("hurt",false);
            animator.SetBool("powerUp1",false);
            animator.SetBool("powerUp2",false);
            animator.SetBool("isDead",true);
         }
             
    }
}
