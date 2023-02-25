using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOundPad : MonoBehaviour
{
    [SerializeField]
    float Speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hound"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Speed, ForceMode2D.Impulse);
        }
    }
}
