using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb_con : MonoBehaviour
{

    public GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
    }

    public void fill_med() {
            gameManager.nowmeds++;
            gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable() {
        
    }

}
