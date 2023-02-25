using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField]
    Animator Anim;

    [SerializeField]
    Rigidbody2D RB;

    [SerializeField]
    float JumpSpeed;

    [SerializeField]
    float TimeBetweenJump, FirstWait;

    float _currentTime;

    private void Start()
    {
        _currentTime = FirstWait;
    }

    private void Update()
    {
        if(_currentTime <= 0)
        {
            Jump();
            _currentTime = TimeBetweenJump;
        }
        else
        {
            _currentTime -= Time.deltaTime;
        }

        if(RB.velocity.y < -1)
        {
            Anim.CrossFade("JumperFall", 0, 0);
        }
    }

    void Jump()
    {
        Anim.CrossFade("JumperJump", 0, 0);
        RB.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Anim.CrossFade("JumperIdle", 0, 0);
    }
}
