using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    AudioSource DamageSource;

    [SerializeField]
    Animator Anim;

    public int Health = 3;
    public int _health;

    bool _canTakeDamage = true;
    bool _lost;

    private void Start()
    {
        _health = Health;
    }

    public void TakeDamage()
    {
        if (!_canTakeDamage) return;
        DamageSource.Play();
        Anim.CrossFade("PlayerHit", 0, 0);

        _health--;
        if(_health < 1)
        {
            if (_lost) return;
            StartCoroutine(Restart());

            Debug.Log("Lost");
            _lost = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damage"))
        {
            
            TakeDamage();
       
            StartCoroutine(FindObjectOfType<PlayerController>().HitAnimNum());
            StartCoroutine(SpareFrames());
        }
    }

    IEnumerator SpareFrames()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(1.1f);
        _canTakeDamage = true;
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
