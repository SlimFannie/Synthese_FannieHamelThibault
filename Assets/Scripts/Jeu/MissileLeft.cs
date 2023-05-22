using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLeft : MonoBehaviour
{

    [SerializeField] private float _speed = 10f;

    private UIManager _uiManager;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }
    void Update()
    {

        LeftMissile();
        
    }

    private void LeftMissile()
    {
        transform.Translate(Vector3.left * Time.deltaTime * _speed);
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
