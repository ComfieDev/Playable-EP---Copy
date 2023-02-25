using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipVelocity : MonoBehaviour
{
    Rigidbody2D _rb;
    SpriteRenderer _re;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _re = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if(_rb.velocity.x > 0)
        {
            _re.flipX = false;
        }
        else if(_rb.velocity.x < 0)
        {
            _re.flipX = true;
        }
    }
}
