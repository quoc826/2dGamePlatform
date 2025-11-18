using Unity.Mathematics;
using UnityEngine;

public class trapAttack : MonoBehaviour
{
    public int damageTrap = 30;

    void Start()
    {

    }
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IcanTakeDamge canTakeDamage = collision.GetComponent<IcanTakeDamge>();

        if(canTakeDamage != null)
        {
            canTakeDamage.TakeDamage(damageTrap, transform.position, gameObject);
        }
    }

}
