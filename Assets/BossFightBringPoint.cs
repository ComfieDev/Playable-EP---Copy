using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightBringPoint : MonoBehaviour
{
    [SerializeField]
    GameObject[] Phases;

    int ir = 1;
    public void OpenGate()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ir++;
            GetComponent<Collider2D>().enabled = false;
            for (int i = 0; i < Phases.Length; i++)
            {
                if (Phases[i] == Phases[ir])
                {
                    Phases[ir].SetActive(true);

                }
                else
                {
                    Phases[ir].SetActive(false);

                }
            }
        }
    }
}
