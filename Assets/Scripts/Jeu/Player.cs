using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private GameObject _gunPrefab = default;
    [SerializeField] private GameObject _missileLeftPrefab = default;
    [SerializeField] private GameObject _missileRightPrefab = default;
    [SerializeField] private float _delai = 0.5f;
    [SerializeField] private AudioClip _gunSound = default;
    [SerializeField] private AudioClip _hurtSound = default;
    [SerializeField] private AudioClip _missileSound = default;
    [SerializeField] private AudioClip _endSound = default;
    [SerializeField] private GameObject _mortPrefab = default;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private Animator _anim;

    public LinearIndicator linearIndicator;
    public float minHealth = 0f;
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    private float _vitesseInitiale;
    private float _canFire = -1;
    private float _canFireMissile = -2;

    private void Awake()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        transform.position = new Vector3(0f, -3f, 0f);  // position initiale du joueur

        linearIndicator.SetupIndicator(minHealth, maxHealth);
        linearIndicator.SetValue(currentHealth);
    }

    void Update()
    {
        Move();
        Tir();

        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        linearIndicator.SetValue(currentHealth);
    }

    // Méthode qui gère le tir du joueur ainsi que le délai entre chaque tir
    private void Tir()
    {

        if (Input.GetAxis("Fire1") == 1 && Time.time > _canFire)
        {
            _canFire = Time.time + _delai;

            AudioSource.PlayClipAtPoint(_gunSound, Camera.main.transform.position, 0.3f);

            Instantiate(_gunPrefab, (transform.position + new Vector3(0f, 0.9f, 0f)), Quaternion.identity);
        }
        else if (Input.GetAxis("Fire2") == 1 && Time.time > _canFireMissile)
        {
            _canFireMissile = Time.time + 3;

            AudioSource.PlayClipAtPoint(_missileSound, Camera.main.transform.position, 0.3f);

            Instantiate(_missileLeftPrefab, (transform.position + Vector3.left), Quaternion.identity);
            Instantiate(_missileRightPrefab, (transform.position + Vector3.right), Quaternion.identity);
        }

    }

    // Déplacements et limitation des mouvements du joueur
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(direction * Time.deltaTime * _speed);

        if (horizontalInput < 0)
        {
            _anim.SetBool("Turn_Left", true);
            _anim.SetBool("Turn_Right", false);
        }
        else if (horizontalInput > 0)
        {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", true);
        }
        else
        {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", false);
        }

        if (verticalInput < 0)
        {
            _anim.SetBool("Go_Down", true);
            _anim.SetBool("Go_Up", false);
        }
        else if (verticalInput > 0)
        {
            _anim.SetBool("Go_Down", false);
            _anim.SetBool("Go_Up", true);
        }
        else
        {
            _anim.SetBool("Go_Down", false);
            _anim.SetBool("Go_Up", false);
        }

        //Gérer la zone verticale
        transform.position = new Vector3(transform.position.x,
        Mathf.Clamp(transform.position.y, -3.07f, 2.3f), 0f);

        //Gérer dépassement horizontaux
        if (transform.position.x >= 11.3)
        {
            transform.position = new Vector3(-7.3f, transform.position.y, 0f);
        }
        else if (transform.position.x <= -11.3)
        {
            transform.position = new Vector3(7.3f, transform.position.y, 0f);
        }
    }

    public void Degats()
    {

        currentHealth -= 20;
        _uiManager.EnleverScore(100);
        AudioSource.PlayClipAtPoint(_hurtSound, Camera.main.transform.position);

        if (currentHealth == 0) 
        { 
            _spawnManager.mortJoueur();
            Instantiate(_mortPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_endSound, Camera.main.transform.position, 0.8f);
            Destroy(this.gameObject);

            StartCoroutine(GameOver());
        }      

    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }

    public float getHealth()
    {
        return currentHealth;
    }

}
