using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource childAudio;

    [SerializeField]
    private AudioSource alarmAudio;

    // Start is called before the first frame update
    void Start()
    {
        if (Data.Instance.isFindCrystal)
            alarmAudio.Play();
        else
            childAudio.Play();
    }

    public void FadeOutAudio()
    {
        StartCoroutine("FadeOutCoroutine");
    }

    IEnumerator FadeOutCoroutine()
    {
        while (childAudio.volume > 0)
        {
            childAudio.volume -= Time.deltaTime / 20;
            yield return null;
        }
    }
}
