using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Present : MonoBehaviour
{
    SpriteRenderer sprite;
    public Sprite[] present;
    public Sprite[] coal;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = present[Random.Range(0, present.Length)];
        sprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            sprite.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            sprite.sprite = coal[Random.Range(0, coal.Length)];
            collision.otherCollider.enabled = false;
            enabled = false;
            print("Present collected");
        }
    }
}
