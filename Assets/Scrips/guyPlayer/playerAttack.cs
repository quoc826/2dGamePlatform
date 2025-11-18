using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class playerAttack : MonoBehaviour
{

    public int damageToGive = 10;
    public Transform pointAttack;
    public float rangeAttack = 0.5f;

    public float nextAttack = 0;
    public float timeDelay = 0.2f;
    public LayerMask EnemyLayer;


    private Animator anim;
    private int IsAttack;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        IsAttack = Animator.StringToHash("isAttack");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Getkey();
            anim.SetTrigger(IsAttack);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (pointAttack == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pointAttack.position, rangeAttack);
    }

    IEnumerator Attack()
    {
        Collider2D[] enemiesToDamge = Physics2D.OverlapCircleAll(pointAttack.position, rangeAttack, EnemyLayer);
        for (int i = 0; i < enemiesToDamge.Length; i++)
        {
            enemiesToDamge[i].GetComponent<IcanTakeDamge>()?.TakeDamage(damageToGive, pointAttack.position, gameObject);
        }
        yield return new WaitForSeconds(0.1f);
    }

    private bool Getkey()
    {
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + timeDelay;
            StartCoroutine(Attack());
            return true;
        }
        else
        {
            return false;
        }
    }
}
