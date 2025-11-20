using System.Collections;
using UnityEngine;



public class bossAttack : MonoBehaviour

{

    private bossAI bossAi;

    [Header("damageAttack")]
    public int damage_1 = 30;
    public int damage_2 = 30;
    public int damage_3 = 50;

    [Header("AttackArea")]
    public Transform pointAttack;
    public float rangeAttack = 20;
    public LayerMask playerLayer;
    public float attackCooldown = 3;
    private float nextAttackTime = 0f;


    [Header("animation")]
    private Animator anim;
    private int isAttack1Hash;
    private int isAttack2Hash;
    private int isAttack3Hash;

    private int isWalkHash;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        bossAi = GetComponent<bossAI>();
        isAttack1Hash = Animator.StringToHash("isAttack1");
        isAttack2Hash = Animator.StringToHash("isAttack2");
        isAttack3Hash = Animator.StringToHash("isAttack3");
        isWalkHash = Animator.StringToHash("isWalk");
    }



    // Update is called once per frame

    void Update()
    {
        if (pointAttack == null)
            return;

        //Collider player nam trong vòng tròn sẽ lưu vào biến hitPlayerCheck
        Collider2D hitPlayerCheck = Physics2D.OverlapCircle(pointAttack.position, rangeAttack, playerLayer);

        if (hitPlayerCheck != null && Time.time >= nextAttackTime)
        {
            if (bossAi.enabled)
            {
                bossAi.enabled = false;
            }
            StartCoroutine(Attack());
        }

        else if (hitPlayerCheck == null)
        {
            anim.SetBool(isAttack1Hash, false);
            anim.SetBool(isAttack2Hash, false);
            anim.SetBool(isAttack3Hash, false);
            bossAi.enabled = true;

            if (!bossAi.enabled) // Chỉ bật nếu chưa bật (tránh gọi SetBool(true) liên tục trong Update)
            {
                bossAi.enabled = true;
                Debug.Log("AI đã được bật lại do Player thoát khỏi phạm vi.");
            }
        }

    }


    private int AttackRandom()
    {
        int randomAttack = Random.Range(0, 4);
        int curretDamage = damage_1;
        // anim.SetBool(isWalkHash, false);
        if (randomAttack == 0)
        {

            anim.SetBool(isAttack1Hash, true);
            curretDamage = damage_2;
        }
        else if (randomAttack == 1)
        {
            anim.SetBool(isAttack2Hash, true);
            anim.SetTrigger(isAttack2Hash);
            curretDamage = damage_2;
        }

        else
        {
            anim.SetBool(isAttack3Hash, true);
            curretDamage = damage_3;
        }

        return curretDamage;
    }


    IEnumerator Attack()
    {

        int damageToDeal = AttackRandom(); // random attack


        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(pointAttack.position, rangeAttack, playerLayer);

        // chờ 4s độ dài animation
        yield return new WaitForSeconds(4.0f);

        // gây sát thương
        foreach (Collider2D playerHit in hitPlayer)
        {
            Debug.Log("Player detected: ");
            IcanTakeDamge damageable = playerHit.GetComponent<IcanTakeDamge>();

            if (damageable != null)
            {
                damageable.TakeDamage(damageToDeal, Vector2.zero, gameObject);
            }
        }

        nextAttackTime = Time.time + attackCooldown;
        bossAi.enabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (pointAttack == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pointAttack.position, rangeAttack);
    }

}







