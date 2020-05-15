using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{   [SerializeField]
    private AudioClip _clip;
    public UIManager _uiManager;

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private GameObject _enemyDestroyPrefab;

    [SerializeField]
    private GameObject _enemyPrefab;

    private GameManager _gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        //when off the screen, respawn on top with a new x pos in bound
        if (transform.position.y < -6.23f)
        {
            transform.position = new Vector3(Random.Range(-7.86f, 7.86f), 3.73f, 0);
        }

        if(_gamemanager.gameOver == true)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //acces the player
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                Instantiate(_enemyDestroyPrefab, transform.position, Quaternion.identity);
                _uiManager.UpdateScore();
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f); // nem jo sourcal, mert ahogy destroyolodik az obj, a zene is!!! a main camera azért, hogy halljuk is
               
                Destroy(this.gameObject);
               
               
            }
        }

        if(other.tag == "Laser")
        {
            Laser laser = other.GetComponent<Laser>();
            Destroy(laser.gameObject);
            Instantiate(_enemyDestroyPrefab, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
            
        }
    }
}
