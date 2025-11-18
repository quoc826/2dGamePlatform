
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform target;
    public float speed = 5f;
    public float nimDistance = 0.1f;

    public Rigidbody2D rb;
    private Animator anim;
    private int isIdle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        target = pointA;
        isIdle = Animator.StringToHash("isIdle");
    }

    // Update is called once per frame
    void Update()
    {
        patrol();
    }



    private void patrol()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;

        if (Vector2.Distance(transform.position, target.position) < nimDistance)
        {
            anim.SetTrigger(isIdle);
            target = target == pointA ? pointB : pointA;

            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        rb.linearVelocity = direction * speed;
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


