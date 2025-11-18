using UnityEngine;

public class AiBoss : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float detectionRange = 10f;
    private bool isFacingRight = true;

    [Header("BossAttack")]
    public Transform player;
    public float attackRange = 1.5f; // Khoảng cách để kích hoạt tấn công
    public float attackCooldown = 2.0f; // Thời gian hồi chiêu giữa các đòn đánh
    private float nextAttackTime;

    private int attackState = 1; // 1: Skill 1, 2: Skill 2, 3: Skill Thunder
    private int currentAttackCount = 0; // Biến đếm để chuyển skill

    public Transform attackPoint;
    public LayerMask EnemyLayers; // Layer của đối tượng nhận sát thương (Player)

    [Header("Skill Damage")]
    private int skill_1 = 50;
    private int skill_2 = 60;
    private int skill_thunder = 70;

    [Header("Invoke Timing (Phải khớp với Animation)")]
    // Thời gian gây sát thương sau khi gọi ChooseAndAttack()
    public float skill1_damageDelay = 0.3f; 
    // Tổng thời gian animation, sau đó Boss mới được phép hành động tiếp
    public float skill1_resetTime = 0.8f;   

    public float skill2_damageDelay = 0.4f;
    public float skill2_resetTime = 0.9f;

    public float skillThunder_damageDelay = 0.7f;
    public float skillThunder_resetTime = 1.5f;


    [Header("Animation")]
    public Animator anim;
    // Sử dụng int hash để tối ưu
    private int isWalkHash;
    private int isIdleHash;
    private int isAttack1Hash;
    private int isAttack2Hash;
    private int isThunderAttackHash;

    // Biến để kiểm soát trạng thái
    private bool isAttacking = false; // Cờ ngăn di chuyển khi đang tấn công
    private int currentDamage = 0; // Biến lưu trữ sát thương của đòn tấn công hiện tại


    void Start()
    {
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }

        // Khởi tạo các Hash của Animator
        isWalkHash = Animator.StringToHash("isWalk");
        isIdleHash = Animator.StringToHash("isIdle");
        isAttack1Hash = Animator.StringToHash("isAttack1");
        isAttack2Hash = Animator.StringToHash("isAttack2");
        isThunderAttackHash = Animator.StringToHash("isThunderAttack");

        // Tìm Player nếu chưa được gán
        if (player == null)
        {
             GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
             if (playerObject != null)
             {
                 player = playerObject.transform;
             }
        }

        nextAttackTime = Time.time;
    }

    void Update()
    {
    
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Ngăn chặn mọi hành động di chuyển/tấn công mới nếu đang trong animation tấn công
        if (isAttacking)
        {
            CheckFlip();
            anim.SetBool(isWalkHash, false);
            return;
        }

        // --- Logic Hành động của Boss ---

        // 1. Tấn công (Nếu trong range tấn công, hết cooldown)
        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            anim.SetBool(isWalkHash, false);
            CheckFlip();
            ChooseAndAttack();
        }
        // 2. Di chuyển (Nếu ở ngoài range tấn công nhưng trong range phát hiện)
        else if (distanceToPlayer < detectionRange)
        {
            anim.SetBool(isWalkHash, true); // Bắt đầu đi bộ
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
            CheckFlip();
        }
        // 3. Đứng yên (Nếu ngoài range phát hiện)
        else // (distanceToPlayer >= detectionRange)
        {
            anim.SetBool(isWalkHash, false); // Chuyển qua Idle (Dừng đi bộ)
        }
    }

    // --- Logic Tấn công chính (Chọn Skill) ---

    private void ChooseAndAttack()
    {
        isAttacking = true; // Bắt đầu quá trình tấn công

        float damageDelay = 0f;
        float resetTime = 0f;

        switch (attackState)
        {
            case 1:
                anim.SetTrigger(isAttack1Hash);
                currentDamage = skill_1;
                damageDelay = skill1_damageDelay;
                resetTime = skill1_resetTime;
                break;
            case 2:
                anim.SetTrigger(isAttack2Hash);
                currentDamage = skill_2;
                damageDelay = skill2_damageDelay;
                resetTime = skill2_resetTime;
                break;
            case 3:
                anim.SetTrigger(isThunderAttackHash);
                currentDamage = skill_thunder;
                damageDelay = skillThunder_damageDelay;
                resetTime = skillThunder_resetTime;
                break;
        }

        // Gây sát thương sau 'damageDelay' giây
        Invoke("InvokeDamage", damageDelay); 

        // Kết thúc trạng thái tấn công sau 'resetTime' giây
        Invoke("EndAttack", resetTime); 

        // Đặt lại thời gian Cooldown
        nextAttackTime = Time.time + attackCooldown;

        // Chuyển sang skill tiếp theo cho lần tấn công kế tiếp
        currentAttackCount++;
        if (currentAttackCount >= 2) 
        {
            attackState++;
            currentAttackCount = 0;
            if (attackState > 3)
            {
                attackState = 1; // Vòng lặp lại từ Skill 1
            }
        }
    }

    // Hàm trung gian được gọi bởi Invoke để thực hiện sát thương
    public void InvokeDamage()
    {
        DealDamage(currentDamage); 
    }
    
    // Hàm này được gọi TỪ INVOKE khi animation kết thúc
    public void EndAttack()
    {
        isAttacking = false; // Kết thúc quá trình tấn công, cho phép di chuyển và tấn công lại
    }


    // --- Logic Gây Sát thương ---

    public void DealDamage(int damageAmount)
    {
        // 1. Kiểm tra xem có Player nào trong phạm vi tấn công (attackPoint) không.
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayers);

        // 2. Gây sát thương cho Player tìm thấy.
        foreach (Collider2D player in hitPlayers)
        {
            IcanTakeDamge damageable = player.GetComponent<IcanTakeDamge>(); 
            if (damageable != null)
            {
                // Gọi hàm TakeDamage của Player/Enemy
                damageable.TakeDamage(damageAmount, Vector2.zero, gameObject);
            }
        }
    }

    /// <summary>
    /// Logic lật nhân vật để luôn đối diện Player
    /// </summary>
    void CheckFlip()
    {
        float directionX = player.position.x - transform.position.x;
        // Lật nếu Boss đang quay phải nhưng Player ở bên trái
        if (directionX < 0 && !isFacingRight) 
        {
            Flip();
        }
        // Lật nếu Boss đang quay trái nhưng Player ở bên phải
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
        // Vẽ Gizmo cho phạm vi tấn công
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        // Vẽ Gizmo cho phạm vi phát hiện
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}