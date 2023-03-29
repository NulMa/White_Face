using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coins : MonoBehaviour
{

    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {

            int random = Random.Range(1, 5);
            gameManager.coins = gameManager.coins + random;
            gameObject.SetActive(false);
            Debug.Log("+1");
        }
    }
}
