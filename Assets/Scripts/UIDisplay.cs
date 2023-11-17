using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] Health playerHealth;

    ScoreKeeper scoreKeeper;
    TextMeshProUGUI scoreText;
    Slider healthSlider;

    void Awake()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
        healthSlider = GetComponentInChildren<Slider>();

        if (scoreText == null || healthSlider == null)
        {
            Debug.LogError("Children components of UI cannot be found.");
        }
    }

    void Start()
    {
        scoreKeeper = ScoreKeeper.Instance;

        if (scoreKeeper == null)
        {
            Debug.Log("ScoreKeeper.Instance; is not found.");
        }

        healthSlider.maxValue = playerHealth.GetHealth();
    }

    void Update()
    {
        UpdateScoreText();
        UpdateHealthSlider();
    }

    void UpdateScoreText()
    {
        scoreText.text = scoreKeeper.GetScore().ToString("00000000000");
    }

    void UpdateHealthSlider()
    {
        healthSlider.value = playerHealth.GetHealth();
    }
}
