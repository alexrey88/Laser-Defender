using UnityEngine;

public class HealthUp : MonoBehaviour
{
    [SerializeField] int HealthBoost = 5;

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = AudioPlayer.Instance;

        if (audioPlayer == null)
        {
            Debug.LogError("AudioPlayer.Instance is null.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.PlayerTag))
        {
            Health playerHealth = other.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.IncreaseHealth(HealthBoost);
                audioPlayer.PlayBoostClip();
            }

            Destroy(gameObject);
        }
    }
}
