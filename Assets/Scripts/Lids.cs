using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lids : MonoBehaviour
{
    [SerializeField]
    private Animator lidsAnimator;

    public bool isClosing;

    // Start is called before the first frame update
    void Start()
    {
        if (Data.Instance.isFindCrystal)
        {
            lidsAnimator.SetTrigger("isClose");
            Open();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Data.Instance.isFindCrystal);
    }

    public void Close()
    {
        isClosing = true;
        lidsAnimator.SetTrigger("Close");
    }

    private void Open()
    {
        lidsAnimator.SetTrigger("Open");
    }

    public void MoveScene()
    {
        SceneManager.LoadScene(1);
    }
}
