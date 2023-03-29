using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_Manage : MonoBehaviour {


    //���� ���� ������Ʈ ��ũ��Ʈ


    Animator anim;
    AudioSource audioSource;


    public AudioClip clip;
    public GameObject player;
    public GameManager gameManager;

    public int save_code;
    // 0 = false
    public int isActive = 0;

    

    // Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }


    public void triggerOn() {
        anim.SetBool("fired", true);
        gameManager.respawn_point = transform.position;
        GetComponentInChildren<saveeffect_cont>().effect_on();
        gameManager.GetComponent<GameManager>().GameSave();
       
        //����
        player.GetComponent<main_char>().black = true;

        //ü�� �� �Ҹ�ǰ ����
        player.GetComponent<main_char>().PHP = player.GetComponent<main_char>().MaxHP;
        gameManager.GetComponent<GameManager>().nowmeds = gameManager.GetComponent<GameManager>().maxmeds;

        //�� ������
        gameManager.Enemy_restore();
    }



    // Update is called once per frame
    void Update(){


        if (anim.GetBool("fired") == true && !audioSource.isPlaying) {

            audioSource.clip = clip;
            audioSource.Play();
            isActive = 1; 
        }

        //ȯ���� ��������
        float i = Mathf.Abs(player.transform.position.x - transform.position.x);
        audioSource.volume = (80 - (i * 3))* 0.01f;
        if(audioSource.volume < 0.2) {
            audioSource.Stop();
        }

    }
}
