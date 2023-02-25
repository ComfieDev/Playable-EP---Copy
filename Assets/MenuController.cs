using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    GameObject Fade;

    bool _canStart;

    private void Start()
    {
        Invoke(nameof(Canstart), 3f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canStart)
            {
                StartCoroutine(StartGame());
            }
        }
    }

    void Canstart()
    {
        _canStart = true;
    }

    IEnumerator StartGame()
   {
        Fade.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
   }
}
