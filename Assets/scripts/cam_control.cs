using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_control : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, -12);
    }
}
