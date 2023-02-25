using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    AudioSource GroundHitA, JumpA, DirectionSwitchA, StopA, MoveA, SplashA;

    [SerializeField]
    Animator Anim;

    [SerializeField]
    float JumpForce;
    [SerializeField]
    float SkatingSpeed1, SkatingSpeed2, SkatingSpeed3;
    [SerializeField]
    float MaxHoldJump;
    [SerializeField]
    float MinimumHoldMultiplier;
    [SerializeField]
    float MaxHoldHorizontal;
    [SerializeField]
    float MinimumHorizontalHoldMultiplier;
    [SerializeField]
    float BounceBackStrength;

    [Space()]

    [SerializeField]
    Rigidbody2D RB;

    [SerializeField]
    Transform GroundDetectionPoint;
    [SerializeField]
    LayerMask GroundMask;

    bool _onGround;
    bool _goingRight;
    bool _notMoving = true;

    float _x, _y;

    [SerializeField]
    SpriteRenderer _rend;
    bool _jumped;

    bool _playParticle;
    //bool _called;
    bool _takingDamage;
    public bool _canMove = true;
    float _currentHold;
    bool _leftHit, _rightHit;

    float _currentHorizontalHold;
    [SerializeField]
    float Gravity, FallGravity;

    bool _Xswitch;
    int sas;

    float _curSpeed;bool _sameDir;
    float _dirCount;
    bool _holding;
    bool _canAirJump;

    bool _hitting;

    private void Start()
    {

        _currentHold = MinimumHoldMultiplier;
        _curSpeed = SkatingSpeed1;
    }
    bool _landing;

    public IEnumerator HitAnimNum()
    {
        _hitting = true;
        yield return new WaitForSeconds(1f);
        _hitting = false;
    }
    void ResetLand()
    {
        _landing = false;
    }

    private void Update()
    {

        if (!_canMove) return;

        _x = Input.GetAxisRaw("Horizontal");
        _y = Input.GetAxisRaw("Vertical");

        _onGround = Physics2D.OverlapBox(GroundDetectionPoint.position, new Vector2(0.9f, 0.3f), 0f, GroundMask);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _holding = false;
        }

        if (_x != 0)
        {

            if (_x > 0&& _onGround)
            {
                _rend.flipX = false;
            }
            else if (_x < 0 && _onGround)
            {
                _rend.flipX = true;
            }
            if (!_Xswitch)
            {
                if (!_onGround) return;
                if(_dirCount == _x)
                {
                    DirectionSwitchA.Play();

                    sas++;
                    if (sas == 1)
                    {
                       
                        _curSpeed = SkatingSpeed1;
                    }
                    else if (sas == 2)
                    {
                        _curSpeed = SkatingSpeed2;
                    }
                    else if (sas == 3)
                    {
                        _curSpeed = SkatingSpeed3;
                    }
                    if (sas > 3)
                    {
                        sas = 3;
                    }
                }

                _Xswitch = true;
                _dirCount = _x;
            }
        }
        else
        {
            _Xswitch = false;
        }

      
        

        if(_y > 0)
        {
            _currentHorizontalHold = MinimumHorizontalHoldMultiplier;
        }

        if (RB.velocity.y > 0 && !_onGround)
        {
            if (!_hitting && !_landing)
            {
                Anim.CrossFade("PlayerJump", 0, 0);
            }

            _jumped = true;
        }

        if (_onGround)
        {
            if (_jumped)
            {
                if (!_hitting)
                {
                    GroundHitA.Play();
                    Anim.CrossFade("PlayerLand", 0, 0);
                }
                _landing = true;
                Invoke(nameof(ResetLand), 0.25f);

            }

            _jumped = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
            }
            if (Input.GetKey(KeyCode.Space))
            {
                _holding = true;
                if(_currentHold > MaxHoldJump)
                {
                    JumpA.Play();
                    RB.velocity = Vector2.zero;
                    RB.AddForce(Vector2.up * JumpForce * _currentHold, ForceMode2D.Impulse);
                    _canAirJump = false;
                    _currentHold = MinimumHoldMultiplier;
                    _holding = false;
                }
                else
                {
                    _currentHold += Time.deltaTime;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                JumpA.Play();
                RB.velocity = Vector2.zero;
                RB.AddForce(Vector2.up * JumpForce * _currentHold, ForceMode2D.Impulse);
                _canAirJump = false;
                _currentHold = MinimumHoldMultiplier;
                _holding = false;
            }

            if (RB.velocity.y < -0.5f)
            {
                RB.gravityScale = FallGravity;
            }
            else
            {
                RB.gravityScale = Gravity;
            }
        }

        if (_onGround)
        {

            if (RB.velocity.x != 0 && !_landing && !_hitting)
            {
                if(sas == 1)
                {
                    Anim.CrossFade("PlayerMove", 0, 0);
                }
                else if(sas == 2)
                {
                    Anim.CrossFade("PlayerMove2", 0, 0);
                }
                else if(sas == 3)
                {
                    Anim.CrossFade("PlayerMove3", 0, 0);
                }
                
            }

            if (_x < 0 && _onGround)
            {

                _goingRight = false;
                _notMoving = false;
            }
            else if (_x > 0 && _onGround)
            {
                _goingRight = true;
                _notMoving = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            sas = 0;
            if (sas == 1)
            {
                _curSpeed = SkatingSpeed1;
            }
            else if (sas == 2)
            {
                _curSpeed = SkatingSpeed2;
            }
            else if (sas == 3)
            {
                _curSpeed = SkatingSpeed3;
            }

            _notMoving = !_notMoving;

            if (RB.velocity.x == 0 && _onGround && _notMoving && !_landing && !_hitting)
            {
                Anim.CrossFade("PlayerIdle", 0, 0);
            }
        }

        if (RB.velocity.x == 0 && _onGround && _notMoving && !_landing && !_hitting)
        {
            Anim.StopPlayback();
            Anim.SetTrigger("DoIdle");
        }
    }

    bool _doneSwitch;

    private void FixedUpdate()
    {
        if (!_canMove)
        {
            RB.velocity = new Vector2(0, RB.velocity.y);
            return;
        }
        if (!_notMoving)
        {
            if (_goingRight)
            {
                RB.velocity = new Vector2(1 * _curSpeed * Time.fixedDeltaTime, RB.velocity.y);
                if (!_doneSwitch)
                {
                    sas--;
                    if (sas == 1)
                    {
                        _curSpeed = SkatingSpeed1;
                    }
                    else if (sas == 2)
                    {
                        _curSpeed = SkatingSpeed2;
                    }
                    else if (sas == 3)
                    {
                        _curSpeed = SkatingSpeed3;
                    }
                    _doneSwitch = true;
                    if (sas < 1)
                    {
                        sas = 1;
                    }
                }
            }
            else
            {
                RB.velocity = new Vector2(-1 * _curSpeed * Time.fixedDeltaTime, RB.velocity.y);
                if (_doneSwitch)
                {
                    sas--;
                    if (sas == 1)
                    {
                        _curSpeed = SkatingSpeed1;
                    }
                    else if (sas == 2)
                    {
                        _curSpeed = SkatingSpeed2;
                    }
                    else if (sas == 3)
                    {
                        _curSpeed = SkatingSpeed3;
                    }
                    _doneSwitch = false;
                    if (sas < 1)
                    {
                        sas = 1;
                    }
                }
            }
        }
        else
        {
            RB.velocity = new Vector2(0, RB.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _canAirJump = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _playParticle = false;

        if (_canAirJump)
        {
            if (_holding)
            {
                RB.velocity = Vector2.zero;
                RB.AddForce(Vector2.up * JumpForce * _currentHold, ForceMode2D.Impulse);
                _holding = false;
                _currentHold = MinimumHoldMultiplier;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("JumpPad"))
        {
            RB.AddForce(Vector2.up * JumpForce * 0.8f, ForceMode2D.Impulse);
        }
        if (collision.CompareTag("Water"))
        {
            SplashA.Play();
        }
    }
    public void StopDamaging()
    {
        _takingDamage = false;
    }
}
