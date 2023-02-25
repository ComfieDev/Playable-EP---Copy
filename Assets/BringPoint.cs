using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BringPoint : MonoBehaviour
{
    [SerializeField]
    Sprite OpenSprite;


    [SerializeField]
    GameObject FadeOBJ;

    [SerializeField]
    AudioSource Source;

    public void ActivateGate()
    {
        Source.Play();
        GetComponent<Collider2D>().enabled = true;
        GetComponentInChildren<SpriteRenderer>().sprite = OpenSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            Invoke(nameof(LoadNextScene), 3.5f);
            FadeOBJ.SetActive(true);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
