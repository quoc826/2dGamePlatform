using UnityEngine;

public class Boss : MonoBehaviour, IcanTakeDamge
{
    [Header("StatusBos")]
    public int currentHeathBoss = 10000;
    public int maxHealthBoss;
    public bool isDead = false;

    private bossAI bossAi;

    [Header("Animation")]
    public Animator anim;
    public int isDeadHash;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        bossAi = GetComponent<bossAI>();
        isDeadHash = Animator.StringToHash("isDead");
        currentHeathBoss = maxHealthBoss;

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void TakeDamage(int damageAmount, Vector2 hitPoint, GameObject hitDirection)
    {
        if (isDead)
            return;

        currentHeathBoss -= damageAmount;

        if(currentHeathBoss <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        bossAi.enabled = false;
        anim.SetTrigger(isDeadHash);
        Destroy(gameObject, 0.5f);

        gameManager.Instance.gameWin();
        
    }
}
