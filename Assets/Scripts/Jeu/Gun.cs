using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;

    private UIManager _uiManager;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }
    void Update()
    {

        DeplacementFusilJoueur();

    }

    private void DeplacementFusilJoueur()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
        if (transform.position.y > 8f)
        {
            if (this.transform.parent == null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.transform.parent.gameObject);
            }
        }
    }

}
