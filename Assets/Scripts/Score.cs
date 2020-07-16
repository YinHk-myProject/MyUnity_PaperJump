using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI yourScore;
    public TextMeshProUGUI bestScore;

   
    // Start is called before the first frame update
    void Start()
    {
        yourScore.text = "Your Score: " + PlayerPrefs.GetFloat("YourScore" ,0);
        bestScore.text = "Best Score: " + PlayerPrefs.GetFloat("BestScore" ,0);
    }

    
}
