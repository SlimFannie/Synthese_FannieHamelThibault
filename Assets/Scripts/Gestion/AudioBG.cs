using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBG : MonoBehaviour
{

    private void Awake()
    {
        int nbMusique = FindObjectsOfType<AudioBG>().Length;
        if (nbMusique > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
