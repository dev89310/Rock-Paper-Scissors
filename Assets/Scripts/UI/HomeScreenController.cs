using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _homeScreenPanel;
    [SerializeField] private Button _playButton;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;


    private void OnEnable()
    {
        EventManager.Instance.OnGameStatus += OnGameStatus;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnGameStatus -= OnGameStatus;
    }

    private void Start()
    {
        _playButton.onClick.AddListener(() =>
        {
            EventManager.Instance.OnGameStatusInvoke(GameStatus.Game);
        });
    }

    private void OnGameStatus(GameStatus gameStatus)
    {
        switch (gameStatus)
        {
            case GameStatus.Home:
                {
                    _homeScreenPanel.SetActive(true);
                    _scoreText.text = $"Score : 0";
                    _highScoreText.text = $"HighScore : {GameManager.Instance.HighScore}";
                }
                break;

            case GameStatus.Game:
                {
                    _homeScreenPanel.SetActive(false);
                }
                break;
        }
    }
}
