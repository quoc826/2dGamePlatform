using System;
using UnityEngine;

public class Player : MonoBehaviour, IcanTakeDamge
{
    public int health = 100;
    public int currentHealth;

    public bool isDead = false;
     public Animator anim;
    public int deadAnimation;



    [SerializeField] HealthBar healthBar;


    void Start()
    {

        currentHealth = health;
        healthBar.updateHealthBar(health, currentHealth);
        anim = GetComponentInChildren<Animator>();
        deadAnimation = Animator.StringToHash("isDead");
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void TakeDamage(int damageAmount, Vector2 hitPoint, GameObject hitDirection)
    {
        currentHealth -= damageAmount;
        Debug.Log("PLAYER: Da nhan sat thuong " + damageAmount + ". Mau con lai: " + currentHealth);
        healthBar.updateHealthBar(health, currentHealth);
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            deadPlayer();
        }
    }

    private void deadPlayer()
    {
        anim.SetTrigger(deadAnimation);
        Destroy(gameObject, 1f);

        gameManager.Instance.gameOver();
    }

    public bool IsDead()
    {
        return isDead;
    }
}
