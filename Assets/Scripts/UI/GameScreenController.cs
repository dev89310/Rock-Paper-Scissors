using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _gameScreenPanel;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [Header("Computer")]
    [SerializeField] private GameObject _computerPanel;
    [SerializeField] private TextMeshProUGUI _computerText;

    [Header("Player")]
    [SerializeField] private Button _rockButton;
    [SerializeField] private Button _paperButton;
    [SerializeField] private Button _scissorsButton;
    [SerializeField] private Button _lizardButton;
    [SerializeField] private Button _spockButton;
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private TextMeshProUGUI _timerText;


    private void OnEnable()
    {
        EventManager.Instance.OnGameStatus += OnGameStatus;
        EventManager.Instance.OnRoundOver += OnRoundOver;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnGameStatus -= OnGameStatus;
        EventManager.Instance.OnRoundOver -= OnRoundOver;
    }

    private void Start()
    {
        _rockButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Player = Item.Rock;
        });

        _paperButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Player = Item.Paper;
        });

        _scissorsButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Player = Item.Scissors;
        });

        _lizardButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Player = Item.Lizard;
        });

        _spockButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Player = Item.Spock;
        });

        _timerSlider.maxValue = Constant.maxTime;
    }

    private void Update()
    {
        if(_timerSlider.gameObject.activeSelf)
        {
            _timerSlider.value = GameManager.Instance.Timer;
            _timerText.text = $"Timer : {_timerSlider.value:F1}";
        }
    }

    private void OnGameStatus(GameStatus gameStatus)
    {
        switch (gameStatus)
        {
            case GameStatus.Home:
                {
                    _gameScreenPanel.SetActive(false);
                    _computerPanel.SetActive(false);
                    _timerSlider.gameObject.SetActive(false);
                }
                break;

            case GameStatus.Game:
                {
                    _gameScreenPanel.SetActive(true);
                    _computerPanel.SetActive(false);
                    _timerSlider.gameObject.SetActive(true);
                    _scoreText.text = $"Score : {GameManager.Instance.Score}";
                }
                break;
        }
    }

    private void OnRoundOver(RoundStatus roundStatus, GameRule gameRule)
    {
        _computerPanel.SetActive(true);
        _computerText.text = GameManager.Instance.Computer.ToString();

        _timerSlider.gameObject.SetActive(false);
    }
}
