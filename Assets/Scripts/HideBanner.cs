using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class HideBanner : MonoBehaviour
{
    string googlePLay_ID = "3606302";
    string placementId = "banner";

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Banner.Hide (true); 
    }

   
}
