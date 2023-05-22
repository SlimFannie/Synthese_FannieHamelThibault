using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FinDePartie : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private TextMeshProUGUI _txtTime = default;

    // Start is called before the first frame update
    void Start()
    {
        int _score = PlayerPrefs.GetInt("Score");
        float _temps = PlayerPrefs.GetFloat("Time");
        _txtScore.text = _score.ToString() + "pts";
        _txtTime.text = System.TimeSpan.FromSeconds(_temps).ToString("mm':'ss");

    }

}
