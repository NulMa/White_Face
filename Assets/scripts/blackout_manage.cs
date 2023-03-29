using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blackout_manage : MonoBehaviour{
    //암전 알파값
    public float fadeV = 0;
    public GameObject am;
    public AudioClip soundbox;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        am.GetComponentInChildren<Image>().color = new Color(0, 0, 0, fadeV);
    }



    public void FadeIn(bool a) {
        if (a == true) {
            StartCoroutine("FadeInCort");
        }
        else{
            StopCoroutine("FadeInCort");
        }
    }

    public void FadeOut(bool a) {
        if (a == true) {
            StartCoroutine("FadeOutCort");
        }
        else{
            StopCoroutine("FadeOutCort");
        }
    }

    /*
    public void playSE() {
        audioSource.clip = soundbox;
        audioSource.PlayOneShot(audioSource.clip);
    }
    */





    IEnumerator FadeInCort() {

        while (fadeV < 1.0f) {
            fadeV += 0.02f;

            yield return new WaitForSeconds(0.01f);
        }
        fadeV = 1f;
    }

    IEnumerator FadeOutCort() {

        while (fadeV > 0f) {
            fadeV -= 0.02f;

            yield return new WaitForSeconds(0.015f);
        }
        fadeV = 0f;
    }



}
