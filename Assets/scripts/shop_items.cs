using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class shop_items : MonoBehaviour
{

    public int item_value;
    public int item_code;
    public GameManager manager;
    public GameObject img;

    public AudioClip buy;
    public AudioClip beep;

    AudioSource audioSource;
    Button btn;

    // Start is called before the first frame update
    void Start(){
        btn = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.GetChild(0).GetComponent<Text>().text = item_value.ToString();

    }

    public void Buy() {

        if(manager.coins > item_value) {
            manager.coins = manager.coins - item_value;
            btn.interactable = false;
            gameObject.GetComponentInChildren<Text>().color = new Color(255, 255, 255, 0.5f);
            img.GetComponent<Image>().color = new Color(200, 200, 200, 0.3f);

            switch (item_code) {
                case 0:
                    manager.player.GetComponent<main_char>().MaxHP++;
                    manager.player.GetComponent<main_char>().PHP++;
                    manager.item_manage.GetComponent<Get_Items>().Images[item_code + 2].color = Color.white;
                    break;
                case 1:
                    manager.player.GetComponent<main_char>().MaxHP++;
                    manager.player.GetComponent<main_char>().PHP++;
                    manager.item_manage.GetComponent<Get_Items>().Images[item_code + 2].color = Color.white;
                    break;
                case 2:
                    manager.player.GetComponent<main_char>().coolTime -= 0.2f;
                    manager.item_manage.GetComponent<Get_Items>().Images[item_code + 2].color = Color.white;
                    break;
                case 3:
                    manager.player.GetComponent<main_char>().MaxHP++;
                    manager.player.GetComponent<main_char>().PHP++;
                    manager.item_manage.GetComponent<Get_Items>().Images[item_code + 2].color = Color.white;
                    break;
                case 4:
                    manager.player.GetComponent<main_char>().MaxHP++;
                    manager.player.GetComponent<main_char>().PHP++;
                    manager.item_manage.GetComponent<Get_Items>().Images[item_code + 2].color = Color.white;
                    break;
            }
            audioSource.clip = buy;
            audioSource.Play();
        }
        else {
            audioSource.clip = beep;
            audioSource.Play();
        }
    }
}
