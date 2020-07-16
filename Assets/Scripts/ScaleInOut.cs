using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleInOut : MonoBehaviour
{   
    private Vector3 finalScale,startScale;
    void Start()
    {
      finalScale = transform.localScale;
      startScale = new Vector3 (0.88f,0.88f,1f);
      StartCoroutine(ScaleOverTime());
        
    }

    /*public void ScaleStart()
    {
      StartCoroutine(ScaleOverTime());
    }*/

    IEnumerator ScaleOverTime()
    {  
      while(true)
      {
       for(float i = 0f ; i <= 1f ; i += 0.05f)
      {
       transform.localScale = Vector3.Lerp(startScale, finalScale, i);
       yield return new WaitForSeconds (0.05f);   
      }

       for(float j = 0f ; j <= 1f ; j += 0.05f)
      {
       transform.localScale = Vector3.Lerp(finalScale, startScale, j);
       yield return new WaitForSeconds (0.05f);   
      }
  
      } 
      
    }
}
