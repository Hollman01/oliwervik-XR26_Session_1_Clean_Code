public class ScoreSystem
{
    public int Score { get; private set; }

    public void AddScore(int amount)
    {
        Score += amount;
    }
}
