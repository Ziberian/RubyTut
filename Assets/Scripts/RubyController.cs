using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public GameObject projectilePrefab;

    public int maxHealth = 5;
    public int health { get { return currentHealth; }}
    public int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    public float speed = 5.0f;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    public HealEffectDock hed;
    
    AudioSource audioSource;
    public AudioClip shotCogClip;
    public AudioClip tookDamageClip;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        hed = GetComponent<HealEffectDock>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if(Input.GetKeyDown(KeyCode.C))
        {
        Launch();
        PlaySound(shotCogClip);
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                character.DisplayDialog1();
            }
        }

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        
        rigidbody2d.MovePosition(position);
    }


    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            PlaySound(tookDamageClip);

        }
        if (amount > 0)
        {
           hed.HealParticle();
        }
           currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
           UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, 
        Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 100);

        animator.SetTrigger("Launch");
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
