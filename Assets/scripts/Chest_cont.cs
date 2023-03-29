using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_cont : MonoBehaviour
{
    public GameObject get_items;
    public Sprite notopen;
    public Sprite open;
    public int item_code;
    public GameManager manager;

    AudioSource AudioSource;
    SpriteRenderer spriteRenderer;
    


    // Start is called before the first frame update
    void Start(){
        AudioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chestopen() {
        if(spriteRenderer.sprite.name == "chest") {
            AudioSource.Play();
            spriteRenderer.sprite = open;
            get_items.SetActive(true);
            GetComponentInParent<Get_Items>().UI_Code = item_code;
            Time.timeScale = 0;
        }

    }

    public void chestclose() {
        gameObject.SetActive(false);
    }



    private void OnDisable() {
        //아이템 관리
        //Debug.Log("dis");
        switch (item_code) {
            case 0:
                manager.player.GetComponent<main_char>().MaxHP++;
                manager.player.GetComponent<main_char>().PHP++;
                break;

            case 1:
                manager.maxmeds++;
                manager.nowmeds++;
                break;

            case 2:

                break;

        }
    }           
}
