using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    Animator animator;

    BoxCollider2D hitbox;

    public bool attacking;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        hitbox = GetComponent<BoxCollider2D>();
        hitbox.enabled = false;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && !attacking)
        {
            attacking = true;
            animator.SetTrigger("attack");
        }
    }

    void FixedUpdate()
    {
        // Wait for attacking animation to finish
        if (attacking && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            attacking = false;
    }
    
    // Hitbox enabled on specific frame of animation
    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Need to set isHit since entities have more than one collider which could cause multiple collisions on the same object simultaneously
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (!enemy.isHit)
            {
                enemy.isHit = true;
                Debug.Log("Enemy hit!");
            }
        }
    }
}
