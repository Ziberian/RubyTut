using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public bool vertical;

    public bool broken;

    public float changeTime = 3.0f;
    float timer;
    int direction = 1;

    public ParticleSystem smokeEffect;
    public ParticleSystem stunnedEffect;

    public float enemySpeed = 2.0f;

    Animator animator;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        broken = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <0)
        {
            direction = -direction;
            timer = changeTime;
        }

        if (!broken)
        {
            return;
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + enemySpeed * Time.deltaTime * direction;

            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + enemySpeed * Time.deltaTime * direction;

            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
    
        rigidbody2d.MovePosition(position);

        if (!broken)
        {
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        //without rigidbody can't stop bullets nor hurt the player

        animator.SetTrigger("Fixed");
        
        //for this to work, in inspector drag the smoke effect UNDER the enemy prefab to the slot

        smokeEffect.Stop();
        ParticleSystem stunnedEffectlocal =  Instantiate(stunnedEffect, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        stunnedEffectlocal.transform.parent = this.transform;
    }
}
