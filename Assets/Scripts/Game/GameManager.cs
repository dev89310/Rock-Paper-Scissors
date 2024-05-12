using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static List<GameRule> GameRules = new List<GameRule>
    {
        new GameRule(Item.Rock, "crushes", Item.Scissors),
        new GameRule(Item.Spock, "vaporizes", Item.Rock),
        new GameRule(Item.Paper, "disproves", Item.Spock),
        new GameRule(Item.Lizard, "eats", Item.Paper),
        new GameRule(Item.Scissors, "decapitate", Item.Lizard),
        new GameRule(Item.Spock, "smashes", Item.Scissors),
        new GameRule(Item.Lizard, "poisons", Item.Spock),
        new GameRule(Item.Rock, "crushes", Item.Lizard),
        new GameRule(Item.Paper, "covers", Item.Rock),
        new GameRule(Item.Scissors, "cut", Item.Paper)
    };

    private Item _player;
    private Item _computer;
    public Item Computer => _computer;

    private int _score;
    public int Score => _score;

    private float _timer;
    public float Timer => _timer;
    private Coroutine _timerCoroutine;

    private void OnEnable()
    {
        EventManager.Instance.OnGameStatus += OnGameStatus;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnGameStatus -= OnGameStatus;
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.OnGameStatusInvoke(GameStatus.Home);
    }

    private void OnGameStatus(GameStatus gameStatus)
    {
        switch(gameStatus)
        {
            case GameStatus.Home:
                {
                    _score = 0;
                    StopGameTimer();
                }
                break;

            case GameStatus.Game:
                {
                    _timerCoroutine = StartCoroutine(StartGameTimer());
                }
                break;
        }
    }

    private void OnRoundOver(RoundStatus roundStatus, GameRule gameRule)
    {
        StopGameTimer();

        switch (roundStatus)
        {
            case RoundStatus.Won:
                _score++;
                break;

            case RoundStatus.Lose:
                _score = 0;
                break;
        }

        //Set HighScore
        if (HighScore < _score)
            HighScore = _score;

        if (gameRule == null)
            roundStatus = RoundStatus.Tie;

        EventManager.Instance.OnRoundOverInvoke(roundStatus, gameRule);
    }

    private IEnumerator StartGameTimer()
    {
        // Get all values of the Item enum
        Item[] items = (Item[])Enum.GetValues(typeof(Item));

        // Generate a random index
        int randomIndex = UnityEngine.Random.Range(0, items.Length);

        // Get the random enum value
        _computer = items[randomIndex];

        //Debug.Log($"Computer : {_computer}");

        _timer = Constant.maxTime;
        while(_timer >= 0f)
        {
            _timer -= Time.deltaTime;
            yield return null;
        }
        //Time Over
        OnRoundOver(RoundStatus.Lose, null);
    }

    private void StopGameTimer()
    {
        if(_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }
    }

    public Item Player
    {
        set
        {
            _player = value;
            Decision decision = Decision.Decide(_player, _computer);
            //Debug.Log($"Player : {_player}, Computer : {_computer}");
            OnRoundOver(decision.roundStatus, decision.winningRule);
        }
    }

    public int HighScore
    {
        get { return PlayerPrefs.GetInt("HighScore", 0); }
        set { PlayerPrefs.SetInt("HighScore", value); }
    }
}

