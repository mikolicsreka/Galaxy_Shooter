using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;

    [SerializeField]
    private GameObject[] powerUps;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }
    //create coroutine spawn enemy every 5 secs
    IEnumerator EnemySpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
           
            Vector3 pos = new Vector3(Random.Range(-7.86f, 7.86f), 3.73f, 0); 
            Instantiate(enemyShipPrefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }

    }

    IEnumerator PowerUpSpawnRoutine()
    {
        while(_gameManager.gameOver == false)
        {
            int randomPowerUp = Random.RandomRange(0, 3);
            Instantiate(powerUps[randomPowerUp], new Vector3(Random.Range(-7.86f, 7.86f), 7.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
        
    }
}
