using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{

    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        StartCoroutine(Fadein());
    }

   IEnumerator  Fadein()
   {
     for(float i = -0.05f ; i <= 1f ; i += 0.05f)
     {
        Color c = rend.material.color;
        c.a  = i;
        rend.material.color = c;
        yield return new WaitForSeconds (0.05f);
	 }
   }
}
