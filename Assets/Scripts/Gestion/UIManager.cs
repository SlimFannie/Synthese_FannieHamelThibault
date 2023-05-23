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
    [SerializeField] private GameObject _pausePanel = default;
    
    [SerializeField] private float _vitesseEnnemi = 1f;
    [SerializeField] private float _augVitesseParNiveau = 1f;

    private Player player;
    private int _score = 0;
    private float currentTime = 0.0f;
    private bool _pauseOn = false;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        _score = 0;
        _pauseOn = false;
        Time.timeScale = 1;
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

        UpdateTime();

        if (player.getHealth() == 0)
        {
            StartCoroutine(GameOver());
        }

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

    public int getScore()
    {
        return _score;
    }

    public float getTime()
    {
        return currentTime;
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

    public void AugmenterVitesse()
    {
        StartCoroutine("VitesseUp");
    }

    IEnumerator VitesseUp()
    {
        yield return new WaitForSeconds(10f);
        _vitesseEnnemi += _augVitesseParNiveau;
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

    IEnumerator GameOver()
    {

        PlayerPrefs.SetInt("Score", getScore());
        PlayerPrefs.SetFloat("Time", getTime());
        PlayerPrefs.Save();

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);

    }

    public float getVitesseEnnemi()
    {
        return _vitesseEnnemi;
    }

}