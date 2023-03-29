using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Get_Items : MonoBehaviour
{
    public int UI_Code;

    public GameManager manager;
    public Sprite[] sprite;
    public GameObject player;
    public GameObject canvas;
    public GameObject image;
    public GameObject name;
    public GameObject flavorText;
    public int[] actived_items;
    public Image[] Images;


    string strArr = "";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update(){




        //아이템 코드배열 프리펩으로 저장하기부터 해야함

        player.GetComponent<main_char>().UI_code = UI_Code;

        switch (UI_Code) {

            case 0:
                image.GetComponent<Image>().sprite = sprite[0];
                name.GetComponent<Text>().text = "옥 조각";
                flavorText.GetComponent<Text>().text = "누군가가 놓고간 옥 조각 \n 최대 체력 +1 증가";
                actived_items[UI_Code] = 1;
                Images[UI_Code].color = Color.white;



                //획득 저장
                for (int i = 0; i < actived_items.Length; i++) {
                    strArr = strArr + actived_items[i].ToString();
                    if(i < actived_items.Length - 1) {
                        strArr = strArr + ",";
                    }
                }
                //Debug.Log(strArr);
                PlayerPrefs.SetString("AcItems", strArr);
                PlayerPrefs.Save();



                break;

            case 1:
                image.GetComponent<Image>().sprite = sprite[1];
                name.GetComponent<Text>().text = "망건";
                flavorText.GetComponent<Text>().text = "상투를 틀기위해 두르는 것. \n 말총으로 만들었다. \n 의약 최대 소지 +1 증가";
                actived_items[UI_Code] = 1;
                Images[UI_Code].color = Color.white;

                //획득 저장
                for (int i = 0; i < actived_items.Length; i++) {
                    strArr = strArr + actived_items[i].ToString();
                    if (i < actived_items.Length - 1) {
                        strArr = strArr + ",";
                    }
                }
                //Debug.Log(strArr);
                PlayerPrefs.SetString("AcItems", strArr);
                PlayerPrefs.Save();

                break;

            case 2:
                image.GetComponent<Image>().sprite = sprite[2];
                name.GetComponent<Text>().text = "약간 녹슨 쇠막대";
                flavorText.GetComponent<Text>().text = "약간 녹슬어있는 쇠막대다. \n 끝부분이 구부러지고 구멍이있다. \n 열쇠일까..? \n ???";
                actived_items[UI_Code] = 2;
                Images[7].color = Color.white;
                manager.key = true;

                //획득 저장
                for (int i = 0; i < actived_items.Length; i++) {
                    strArr = strArr + actived_items[i].ToString();
                    if (i < actived_items.Length - 1) {
                        strArr = strArr + ",";
                    }
                }
                //Debug.Log(strArr);
                PlayerPrefs.SetString("AcItems", strArr);
                PlayerPrefs.Save();

                break;

        }



        if (Input.GetButtonDown("Cancel")) {
            if (canvas.activeSelf) {
                canvas.SetActive(false);
                Time.timeScale = 1;
            }

        }

    }

    






}
