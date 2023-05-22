using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionAudio : MonoBehaviour
{

    private AudioSource _audioSource;
    bool isPaused = false;

    // Start is called before the first frame update
    private void Start()
    {

        _audioSource = GameObject.Find("Musique").GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("Muted") == 0)
        {

            _audioSource.Stop();

        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isPaused)
        {

            isPaused = true;
            _audioSource.Pause();
            PlayerPrefs.SetInt("Muted", 0);
            PlayerPrefs.Save();

        } 
        else if (Input.GetKeyDown(KeyCode.Tab) && isPaused)
        {

            isPaused = false;
            _audioSource.Play();
            PlayerPrefs.SetInt("Muted", 1);
            PlayerPrefs.Save();

        }
    }

    public void MusiqueOnOff()
    {

        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {

            _audioSource.Play();
            PlayerPrefs.SetInt("Muted", 1);
            PlayerPrefs.Save();

        }
        else
        {

            _audioSource.Pause();
            PlayerPrefs.SetInt("Muted", 0);
            PlayerPrefs.Save();

        }

    }

}
