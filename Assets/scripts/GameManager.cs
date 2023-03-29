using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
    public GameObject menuSet;
    public GameObject GameOver;
    public GameObject GameOver_text;
    public GameObject player;
    public GameObject status_cover;
    public GameObject HP_bar;
    public GameObject item_manage;
    public GameObject coin_check;
    public GameObject normal_enemy;
    public GameObject boss;
    public GameObject soundmanage;

    public GameObject gameover_sub;
    public GameObject A;
    public GameObject S;


    public GameObject BoxControler;
    public GameObject Shop_Btn;
    public Text TalkBox;
    public Text NameBox;


    public int coins;
    public int maxmeds;
    public int nowmeds;

    public GameObject[] meds;
    public Sprite[] sprite;

    public GameObject[] items;

    float x = 22.3f;
    float y = 9.17f;


    private float curTime;
    public float coolTime = 0.5f;


    public bool key = false;
    public Vector2 respawn_point;
    int i = 100;


    public bool isAction;

    public void Talk(GameObject gameObject) {

        if (isAction) {
            isAction = false;
            gameObject.GetComponent<NPC_manager>().talk_arr_num = 0;
        }
        else {
            isAction = true;

            TalkBox.text = gameObject.GetComponent<NPC_manager>().talk[gameObject.GetComponent<NPC_manager>().talk_arr_num];
            NameBox.text = gameObject.GetComponent<NPC_manager>().NpcName;
        }

        BoxControler.SetActive(isAction);

    }

    public GameObject shopUI;


    //적 리스폰
    public void Enemy_restore() {
        Transform[] normal_list = normal_enemy.GetComponentsInChildren<Transform>();

        for (i = 0; i < normal_list.Length; i++) {
            normal_enemy.transform.GetChild(i).gameObject.GetComponent<enemys>().Hp = normal_enemy.transform.GetChild(i).gameObject.GetComponent<enemys>().HpOrigin;
            normal_enemy.transform.GetChild(i).gameObject.GetComponent<enemys>().stg_point = normal_enemy.transform.GetChild(i).gameObject.GetComponent<enemys>().stg_point_Origin;
            normal_enemy.transform.GetChild(i).gameObject.GetComponent<enemys>().cancounter = false;
            normal_enemy.transform.GetChild(i).gameObject.SetActive(true);
            
        }
    }




    void Start() {
        gameover_sub.gameObject.SetActive(true);
        BoxControler.gameObject.SetActive(false);


        GameLoad();

        nowmeds = maxmeds;
    }

    public void timestart() {
        Time.timeScale = 1;
    }


    void Update() {

        

        

        

        //소모품 사용시 UI
        for (int i = 0; i < maxmeds; i++) {
            meds[i].SetActive(true);
            meds[i].GetComponent<Image>().sprite = sprite[1];
        }

        for (int i = 0; i < nowmeds; i++) {
            meds[i].GetComponent<Image>().sprite = sprite[0];
        }

        coin_check.GetComponent<Text>().text = coins.ToString();
        
        if (curTime <= 0) {
            if (Input.GetButtonDown("Fire5") && nowmeds > 0 && player.GetComponent<main_char>().MaxHP != player.GetComponent<main_char>().PHP) {
                Debug.Log("on");
                player.GetComponent<main_char>().PHP++;
                nowmeds--;
                curTime = coolTime;
                
                
            }
        }
        else {
            //딜레이
            curTime -= Time.deltaTime;
        }
        




        //메뉴창 켜기
        if (Input.GetButtonDown("Cancel")) {
            if (menuSet.activeSelf) {
                menuSet.SetActive(false);
                Time.timeScale = 1;
            }
            else if (!menuSet.activeSelf) {
                menuSet.SetActive(true);
                Time.timeScale = 0;
            }
        }


        //상단  UI
        status_cover.GetComponent<RectTransform>().sizeDelta = new Vector2(player.GetComponent<main_char>().MaxHP * 200, 70);
        HP_bar.GetComponent<RectTransform>().offsetMax = new Vector2(-(player.GetComponent<main_char>().MaxHP - player.GetComponent<main_char>().PHP) * 200, 0);


        //사망 연출
        if(player.GetComponent<main_char>().PHP == 0) {
            GameOver.SetActive(true);
            StartCoroutine("paper");
        }
        else if (player.GetComponent<main_char>().PHP != 0){
            GameOver.SetActive(false);
        }


        if (GameOver.activeSelf && Input.anyKeyDown) {
            //아무 키 입력
            StopCoroutine("paper");
            player.GetComponent<main_char>().PHP = player.GetComponent<main_char>().MaxHP;
            GameOver.SetActive(false);
            GameLoad();
        }



    }


    IEnumerator paper() {

        while (i < 680) {
            i += 10;
            GameOver_text.GetComponent<RectTransform>().sizeDelta = new Vector2(i, 300);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
    }



    public void GameSave() {
        PlayerPrefs.SetFloat("PlayerX", respawn_point.x);
        PlayerPrefs.SetFloat("PlayerY", respawn_point.y);
        PlayerPrefs.SetInt("PlayerMHP", player.GetComponent<main_char>().MaxHP);
        PlayerPrefs.SetInt("PlayerMeds", maxmeds);
        PlayerPrefs.Save();
    }

    public void GameLoad() {

        boss.transform.GetChild(1).transform.localPosition = new Vector3(19, 6.5f, 6);
        boss.transform.GetChild(1).gameObject.SetActive(false);
        boss.transform.GetChild(2).transform.localPosition = new Vector3(70, 6.5f, 6);
        boss.transform.GetChild(2).gameObject.SetActive(false);






        soundmanage.GetComponent<sound_cont>().playBGM(0);


        string[] itemData = PlayerPrefs.GetString("AcItems").Split(',');

        int[] number2 = new int[itemData.Length];

        for(int i = 0; i< itemData.Length; i++) {
            number2[i] = System.Convert.ToInt32(itemData[i]);
        }


        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int hp = PlayerPrefs.GetInt("PlayerMHP");
        maxmeds = PlayerPrefs.GetInt("PlayerMeds");
        //소모품 충전
        nowmeds = maxmeds;
        Time.timeScale = 1;
        player.transform.position = new Vector2(x, y);
        //Debug.Log(hp);
    }


    //게임 종료
    public void GameExit() {
        Application.Quit();
    }


}
