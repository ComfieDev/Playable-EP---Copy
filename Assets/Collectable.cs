using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    int t;

    [SerializeField]
    GameObject Skin;

    private void Start()
    {
        Skin.SetActive(true);
    }
    public void Collect()
    {
        gameObject.SetActive(false);
        FindObjectOfType<CollectableController>().SpawnActor(t);
    }
}
