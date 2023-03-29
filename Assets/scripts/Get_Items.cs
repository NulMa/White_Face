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




        //������ �ڵ�迭 ���������� �����ϱ���� �ؾ���

        player.GetComponent<main_char>().UI_code = UI_Code;

        switch (UI_Code) {

            case 0:
                image.GetComponent<Image>().sprite = sprite[0];
                name.GetComponent<Text>().text = "�� ����";
                flavorText.GetComponent<Text>().text = "�������� ���� �� ���� \n �ִ� ü�� +1 ����";
                actived_items[UI_Code] = 1;
                Images[UI_Code].color = Color.white;



                //ȹ�� ����
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
                name.GetComponent<Text>().text = "����";
                flavorText.GetComponent<Text>().text = "������ Ʋ������ �θ��� ��. \n �������� �������. \n �Ǿ� �ִ� ���� +1 ����";
                actived_items[UI_Code] = 1;
                Images[UI_Code].color = Color.white;

                //ȹ�� ����
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
                name.GetComponent<Text>().text = "�ణ �콼 �踷��";
                flavorText.GetComponent<Text>().text = "�ణ �콽���ִ� �踷���. \n ���κ��� ���η����� �������ִ�. \n �����ϱ�..? \n ???";
                actived_items[UI_Code] = 2;
                Images[7].color = Color.white;
                manager.key = true;

                //ȹ�� ����
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
