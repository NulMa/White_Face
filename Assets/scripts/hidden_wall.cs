using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class hidden_wall : MonoBehaviour
{
    AudioSource audioSource;
    Tilemap tilemap;
    float i = 1.0f;
    public AudioClip clip;
    
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        tilemap = GetComponent<Tilemap>();

    }
    private void Update() {
        if (audioSource.isPlaying) {
            Invoke("WallFO", 2f);
        }


    }

    void WallFO() {
        tilemap.color = new Color(255, 255, 255, i);
        i = i - 0.005f;
    }


    public void breakwall() {
        audioSource.clip = clip;

        if (!audioSource.isPlaying) {
            
            audioSource.PlayDelayed(0f);
            Destroy(gameObject, 3f);
        }

    }
}
