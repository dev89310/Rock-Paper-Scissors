using System.Linq;

public class Decision
{
    public RoundStatus roundStatus;
    public GameRule winningRule;

    private Decision(RoundStatus roundStatus, GameRule winningRule)
    {
        this.roundStatus = roundStatus;
        this.winningRule = winningRule;
    }

    public static Decision Decide(Item player, Item opponent)
    {
        var rule = FindWinningRule(player, opponent);
        if (rule != null)
            return new Decision(RoundStatus.Won, rule);

        rule = FindWinningRule(opponent, player);
        if (rule != null)
            return new Decision(RoundStatus.Lose, rule);

        return new Decision(RoundStatus.Tie, null);
    }

    private static GameRule FindWinningRule(Item player, Item opponent)
    {
        return GameManager.GameRules.FirstOrDefault(r => r.winner == player && r.loser == opponent);
    }
}
