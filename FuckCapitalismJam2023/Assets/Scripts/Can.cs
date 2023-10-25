using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : ImpulseRandomForce, IPooledObject
{
    public void OnObjectSpawn()
    {
        Impulse();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<Player>().Hurt();
    }
}
