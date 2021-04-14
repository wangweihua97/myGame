using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public bool dead = false;
    public int explosionRadius = 40; // unity units * 100

    public void OnDestroy()
    {

        Vector2 explosionPos = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, (float)explosionRadius/100);

        for (int i = 0; i < colliders.Length; i++)
        {
            // TODO: two calls for getcomponent is bad
            if(colliders[i].GetComponent<DestructibleSprite>())
                colliders[i].GetComponent<DestructibleSprite>().ApplyDamage(explosionPos, explosionRadius);
        }
    }

    public void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up*-1);
    }

    void OnCollisionEnter2D (Collision2D coll)
    {
        if (dead) {
            return;
        }
        Destroy(gameObject);
    }
}
