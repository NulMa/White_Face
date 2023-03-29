using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_cont : MonoBehaviour
{
    public AudioClip forrest;
    public AudioClip bandit_chief;

    public int sound_code = 0;

    AudioSource audio;

    // Start is called before the first frame update
    void Start(){
        audio = GetComponent<AudioSource>();
        playBGM(0);
    }

    
    public void playBGM(int sound_code) {
       
        float i = 0.4f;
        audio.volume = i;
        

        switch (sound_code) {
            case 0:
                audio.Stop();
                audio.clip = forrest;
                audio.Play();
                break;

            case 1:
                audio.Stop();
                audio.clip = bandit_chief;
                audio.Play();
                break;
        }
    }
    




    
    
    void Update(){

    }
}
