using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMAnager : MonoBehaviour {
    public GameObject coinPrefab;


    GameObject[] coins;
    GameObject[] targetPool;

    private void Awake() {
        coins = new GameObject[50];

        Generate();
    }


    void Generate() {
        for(int index = 0; index < coins.Length; index++) {
            coins[index] = Instantiate(coinPrefab);
            coins[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type) {
        

        switch (type) {
            case "Coin":
                targetPool = coins;
                break;
        }


        for (int index = 0; index < targetPool.Length; index++) {
            if (!targetPool[index].activeSelf) {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }




}
