using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    public float speed = 3.0f;
    public float timeInvincible = 2.0f;
    public GameObject projectilePrefab;
    public GameObject damageEffect;
    public GameObject healEffect;
    public GameObject Canvas;
    public Text cogNum;

    public AudioClip cogClip;

    public int health { get { return currentHealth; } }
    //public int cogs { get { return currentCogs; } }
    int currentHealth;
    int currentCogs = 0;




    bool isInvincible;
    float invincibleTimer;


    public bool hasPower = false;



    float horizontal;
    float vertical;

    Animator animator;
    AudioSource audioSource;
    Rigidbody2D rigidbody2d;

    Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;
        ChangeCogs(6);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }


        //Debug.Log("LookX: " + lookDirection.x);
        //Debug.Log("LookY: " + lookDirection.y);


        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                if (hit.collider != null)
                {
                    NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                    if (character != null)
                    {
                        character.DisplayDialog();
                    }
                }
            }
        }





        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
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
            GameObject DMG = Instantiate(damageEffect, rigidbody2d.position + Vector2.up * 1f, Quaternion.identity, gameObject.transform);

            invincibleTimer = timeInvincible;
        }
        else
        {
            GameObject HEAL = Instantiate(healEffect, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity, gameObject.transform);
        }


        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        if(currentHealth <=0)
        {
            Canvas.SendMessage("LoseScreen");
        }
    }

    void Launch()
    {
        if(currentCogs > 0)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);

            animator.SetTrigger("Launch");

            PlaySound(cogClip);

            ChangeCogs(-1);


            if(hasPower)
            {

                
                GameObject projectileObject2 = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
                Projectile projectile2 = projectileObject2.GetComponent<Projectile>();




                GameObject projectileObject3 = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
                Projectile projectile3 = projectileObject3.GetComponent<Projectile>();

                if (lookDirection.x == 0)
                {
                    projectile2.Launch(new Vector2(lookDirection.x + 0.2f, lookDirection.y), 300);

                    projectile3.Launch(new Vector2(lookDirection.x - 0.2f, lookDirection.y), 300);
                }
                else
                {

                    projectile2.Launch(new Vector2(lookDirection.x, lookDirection.y + 0.2f), 300);

                    projectile3.Launch(new Vector2(lookDirection.x, lookDirection.y - 0.2f), 300);

                }

            }

        }

    }


    public void ChangeCogs(int amount)
    {
        currentCogs += amount;
        cogNum.text = currentCogs.ToString();
    }


    public void GivePower()
    {
        hasPower = true;
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}


