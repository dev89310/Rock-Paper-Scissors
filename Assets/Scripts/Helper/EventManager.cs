using System;

public class EventManager : Singleton<EventManager>
{
    //Status of the game - Home, Game, RoundOver
    public event Action<GameStatus> OnGameStatus;
    public void OnGameStatusInvoke(GameStatus gameStatus) => OnGameStatus?.Invoke(gameStatus);

    //Invoked when the current round is finished
    public event Action<RoundStatus, GameRule> OnRoundOver;
    public void OnRoundOverInvoke(RoundStatus roundStatus, GameRule gameRule) => OnRoundOver?.Invoke(roundStatus, gameRule);
}
