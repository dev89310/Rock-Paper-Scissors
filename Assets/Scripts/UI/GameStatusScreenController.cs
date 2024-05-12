using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStatusScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _gameStatusPanel;
    [SerializeField] private TextMeshProUGUI _gameStatusText;
    [SerializeField] private Button _goHomeButton;
    [SerializeField] private Button _continueButton;

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
        _goHomeButton.onClick.AddListener(() =>
        {
            EventManager.Instance.OnGameStatusInvoke(GameStatus.Home);
        });

        _continueButton.onClick.AddListener(() =>
        {
            EventManager.Instance.OnGameStatusInvoke(GameStatus.Game);
        });
    }

    private void OnGameStatus(GameStatus gameStatus)
    {
        _gameStatusPanel.SetActive(false);
    }

    private void OnRoundOver(RoundStatus roundStatus, GameRule gameRule)
    {
        switch(roundStatus)
        {
            case RoundStatus.Won:
                _gameStatusText.text = $"You won, {gameRule.winner} {gameRule.winningPhrase} {gameRule.loser}";
                break;

            case RoundStatus.Lose:
                _gameStatusText.text = $"You Lose, {gameRule.winner} {gameRule.winningPhrase} {gameRule.loser}";
                break;

            case RoundStatus.Tie:
                _gameStatusText.text = $"Match Tie";
                break;
        }
        _continueButton.gameObject.SetActive(roundStatus != RoundStatus.Lose);
        _gameStatusPanel.SetActive(true);
    }
}
