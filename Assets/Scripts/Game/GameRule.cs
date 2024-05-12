

public class GameRule
{
    public readonly Item winner;
    public readonly string winningPhrase;
    public readonly Item loser;

    public GameRule(Item winner, string winningPhrase, Item loser)
    {
        this.winner = winner;
        this.winningPhrase = winningPhrase;
        this.loser = loser;
    }
}
