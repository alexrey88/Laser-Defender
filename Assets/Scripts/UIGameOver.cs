using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    ScoreKeeper scoreKeeper;

    void Start()
    {
        scoreKeeper = ScoreKeeper.Instance;

        if (scoreKeeper == null)
        {
            Debug.Log("ScoreKeeper.Instance; is not found.");
        }

        scoreText.text = "You scored: \n" + scoreKeeper.GetScore();
    }
}
