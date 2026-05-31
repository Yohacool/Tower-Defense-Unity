using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;

    [HideInInspector]
    public float speedMultiplier = 1f;

    private int waypointIndex = 0;

    void Update()
    {
        if (waypointIndex >= waypoints.Length)
        {
            ReachCastle();
            return;
        }

        Transform target = waypoints[waypointIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * speedMultiplier * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            waypointIndex++;
        }
    }

    void ReachCastle()
    {
        CastleHealth castle =
            FindObjectOfType<CastleHealth>();

        if (castle != null)
        {
            castle.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}