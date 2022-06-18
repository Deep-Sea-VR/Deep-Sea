using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookFlip : MonoBehaviour
{
    [SerializeField]
    private AutoFlip autoFlip;

    [SerializeField]
    private Lids lids;

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
            // 잠들기 전이면 마지막 페이지 남기고 잠들기
            if ((autoFlip.ControledBook.currentPage >= autoFlip.ControledBook.TotalPageCount - 1)
                && !Data.Instance.isFindCrystal)
            {
                lids.Close();
            }
            else
            {
                autoFlip.FlipRightPage();
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))  // X button
        {
            autoFlip.FlipLeftPage();
        }
    }
}
