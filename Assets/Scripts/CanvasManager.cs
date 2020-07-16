using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject canvasScreen;
    public GameObject ok;
    public GameObject cancel;
    public GameObject canvas2;


    // Start is called before the first frame update
    void Start()
    {
        canvasScreen.SetActive(false);
        ok.SetActive(false);
        cancel.SetActive(false);
    }

    public void  Press()
    {
       canvasScreen.SetActive(true);
       ok.SetActive(true);
       cancel.SetActive(true);
       canvas2.SetActive(false);
    }

    public void Cancel()
    {
      canvasScreen.SetActive(false);
      ok.SetActive(false);
      cancel.SetActive(false);
      canvas2.SetActive(true);  
    }
}
