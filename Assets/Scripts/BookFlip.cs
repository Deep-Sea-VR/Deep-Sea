using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookFlip : MonoBehaviour
{
    [SerializeField]
    private AutoFlip autoFlip;

    [SerializeField]
    private Animator lidsAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BtnDown();
    }

    void BtnDown()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))  // A button
        {
            if (!autoFlip.FlipRightPage())  // ������ ���������
            {
                lidsAnimator.SetTrigger("Close");
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))  // X button
        {
            autoFlip.FlipLeftPage();
        }
    }
}
