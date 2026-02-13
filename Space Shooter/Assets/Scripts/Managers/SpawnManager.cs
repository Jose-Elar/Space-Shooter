using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;

    [SerializeField] private GameObject[] powerUps;

    [SerializeField] private GameObject _tripleshotPowerup;
    [SerializeField] private GameObject _speedPowerup;
    [SerializeField] private GameObject _shieldPowerup;

    private bool _stopSpawning = false;

    public void startSpawning()
    {
        StartCoroutine(SpawnEnemyDelay(5.0f));
        StartCoroutine(SpawnPowerups(9.0f));
    }

    private IEnumerator SpawnEnemyDelay(float delay)
    {
        yield return new WaitForSeconds(2f);

        while (!_stopSpawning)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9.4f, 9.4f), 8f, 0f), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(delay);
        }
    }



    private IEnumerator SpawnPowerups(float delay)
    {
        yield return new WaitForSeconds(5f);

        while (!_stopSpawning)
        {

            int random = Random.Range(0, 3);
            Instantiate(powerUps[random], new Vector3(Random.Range(-9.4f, 9.4f), 8f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
    }
    
        public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
