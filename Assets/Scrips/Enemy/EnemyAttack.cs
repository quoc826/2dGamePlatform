using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public int attackDamage = 20;

    public Transform attackPoint;
    public float attackRange = 0.5f;

    public LayerMask playerLayers;
    public Animator anim;
    public int isAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        isAttack = Animator.StringToHash("isAttack");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool(isAttack, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // TẮT biến bool "isAttack" trong Animator
            anim.SetBool(isAttack, false);
        }
    }

    public void DealDamage()
    {
 
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        // 2. Gây sát thương cho Player tìm thấy.
        foreach (Collider2D player in hitPlayers)
        {
            IcanTakeDamge damageable = player.GetComponent<IcanTakeDamge>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage, Vector2.zero, gameObject);
            }
        }
    }
                                                                
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
