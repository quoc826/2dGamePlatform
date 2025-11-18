using UnityEngine;

public class Enemy : MonoBehaviour, IcanTakeDamge
{
    [Header("enemy setting")]
    public int maxHealth = 100;
    public int currentHealth;
    public bool isDead = false;

    private Rigidbody2D rb;
    public float nextAttackTime;
    public float attackRange = 1f;
    public int damageEnemy = 10;

    public float timeDestroy = 10f;

    private EnemyAI enemyAI;

    public Animator anim;
    public int dieAnimation;

    [SerializeField] HealthBarEnemy healthBarEnemy;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBarEnemy.updateHealthBar(maxHealth, currentHealth);
        enemyAI = GetComponent<EnemyAI>();
        anim = GetComponentInChildren<Animator>();
        dieAnimation = Animator.StringToHash("isDead");
    }

    public void Die()
    {
        isDead = true;
        enemyAI.enabled = false;
        anim.SetTrigger(dieAnimation);
        Destroy(gameObject, timeDestroy);
    }


    public void TakeDamage(int damageAmount, Vector2 hitPoint, GameObject hitDirection)
    {
        if (isDead)
            return;
        currentHealth -= damageAmount;
        healthBarEnemy.updateHealthBar(maxHealth, currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
            return;
        if (collision.CompareTag("Player"))
        {
            if(collision.GetComponent<Player>().IsDead() == false)
            {
                if(Time.time > nextAttackTime)
                {
                    nextAttackTime = Time.time + attackRange;
                    IcanTakeDamge damageable = collision.GetComponent<IcanTakeDamge>();
                    if (damageable != null)
                    {
                        damageable.TakeDamage(damageEnemy, transform.position, gameObject);
                    }
                }
            }
        }
    }
}