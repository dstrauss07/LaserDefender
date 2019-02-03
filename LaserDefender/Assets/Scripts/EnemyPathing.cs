using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    WaveConfig waveConfig;
    [SerializeField] List<Transform> waypoints;  //for debugging
    [SerializeField] GameObject path;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        path = waveConfig.GetPathPrefab();
        AddPath();
        transform.position = waypoints[waypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards
                (transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
               // Debug.Log("waypoint reached!" + waypointIndex);
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void AddPath()
    {
        //adding all path children in list pathWaypoints
        for (int i = 0; i < path.transform.childCount; i++)
        {
            waypoints.Add(path.transform.GetChild(i));
        }


    }
}
