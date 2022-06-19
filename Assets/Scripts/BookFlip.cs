using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookFlip : MonoBehaviour
{
    [SerializeField]
    private AutoFlip autoFlip;

    [SerializeField]
    private Lids lids;

    [SerializeField]
    private PlayAudio playAudio;

    [SerializeField]
    private float endDelayTime;

    [SerializeField]
    private TextMeshProUGUI endText;

    private bool isEnd;

    // Update is called once per frame
    void Update()
    {
        BtnDown();
    }

    void BtnDown()
    {
        if (!isEnd)
        {
            if (OVRInput.GetDown(OVRInput.Button.One))  // A button
            {
                // ���� ���̸� ������ ������ ����� ����
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

                    if (Data.Instance.isFindCrystal && autoFlip.ControledBook.currentPage >= autoFlip.ControledBook.TotalPageCount) // å�� �� ������
                    {
                        isEnd = true;
                        StartCoroutine("GameEndCoroutine"); // ���� ����
                    }
                }
            }
            if (OVRInput.GetDown(OVRInput.Button.Three))  // X button
            {
                autoFlip.FlipLeftPage();
            }
        }
    }

    // ���� ����
    IEnumerator GameEndCoroutine()
    {
        yield return new WaitForSeconds(endDelayTime);

        for (float a = 0; endText.color.a < 1; a += 0.02f)
        {
            endText.color = new Color(255, 255, 255, a);
            yield return null;
        }
    }
}
