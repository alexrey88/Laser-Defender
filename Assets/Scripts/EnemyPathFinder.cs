using System.Collections.Generic;
using UnityEngine;


public class EnemyPathFinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfig currentWave;
    List<Transform> waypoints;
    int waypointIndex = 0;
    Vector3 targetPosition;

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner is not found.");
        }

        currentWave = enemySpawner.GetCurrentWave();
        waypoints = currentWave?.GetWayPoints();
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("Waypoints are not properly set.");
        }

        UpdateTargetPosition();
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        MoveTowardsTargetPosition(targetPosition);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            waypointIndex++;

            if (waypointIndex == waypoints.Count)
            {
                Destroy(gameObject);
                return;
            }

            UpdateTargetPosition();
        }
    }

    void UpdateTargetPosition()
    {
        targetPosition = waypoints[waypointIndex].position;
    }

    void MoveTowardsTargetPosition(Vector3 targetPosition)
    {
        float moveSpeed = currentWave.GetMoveSpeed() * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);
    }
}

