using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour,IUnityAdsListener 
{
    string googlePLay_ID = "3606302";
    string placementId = "video";
    bool testMode = false;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Ads service:
        Advertisement.Initialize (googlePLay_ID, testMode);
        Advertisement.AddListener (this);
        StartCoroutine(ShowAds());
    }

    public void DisplayInterstitialAD()
    {
        // Show an ad:
        Advertisement.Show (placementId); 
        AudioListener.pause = true;
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) {
            AudioListener.pause = false;
        } else if (showResult == ShowResult.Skipped) {
            AudioListener.pause = false;
        } else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, show the ad:
        
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    } 


    IEnumerator ShowAds()
    {
      yield return new WaitForSeconds(0.3f);
      DisplayInterstitialAD();
    }
}
