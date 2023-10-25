using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    Vector2 input;
    [SerializeField] bool canMove = true;
    [SerializeField] bool isSwimming = true;
    [SerializeField] bool isInvincible;
    [SerializeField] bool isHurt;
    [Space]
    [SerializeField] float rotationSpeed;
    [SerializeField] float cooldownMove;
    [Space]
    [SerializeField] float speed;
    [SerializeField] float speedAtk;
    [SerializeField] float speedMax;
    [Space]
    [SerializeField] float timeResetMove;
    [SerializeField] float timeResetInvincible;
    [Space]
    [SerializeField] float magnitude;
    float oriGravity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        oriGravity = rb.gravityScale;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //Inputs
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            input.Normalize();

            //Rotate to direction
            if (rb.velocity != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(transform.forward, rb.velocity);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

            //Flip
            if (rb.velocity != Vector2.zero)
            {
                if (rb.velocity.x > 0)
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                else
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            }
        }

        anim.SetBool("isHurt", isHurt);

        //anim.SetFloat("speed", rb.velocity.magnitude);
    }

    void FixedUpdate()
    {
        if (!canMove)
            rb.velocity = Vector2.zero;

        if (isSwimming)
        {
            rb.AddForce(input * speed, ForceMode2D.Force);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = oriGravity;
        }

        if (rb.velocity.magnitude > speedMax)
        {
            rb.velocity = rb.velocity.normalized * speedMax;
        }

        magnitude = rb.velocity.magnitude;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Boat"))
        {
            if (magnitude >= speedAtk)
            {
                if (canMove)
                {
                    collision.collider.GetComponent<Boat>().Hurt();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isSwimming = true;
            AudioManager.instance.Play("EnterWater");
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isSwimming = false;
            AudioManager.instance.Play("LeaveSurface");
        }
    }

    public void Hurt()
    {
        if (!isInvincible)
        {
            canMove = false;
            isHurt = true;
            isInvincible = true;

            Invoke("ResetMove", timeResetMove);

            AudioManager.instance.Play("GladysHurt");
        }
    }

    void ResetMove()
    {
        canMove = true;
        isHurt = false;

        Invoke("ResetInvincible", timeResetInvincible);
    }

    void ResetInvincible()
    {
        isInvincible = false;
    }
}