using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Enemy : MonoBehaviour
{
    private Collider2D _collision;


    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _xSpeed = 2f;
    [SerializeField] private GameObject _lazerPrefab;



    private AudioSource _explosionAudio;
    private Animator _animator;
    private Player _playerInfo;

    private float _fireRate = 3.0f;
    private float _canFire = -1.0f;


    void Start()
    {
        _explosionAudio = GetComponent<AudioSource>();
        _collision = GetComponent<Collider2D>();
        _playerInfo = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        movementEnemy();


        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLazer = Instantiate(_lazerPrefab, transform.position, Quaternion.identity);
            Lazer[] lazers = enemyLazer.GetComponentsInChildren<Lazer>();
            foreach (Lazer lazer in lazers)
            {
                lazer.activateEnemyLazers();
            }
        }
    }

    private void movementEnemy()
    {
        transform.Translate((Vector3.down) * _speed * Time.deltaTime + new Vector3(_xSpeed, 0, 0) * Time.deltaTime);


        //Boundaries
        if (transform.position.y <= -5f)
        {
            transform.position = new Vector3(Random.Range(9.4f, -9.4f), 8f, 0f);
            _xSpeed = Random.Range(0, 2) == 0 ? -2 : 2;    //Asigna entre los ultimos dos valores un valor random
        }
        if (transform.position.x >= 9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0f);
            _xSpeed = -_xSpeed;
        }
        if (transform.position.x <= -9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0f);
            _xSpeed = -_xSpeed;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player_Lazer")
        {
            Lazer lazerInfo = other.GetComponent<Lazer>();
            if (lazerInfo != null)
            {
                if (!lazerInfo.isActiveEnemyLazer())
                {
                    _collision.enabled = false;
                    Destroy(other.gameObject);
                    _playerInfo.addScore(10);
                    _animator.SetTrigger("OnEnemyDeath");
                    _explosionAudio.Play();
                    _speed = 0;
                    _xSpeed = 0;
                    Destroy(this.gameObject, 2.8f);
                }
            }

        }
        else if (other.tag == "Player")
        {

            _collision.enabled = false;
            _playerInfo.Damage();
            _animator.SetTrigger("OnEnemyDeath");
            _explosionAudio.Play();
            _speed = 0;
            _xSpeed = 0;
            Destroy(this.gameObject, 2.8f);
        }

    }
}
