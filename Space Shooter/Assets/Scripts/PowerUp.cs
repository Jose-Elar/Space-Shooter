using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private float _speed = 3.0f;
    private float _xMovement = 3.0f;
    private Player _playerInfo;

    [SerializeField] private float _powerUpID; //1= TripleShot 2=SpeedUp 3=Shields
    [SerializeField] private AudioClip _pickUpSound;

    void Start()
    {
        _playerInfo = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        movementPowerUp();
    }

    void movementPowerUp()
    {
        transform.Translate((Vector3.down) * _speed * Time.deltaTime + new Vector3(_xMovement, 0, 0) * Time.deltaTime);

        if (transform.position.y <= -6.2f)
        {
            GameObject.Destroy(this.gameObject);
        }
        if (transform.position.x >= 9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0f);
            _xMovement = -_xMovement;
        }
        if (transform.position.x <= -9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0f);
            _xMovement = -_xMovement;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (_playerInfo != null)
            {
                AudioSource.PlayClipAtPoint(_pickUpSound, transform.position);
                switch (_powerUpID)
                {
                    case 1:
                        _playerInfo.setTriplePowerUp(true);
                        GameObject.Destroy(this.gameObject);
                        break;
                    case 2:
                        _playerInfo.setSpeedBoost(true);
                        GameObject.Destroy(this.gameObject);
                        break;
                    case 3:
                        _playerInfo.setShield(true);
                        GameObject.Destroy(this.gameObject);
                        break;
                }
            }


        }
    }

}
