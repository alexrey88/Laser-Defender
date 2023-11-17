using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float baseTimeBetweenShots = 0.5f;
    [SerializeField] float laserSpeed = 5f;
    [SerializeField] float laserLifetime = 5f;

    [Header("AI")] // If the Shooter shoots without player input
    [SerializeField] bool useAI = false;
    [SerializeField] float timeBetweenShotsVariance = 0.2f;
    [SerializeField] float minFireRate = 0.01f;

    [HideInInspector] public bool isFiring;

    Coroutine fireCoroutine;
    AudioPlayer audioPlayer;

    void Start()
    {
        audioPlayer = AudioPlayer.Instance;

        if (audioPlayer == null)
        {
            Debug.LogError("AudioPlayer.Instance not found.");
        }

        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab,
                                            transform.position,
                                            Quaternion.identity);

            Rigidbody2D laserRigidbody = laser.GetComponent<Rigidbody2D>();
            if (laserRigidbody != null)
            {
                laserRigidbody.velocity = transform.up * laserSpeed;
                audioPlayer.PlayShootingClip();
            }

            Destroy(laser, laserLifetime);

            yield return new WaitForSeconds(GetRandomTimeBetweenShots());
        }
    }

    float GetRandomTimeBetweenShots()
    {
        float spawnTime = Random.Range(baseTimeBetweenShots - timeBetweenShotsVariance,
                                        baseTimeBetweenShots + timeBetweenShotsVariance);
        return Mathf.Clamp(spawnTime, minFireRate, float.MaxValue);
    }


    public void DecreaseTimeBetweenShots(float decreaseRatio)
    {
        baseTimeBetweenShots *= decreaseRatio;
    }
}
