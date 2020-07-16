using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{
    string googlePLay_ID = "3606302";
    string placementId = "banner";
    bool testMode = true;

    void Start () {
        Advertisement.Initialize (googlePLay_ID, testMode);
        StartCoroutine (ShowBannerWhenReady ());
    }

    void OnDestroy() 
    {
        Advertisement.Banner.Hide(true);
    }
    IEnumerator ShowBannerWhenReady () {
        while (!Advertisement.IsReady (placementId)) {
            yield return new WaitForSeconds (0.5f);
        }
        Advertisement.Banner.SetPosition (BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show (placementId);
    }
}
