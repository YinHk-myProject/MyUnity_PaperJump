using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{    
    private Vector3 startScale,finalScale;
    public float scalex,scaley;
    // Start is called before the first frame update
    void Start()
    {
      finalScale = transform.localScale;
      startScale = new Vector3 (scalex,scaley,1f);
      //StartCoroutine(ScaleOverTime());
        
    }

    public void ScaleStart()
    {
      StartCoroutine(ScaleOverTime());
    }

    IEnumerator ScaleOverTime()
    {   
      for(float i = 0f ; i <= 2f ; i += 0.05f)
      {
       transform.localScale = Vector3.Lerp(startScale, finalScale, i);
       yield return new WaitForSeconds (0.05f);   
      }

    }
}
