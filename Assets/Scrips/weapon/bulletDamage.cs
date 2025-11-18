using UnityEngine;

public class bulletDamage : MonoBehaviour
{

    public int damageBullet = 30;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IcanTakeDamge icanTakeDamge = collision.gameObject.GetComponent<IcanTakeDamge>();
            if (icanTakeDamge != null)
            {
                icanTakeDamge.TakeDamage(damageBullet, transform.position, gameObject);
                Destroy(gameObject);
            }
        }
    }

}

