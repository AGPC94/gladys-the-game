using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float coinsPerHit;
    [SerializeField] float speed;

    [Space]
    [SerializeField] float timeBetweenCans;
    [SerializeField] float timeResetThrow;

    [Space]
    [SerializeField] Sprite spriteDead;
    [SerializeField] float timeInvincible;
    [SerializeField] bool isInvincible;

    float direction = -1;

    Rigidbody2D rb;
    ObjectPooler pooler;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pooler = GetComponentInChildren<ObjectPooler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThrowCans());

        GameManager.instance.AddBoat();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * direction * Time.fixedDeltaTime, rb.velocity.y);

        Vector3 scale = transform.localScale;
        if (direction == -1)
            scale.x = 1;
        else
            scale.x = -1;
        transform.localScale = scale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            direction *= -1;
        }
    }

    public void Hurt()
    {
        print("Hurt enemy");

        if (isInvincible)
            return;

        for (int i = 0; i < coinsPerHit; i++)
            pooler.SpawnFromPool("Coin", transform.position, Quaternion.identity);

        AudioManager.instance.Play("BoatHit");

        health--;
        if (health <= 0)
        {
            Die();
        }

        StopAllCoroutines();
        Invoke("ResetInvincible", timeInvincible);
    }

    void Die()
    {
        AudioManager.instance.Play("BoatDrown");
        GameManager.instance.SubstractBoat();

        GetComponent<Collider2D>().enabled = false;

        GetComponent<SpriteRenderer>().sprite = spriteDead;
        StopAllCoroutines();
        enabled = false;
    }

    void ResetInvincible()
    {
        isInvincible = false;
        StartCoroutine(ThrowCans());
    }

    IEnumerator ThrowCans()
    {
        float maxCans = 0;

        foreach (var item in pooler.pools)
            if (item.tag.Equals("Can"))
                maxCans = item.size;

        while (health > 0)
        {
            for (int i = 0; i < maxCans; i++)
            {
                yield return new WaitForSeconds(timeBetweenCans);
                pooler.SpawnFromPool("Can", transform.position, Quaternion.identity);
                AudioManager.instance.Play("ShotCan");
            }

            yield return new WaitForSeconds(timeResetThrow);
        }
    }
}
