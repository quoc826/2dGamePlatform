using System;
using System.Runtime.Versioning;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("Fire Settings")]
    public GameObject bulletPrefab;
    public Transform bulletsPosition;
    public float bulletSpeed = 20f;
    private float timeDestroy = 2f;

    private guyPlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<guyPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
            
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletsPosition.position, bulletsPosition.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (playerController.FacingRight())
        {
            rb.linearVelocity = Vector2.right * bulletSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.left * bulletSpeed;
        }
    }
}
