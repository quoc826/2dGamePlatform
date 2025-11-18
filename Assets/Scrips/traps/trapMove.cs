using Unity.Mathematics;
using UnityEngine;

public class trapMove : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform targertPoint;
    public float speed = 5f;
    public float distanceToPoint = 0.1f;

    void Start()
    {
        targertPoint = pointB;
    }

    // Update is called once per frame
    void Update()
    {
        trapMoves();
    }

    private void trapMoves()
    {
        Vector2 direction = (targertPoint.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
        if (math.distance(transform.position, targertPoint.position) < distanceToPoint)
        {
            if (targertPoint == pointA)
            {
                targertPoint = pointB;
            }
            else
            {
                targertPoint = pointA;
            }
        }

    }

    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
