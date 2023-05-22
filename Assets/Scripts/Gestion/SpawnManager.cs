using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject _enemyPrefab = default;
    [SerializeField] private GameObject _container = default;

    private bool _stopSpawn = false;

    void Start()
    {
        StartSpawning();
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnCoroutine());
    }

    //Coroutine pour l'apparition des ennemis
    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(1f); // Délai initial
        while (!_stopSpawn)
        {
            Vector3 positionSpawn = new Vector3(Random.Range(-5f, 5f), 6f, 0f);
            GameObject newEnemy = Instantiate(_enemyPrefab, positionSpawn, Quaternion.identity);
            newEnemy.transform.parent = _container.transform;
            yield return new WaitForSeconds(2f);
        }

    }

    public void mortJoueur()
    {
        _stopSpawn = true;
    }

}
