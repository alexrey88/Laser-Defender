using UnityEngine;

public class ScoreKeeper : Singleton<ScoreKeeper>
{
    int scoreValue = 0;

    public void ModifyScore(int value)
    {
        scoreValue += value;
        Mathf.Clamp(scoreValue, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        scoreValue = 0;
    }

    public int GetScore()
    {
        return scoreValue;
    }
}
