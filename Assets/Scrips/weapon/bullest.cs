using Unity.VisualScripting;
using UnityEngine;

public class bullest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float timeDestroy = 3f;
    public int dameValue = 1;
    public AudioClip shooting;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IcanTakeDamge damageAble = collision.gameObject.GetComponent<IcanTakeDamge>();
            if (audioManager.Instance != null && shooting != null)
            {
                audioManager.Instance.PlayGunSound(shooting);
            }
            if (damageAble != null)
            {
                damageAble.TakeDamage(dameValue, Vector2.zero, gameObject);
            }
            Destroy(gameObject, 0.05f);
        }
    }
}
