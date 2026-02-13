using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _rotationSpeed = 50.0f;

    
    private SpawnManager _spawnManager;
    private float _xSpeed = 5;


    void Start()
    {
        transform.position = new Vector3(0f, 4f, 0f);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }



    void Update()
    {
        asteroidMovement();

    }


    private void asteroidMovement()
    {
        transform.Translate(Vector3.right * _xSpeed * Time.deltaTime, Space.World);

        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        if (transform.position.x >= 11.0f)
        {
            transform.position = new Vector3(-11.0f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.0f)
        {
            transform.position = new Vector3(11.0f, transform.position.y, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_Lazer")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.startSpawning();

        
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
