using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigation : MonoBehaviour
{

    public GameObject player;
    public GameObject CurrentPoint; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x;
        float playerZ = player.transform.position.z;

        CurrentPoint.transform.position = new Vector3((playerX -100) * 200 / 170, CurrentPoint.transform.position.y, (playerZ - 400) * 200 / 170);
    }
}
