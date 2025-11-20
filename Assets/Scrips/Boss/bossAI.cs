using System.Collections;
using UnityEngine;

public class bossAI : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    private bool isFacingRight = true;



    [Header("Attack Settings")]

    public float rangeAttack = 1.5f;


    [Header("Dependencies")]
    public Transform player;
    public LayerMask playerLayer;


    [Header("animation")]
    public Animator anim;
    private int isWalkHash;
    private int isAttack1Hash;
    private int isAttack2Hash;
    private int isAttack3Hash;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        isAttack1Hash = Animator.StringToHash("isAttack1");
        isAttack2Hash = Animator.StringToHash("isAttack2");
        isAttack3Hash = Animator.StringToHash("isAttack3");

        isWalkHash = Animator.StringToHash("isWalk");


        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    void Update()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= rangeAttack)
        {
            anim.SetBool(isWalkHash, false);
            CheckFlip();
        }

        else if (distanceToPlayer < detectionRange)
        {
            anim.SetBool(isWalkHash, true);

            //duy chuyen mot diem tu vi tri hien tai den vi tri cua player
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
            CheckFlip();
        }
        else
        {
            anim.SetBool(isWalkHash, false);
        }
    }

    void CheckFlip()
    {
        float directionX = player.position.x - transform.position.x;

        if (directionX < 0 && !isFacingRight)
        {
            Flip();
        }
        else if (directionX > 0 && isFacingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}