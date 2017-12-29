using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioSource audioSur;
    public AudioClip[] audioClips;
    public void PlayAudio(string audioName)
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            if (audioName == audioClips[i].name)
                audioSur.PlayOneShot(audioClips[i]);
        }
    }
}
