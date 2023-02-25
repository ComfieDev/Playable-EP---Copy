using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoundController : MonoBehaviour
{
    [SerializeField]
    AudioSource WaterSource;

    [SerializeField]
    Animator Anim;

    [SerializeField]
    float startwaittime = 2.2f;
    [SerializeField]
    float Speed;

    [SerializeField]
    float[] Speeds;
    int x;

    Rigidbody2D _rb;
    bool _move;

    [SerializeField]
    Transform PlayerPos;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(StartMove), startwaittime);
    }

    private void FixedUpdate()
    {
        if (!_move) return;
        _rb.velocity = new Vector2(1 * Speed * Speeds[x] * Time.fixedDeltaTime, _rb.velocity.y);
    }

    void StartMove()
    {
        _move = true;
        Anim.CrossFade("CootsRun", 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HoundTrigger"))
        {
            x++;
        }

        if (collision.CompareTag("Water"))
        {
            WaterSource.Play();
        }
    }
}
