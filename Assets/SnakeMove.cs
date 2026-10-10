using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform[] waypoints;
    public float speed = 2f;

    private int currentWaypoint = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        Transform target = waypoints[currentWaypoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            currentWaypoint++;
            
            if (currentWaypoint >= waypoints.Length)
            {
                ReachEnd();
            }
        }
    }

    private void ReachEnd()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnemyReachedBase();
        }

        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.EnemyRemoved();
        }

        Destroy(gameObject);
    }
}
