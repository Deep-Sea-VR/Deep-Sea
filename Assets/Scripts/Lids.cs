using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lids : MonoBehaviour
{
    [SerializeField]
    private Animator lidsAnimator;

    // Start is called before the first frame update
    void Start()
    {
        if (false)
        {
            lidsAnimator.SetTrigger("isClose");
            Open();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
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
