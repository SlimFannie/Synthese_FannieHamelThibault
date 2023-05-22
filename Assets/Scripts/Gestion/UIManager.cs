using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private TextMeshProUGUI _txtTime = default;
    [SerializeField] private Image _livesDisplayImage = default;
    [SerializeField] private Sprite[] _liveSprites = default;
    [SerializeField] private GameObject _pausePanel = default;

    [SerializeField] private float _vitesseEnnemi = 1f;
    [SerializeField] private float _augVitesseParNiveau = 1f;
    [SerializeField] private int _pointageAugmentation = 500;

    private int _score = 0;
    private float currentTime = 0.0f;
    private bool _estChanger = false;
    private bool _pauseOn = false;

    private void Start()
    {
        _score = 0;
        _pauseOn = false;
        Time.timeScale = 1;
        ChangeLivesDisplayImage(3);
        UpdateScore();
    }

    private void Update()
    {

        // Permet la gestion du panneau de pause (marche/arrêt)
        if ((Input.GetKeyDown(KeyCode.Escape) && !_pauseOn))
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
            _pauseOn = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) && _pauseOn))
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
            _pauseOn = false;
        }

        if (_score % _pointageAugmentation == 0 && _score != 0 && _estChanger == false)
        {
            AugmentVitesseEnnemi();
            _estChanger = true;
        }
        else if (_score % _pointageAugmentation != 0)
        {
            _estChanger = false;
        }

        UpdateTime();

    }

    // Méthode qui change le pointage sur le UI
    private void UpdateScore()
    {
        _txtScore.text = _score.ToString() + " pts";
    }

    //Méthode qui change le temps sur le UI

    private void UpdateTime()
    {
        currentTime += Time.deltaTime;

        _txtTime.text = System.TimeSpan.FromSeconds(currentTime).ToString("mm':'ss");
    }

    private void AugmentVitesseEnnemi()
    {
        _vitesseEnnemi += _augVitesseParNiveau;
    }

    public int getScore()
    {
        return _score;
    }

    public void AjouterScore(int points)
    {
        _score += points;
        UpdateScore();
    }

    public void EnleverScore(int points)
    {
        _score -= points;
        UpdateScore();
    }

    public void ChangeLivesDisplayImage(int noImage)
    {
        if (noImage < 0)
        {
            noImage = 0;
        }
        _livesDisplayImage.sprite = _liveSprites[noImage];

        // Si le joueur n'a plus de vie on lance la séquence de fin de partie
        if (noImage == 0)
        {
            PlayerPrefs.SetInt("Score", _score);
            PlayerPrefs.SetFloat("Time", currentTime);
            PlayerPrefs.Save();
            StartCoroutine("FinPartie");
        }
    }

    IEnumerator FinPartie()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }

    public void ResumeGame()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        _pauseOn = false;
    }

    public void ChargerDepart()
    {
        SceneManager.LoadScene(0);
    }

    public float getVitesseEnnemi()
    {
        return _vitesseEnnemi;
    }

}