using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookFlip : MonoBehaviour
{
    [SerializeField]
    private AutoFlip autoFlip;

    [SerializeField]
    private Lids lids;

    [SerializeField]
    private PlayAudio playAudio;

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
            if ((autoFlip.ControledBook.currentPage == autoFlip.ControledBook.TotalPageCount - 2)
            && !Data.Instance.isFindCrystal)
            {
                if (!lids.isClosing)
                {
                    lids.Close();
                    playAudio.FadeOutAudio();
                }
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
