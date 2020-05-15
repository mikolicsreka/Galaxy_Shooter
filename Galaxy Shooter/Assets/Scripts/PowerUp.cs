using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private AudioClip _clip;

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerUpID; // 0 - tripleshot 1 - speed 2 - shields
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if(transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Collided with" + other.name);
        if (other.tag == "Player")
        {
            //acces the player
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                //enable TRIPLEshot if powerupID = 0
                if (powerUpID == 0)
                {
                    player.TripleShotPowerUpOn();
                } else if (powerUpID == 1)
                { //SPEEDBOOST
                    player.SpeedPowerUpOn();
                    
                } else if (powerUpID == 2)
                {//SHIELD
                    player.EnableShields();
                }

               
            }
            //destroy ourselves 
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f); 
            Destroy(this.gameObject);
            

        }

    }
}
