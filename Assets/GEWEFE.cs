using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GEWEFE : MonoBehaviour
{
    [SerializeField]
    GameObject Fade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hound"))
        {
            Invoke(nameof(kegewg), 1f);
            Fade.SetActive(true);
        }
    }

    void kegewg()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
