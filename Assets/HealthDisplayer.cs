using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayer : MonoBehaviour
{
    public int numOfhearts;

    public Image[] hearts;
    public Sprite full;
    public Sprite empty;

    HealthSystem _healthsystem;

    private void Start()
    {
        _healthsystem = FindObjectOfType<HealthSystem>();
    }

    private void Update()
    {

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < _healthsystem._health)
            {

                hearts[i].sprite = full;
            }
            else
            {
                hearts[i].sprite = empty;
            }

            if(i < numOfhearts)
            {
                hearts[i].enabled = true;
            }
            else{
                hearts[i].enabled = false;
            }
        }
    }
}
