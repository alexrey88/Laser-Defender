using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    [SerializeField] int destroyScoreValue;
    [SerializeField] bool isPlayer;

    ScreenShake screenShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    int maxHealth;

    void Awake()
    {
        screenShake = Camera.main?.GetComponent<ScreenShake>();
    }

    void Start()
    {
        audioPlayer = AudioPlayer.Instance;
        if (audioPlayer == null) { Debug.LogError("AudioPlayer.Instance not found."); }

        scoreKeeper = ScoreKeeper.Instance;
        if (scoreKeeper == null) { Debug.LogError("ScoreKeeper.Instance not found."); }

        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager == null) { Debug.LogError("LevelManager not found."); }

        maxHealth = health;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            audioPlayer.PlayDamageClip();
            PlayHitEffect();
            damageDealer.Hit();
            ShakeCamera();
        }
    }

    void ShakeCamera()
    {
        if (screenShake != null && applyCameraShake)
        {
            screenShake.Play();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        scoreKeeper.ModifyScore(destroyScoreValue);
        if (isPlayer)
        {
            levelManager.LoadGameOver();
        }
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void IncreaseHealth(int increase)
    {
        health = Mathf.Clamp(health + increase, 0, maxHealth);
    }
}
