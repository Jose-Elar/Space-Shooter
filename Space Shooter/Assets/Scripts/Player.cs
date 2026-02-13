using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    private BoxCollider2D _collision;

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _speedMultiplier = 2;

    private float _horizontalInput;
    private float _verticalInput;

    [SerializeField] private GameObject _lazerprefab;
    [SerializeField] private GameObject _tripleLazerPrefab;
    [SerializeField] private GameObject _shields;

    [SerializeField] private GameObject _rightEngine;
    [SerializeField] private GameObject _leftEngine;
    [SerializeField] private GameObject _thrusterEngine;
    private AudioSource _lazerSound;


    private float _timer = 1.0f;

    [SerializeField] private int _lives = 3;
    [SerializeField] private float _score = 0f;

    private SpawnManager _spawnManager;
    private UIManager _UIManager;

    private bool _tripleLazerActive = false;
    private bool _speedBoostActive = false;
    private bool _setShieldActive = false;




    public void setTriplePowerUp(bool estado)
    {
        _tripleLazerActive = estado;
        StartCoroutine(disableTripleShoot());
    }

    public void setSpeedBoost(bool estado)
    {
        _speedBoostActive = estado;
        _speed = _speed * _speedMultiplier;
        StartCoroutine(disableSpeedBoost());
    }

    public void setShield(bool estado)
    {
        _setShieldActive = estado;
        _shields.SetActive(true);
        StartCoroutine(disableShield());
    }

    void Start()
    {
        _collision = GetComponent<BoxCollider2D>();

        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _lazerSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        movimientoPersonaje();
        //Sistema de Timing de CD de disparo
        if ((Input.GetKeyDown(KeyCode.Space)) && (_timer <= 0))
        {
            spawnLazer(_timer);
            _timer = 0.2f;
        }
        else
        {
            if (_timer >= 0.0f)
            {
                _timer -= Time.deltaTime;
            }
        }

    }

    private void movimientoPersonaje()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        transform.Translate(((Vector3.right * _horizontalInput) + (Vector3.up * _verticalInput)) * _speed * Time.deltaTime);


        //PLAYERS BOUNDS
        if (transform.position.y >= 5.5f)
        {
            transform.position = new Vector3(transform.position.x, 5.5f, 0);
        }
        else if (transform.position.y <= -3.3f)
        {
            transform.position = new Vector3(transform.position.x, -3.3f, 0);
        }

        if (transform.position.x >= 11.0f)
        {
            transform.position = new Vector3(-11.0f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.0f)
        {
            transform.position = new Vector3(11.0f, transform.position.y, 0);
        }
    }

    private void spawnLazer(float _timer)
    {
        if (_tripleLazerActive)
        {
            Instantiate(_tripleLazerPrefab, transform.position + new Vector3(-0.07f, 1.8f, 0f), Quaternion.identity);
        }
        else
        {
            Instantiate(_lazerprefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);

        }
        _lazerSound.Play();
    }

    public void Damage()
    {
        if (_setShieldActive)
        {
            _setShieldActive = false;
            _shields.SetActive(false);
            return;
        }

        _lives--;
        _UIManager.updateHpSprite(_lives);

        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }

        if (_lives < 1)
        {

            _collision.enabled = false;
            _spawnManager.onPlayerDeath();

            _rightEngine.SetActive(false);
            _leftEngine.SetActive(false);
            _thrusterEngine.SetActive(false);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //this.gameObject.SetActive(false);
        }
    }

    public void addScore(int value)
    {
        _score += value;
        _UIManager.updateScore(_score);
    }

    private IEnumerator disableTripleShoot()
    {
        yield return new WaitForSeconds(5);
        _tripleLazerActive = false;
    }

    private IEnumerator disableSpeedBoost()
    {
        yield return new WaitForSeconds(5);
        _speedBoostActive = false;
        _speed = _speed / _speedMultiplier;
    }

    private IEnumerator disableShield()
    {
        yield return new WaitForSeconds(5);
        if (_setShieldActive)
        {
            _setShieldActive = false;
            _shields.SetActive(false);
        }
    }

}
