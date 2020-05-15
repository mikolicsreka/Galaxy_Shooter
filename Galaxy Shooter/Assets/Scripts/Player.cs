using System.Collections;
using System.Collections.Generic;
using UnityEngine; //MonoBehaviour ebbol jon

public class Player : MonoBehaviour // ':' öröklés (extends)
{

    private AudioSource _audioSource;
   
    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    public bool isShieldOn = false;

    [SerializeField]
    private GameObject _explosion;

    public int HP = 3;

    [SerializeField]
    public bool canTripleShot = false;

    [SerializeField]
    public bool isSpeedBoosted = false;

    [SerializeField]
    private GameObject laserPrefab; //TRANSFORM = GAMEOBJECT

    [SerializeField]
    private GameObject tripleShotPrefab;

    [SerializeField]
    private GameObject[] _engines;
    //firerate = 0.25f
    //canFire -- has the amount of time between fireing passed
    //Time.time
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 6.0f;

    private int hitCount = 0;

    private GameManager _gamemanager;
    private UIManager _uimanager;
    private Spawn_Manager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_uimanager != null)
        {
            _uimanager.UpdateLives(HP);
        }

        _gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        if(_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }

        _audioSource = GetComponent<AudioSource>();

        hitCount = 0;
    }

    // Update is called once per frame 60FPS!!
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }



    }

    public void Damage()
    {
        
        if(isShieldOn == true)
        {
            isShieldOn = false;
            _shieldGameObject.SetActive(false);

        } else if (isShieldOn == false)
        {
            hitCount++;
            if (hitCount == 1)
            {
                //turn left engine failure on
                _engines[0].SetActive(true);
            }
            else if (hitCount == 2)
            {
                //turn right on
                _engines[1].SetActive(true);
            }
            HP--;
            _uimanager.UpdateLives(HP);
        }
        if (isPlayerDead())
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _gamemanager.gameOver = true;
            _uimanager.ShowTitleScreen();
            Destroy(transform.gameObject);
        }
    }
    private bool isPlayerDead()
    {
        if (HP < 1)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void Shoot()
    {

        //is space key is pressed -> spawn laser at player pos
        
            //SPAWN LASER
            if (Time.time > _canFire)
            {
            _audioSource.Play(); //loves hang
                if (canTripleShot == true)
            {
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
               
            } else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
               
            }
            _canFire = Time.time + _fireRate;

        }
            //QUa.. no rotation
        
    }
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if(isSpeedBoosted == true)
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime * 1.5f); // cisarp default vector , deltaTime smooths 1m/s alap 
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime * 1.5f);

        } else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime); // cisarp default vector , deltaTime smooths 1m/s alap 
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

        }

        //if the player on the y is greater than 0
        //set player to y =0

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //if x greater than 9.5 then x = -9.5
        if (transform.position.x > 9.5)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    public void TripleShotPowerUpOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

   public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void SpeedPowerUpOn()
    {
        isSpeedBoosted = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoosted = false;
    }

    public void EnableShields()
    {
        isShieldOn = true;
        _shieldGameObject.SetActive(true);
    }
}
