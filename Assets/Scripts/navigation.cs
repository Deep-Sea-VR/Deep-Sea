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

        float pointX = (playerX - 100) / 200 * 150;
        float pointY = (playerZ - 400) / 200 * 150;

        
        CurrentPoint.transform.localPosition = new Vector3(pointX, pointY, CurrentPoint.transform.position.z);
        //CurrentPoint.transform.position = new Vector3(0, 0, CurrentPoint.transform.position.z);
    }
}
