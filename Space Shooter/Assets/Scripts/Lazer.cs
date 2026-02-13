using UnityEngine;

public class Lazer : MonoBehaviour
{

    private float _speed = 15.0f;

    private bool _enemyLazers = false;


    void Update()
    {
        if (!_enemyLazers)
        {
            lazerMovementUP();
        }
        else
        {
            lazerMovementDown();
        }


    }

    private void lazerMovementUP()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 7)
        {
            if (this.transform.parent != null)
            {
                Object.Destroy(this.transform.parent.gameObject);
            }
            else
            {
                Object.Destroy(this.gameObject);
            }

        }
    }

    private void lazerMovementDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5.3f)
        {
            if (this.transform.parent != null)
            {
                Object.Destroy(this.transform.parent.gameObject);
            }
            else
            {
                Object.Destroy(this.gameObject);
            }

        }
    }

    public void activateEnemyLazers()
    {
        _enemyLazers = true;
    }

    public bool isActiveEnemyLazer()
    {
        return _enemyLazers;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _enemyLazers)
        {
            Player player = collision.GetComponent<Player>();
            player.Damage();
            if (this.transform.parent != null)
            {
                Object.Destroy(this.transform.parent.gameObject);
            }
            else
            {
                Object.Destroy(this.gameObject);
            }
        }
    }
}

