using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int _points = 100;
    [SerializeField] private GameObject _deathPrefab = default;

    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
    }

    void Update()
    {
        //D�place l'ennemi vers le bas et s'il sort de l'�cran le replace en
        //haut de la sc�ne � une position al�atoire en X
        DeplacementEnnemi();

    }

    private void DeplacementEnnemi()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _uiManager.getVitesseEnnemi());
        if (transform.position.y <= -5f)
        {
            float randomX = Random.Range(-8.17f, 8.17f);
            transform.position = new Vector3(randomX, 8f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            player.Degats();  
            Instantiate(_deathPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject); 
        }
        else if (other.tag == "Bullet")
        {
            Instantiate(_deathPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            _uiManager.AjouterScore(_points);
        }

    }

}
