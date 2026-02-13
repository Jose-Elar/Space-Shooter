using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _livesSprite;
    [SerializeField] private Sprite[] _scoreSprites;

    [SerializeField] private GameObject _scoreGameOver;

    [SerializeField] private GameObject _restartText;

    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _scoreText.text = "Score: " + 0;
        _livesSprite.sprite = _scoreSprites[3];

        _scoreGameOver.SetActive(false);
        _restartText.SetActive(false);
    }


    public void updateScore(float playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void updateHpSprite(int currentHP)
    {
        switch (currentHP)
        {
            case 0:
                _livesSprite.sprite = _scoreSprites[currentHP];
                _scoreGameOver.SetActive(true);
                _restartText.SetActive(true);
                _gameManager.GameOver();
                StartCoroutine(flickerGameOverText());
                break;
            case 1:
                _livesSprite.sprite = _scoreSprites[currentHP];
                break;
            case 2:
                _livesSprite.sprite = _scoreSprites[currentHP];
                break;
            case 3:
                _livesSprite.sprite = _scoreSprites[currentHP];
                break;
        }
    }

    private IEnumerator flickerGameOverText()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _scoreGameOver.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _scoreGameOver.SetActive(false);
        }
    }
}
