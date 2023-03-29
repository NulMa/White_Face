using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_manage : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //게임 종료
    public void GameExit() {
        Application.Quit();
    }

    public void NewGame() {
        SceneManager.LoadScene("SampleScene");
    }
}
