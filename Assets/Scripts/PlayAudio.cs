using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource clip;

    public void PlayAudioSource()
    {
       clip.Play();
	}
}
