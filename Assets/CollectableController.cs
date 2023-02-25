using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    [SerializeField]
    AudioSource CollectableA;

    [SerializeField]
    GameObject ThingAnim;

    [SerializeField]
    GameObject Calico, Grey, Pink;

    //bool _haveCollectable;
    GameObject Actor;

    List<GameObject> Actors = new List<GameObject>();
    public bool IsBossfight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            //if (_haveCollectable) return;

            collision.GetComponent<Collectable>().Collect();
            
        }

        if (collision.CompareTag("BringPoint"))
        {
            //_haveCollectable = false;
            Actors.RemoveAt(1);
            Destroy(Actor);
        }
    }

    public void SpawnActor(int t)
    {
        CollectableA.Play();
        //_haveCollectable = true;
        if(t == 1)
        {
            Actor = Instantiate(Calico, transform.position, Quaternion.identity);
        }else if(t == 2)
        {
            Actor = Instantiate(Grey, transform.position, Quaternion.identity);
        }
        else if(t == 3)
        {
            Actor = Instantiate(Pink, transform.position, Quaternion.identity);
        }

        Actors.Add(Actor);
        if(Actors.Count == 1)
        {
            Actor.GetComponent<SpringJoint2D>().distance = 0.5f;
        }else if(Actors.Count == 2)
        {
            Actor.GetComponent<SpringJoint2D>().distance = 2.5f;
        }
        else
        {
            ThingAnim.SetActive(true);
            Actor.GetComponent<SpringJoint2D>().distance = 4.5f;
            if (!IsBossfight)
            {
                FindObjectOfType<BringPoint>().ActivateGate();
            }
            else
            {
                FindObjectOfType<BossFightBringPoint>().OpenGate();
            }
        }

        Actor.GetComponent<SpringJoint2D>().connectedBody = GetComponent<Rigidbody2D>();

    }
}
