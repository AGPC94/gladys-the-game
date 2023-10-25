using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseRandomForce : MonoBehaviour
{
    [SerializeField] protected float forceV;
    float forceH;
    [SerializeField] protected float forceHRange;
    protected Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected void Impulse()
    {
        rb.velocity = Vector2.zero;

        forceH = Random.Range(-forceHRange, forceHRange);

        rb.AddForce(new Vector2(forceH, forceV), ForceMode2D.Impulse);
    }
}
