using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        
    }

    public void StartFadeOut()
    {
        StartCoroutine(Fade());
	}

   IEnumerator  Fade()
   {
     for(float i = 1f ; i >= -0.05f ; i -= 0.05f)
     {
        Color c = rend.material.color;
        c.a  = i;
        rend.material.color = c;
        yield return new WaitForSeconds (0.05f);
	 }
   }

}
