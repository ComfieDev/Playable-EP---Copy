using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField]
    float Speed;

    [SerializeField]
    Transform Obstacle;

    [SerializeField]
    Transform PointA, PointB;

    Vector3 currentTargetDestination;

    public float distanceTolerance = 0.5f;
 
    void Start()
    {
        Obstacle.position = PointA.position; //set the initial position
        currentTargetDestination = PointB.position;
    }

    void Update()
    {
        Obstacle.position = Vector3.MoveTowards(Obstacle.position, currentTargetDestination, Speed * Time.deltaTime);

        if (Vector3.Distance(Obstacle.position, currentTargetDestination) <= distanceTolerance)
        {
            //once we reach the current destination, set the other location as our new destination
            if (currentTargetDestination == PointA.position)
            {
                currentTargetDestination = PointB.position;
            }
            else if(currentTargetDestination == PointB.position)
            {
                currentTargetDestination = PointA.position;
            }
        }
    }
}
