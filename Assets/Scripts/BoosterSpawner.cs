using System.Collections;
using UnityEngine;


public class BoosterSpawner : MonoBehaviour
{
    [SerializeField] GameObject normalBoosterPrefab;
    [SerializeField] GameObject higherBoosterPrefab;
    [SerializeField] float probabilityOfHigherBooster = 0.1f;
    [SerializeField] float boosterDestroyDelay = 10f;
    [SerializeField] float minDelayBetweenBoosters = 20f;
    [SerializeField] float maxDelayBetweenBoosters = 40f;

    PlayerMovement player;
    Vector2 minBounds;
    Vector2 maxBounds;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        if (player == null)
        {
            Debug.LogError("PlayerMovement is not found.");
        }

        InitBounds();
        StartCoroutine(InstantiateBooster());
    }

    void InitBounds()
    {
        minBounds = player.GetPlayerMinBounds();
        maxBounds = player.GetPlayerMaxBounds();
    }

    IEnumerator InstantiateBooster()
    {
        while (true)
        {
            float delay = Random.Range(minDelayBetweenBoosters, maxDelayBetweenBoosters);
            yield return new WaitForSeconds(delay);

            GameObject randomBooster = Instantiate(GetRandomBooster(),
                                                     GetRandomBoosterPosition(),
                                                     Quaternion.Euler(new Vector3(0, 0, 0)));
            Destroy(randomBooster, boosterDestroyDelay);
        }
    }

    Vector3 GetRandomBoosterPosition()
    {
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        return new Vector3(randomX, randomY, 0f);
    }

    GameObject GetRandomBooster()
    {
        if (Random.Range(0f, 1f) < probabilityOfHigherBooster)
        {
            return higherBoosterPrefab;
        }
        return normalBoosterPrefab;
    }
}
