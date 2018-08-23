using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    Transform destination = null;

    [SerializeField]
    Cinemachine.CinemachineConfiner confiner = null;

    [SerializeField]
    PolygonCollider2D destinationBoundsPolygon;

    [SerializeField]
    bool hasButtonTrigger = false; 

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && !hasButtonTrigger)
        {
            other.gameObject.transform.position = new Vector3(destination.position.x, destination.position.y);
            confiner.m_BoundingShape2D = destinationBoundsPolygon;
        }
    }

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "Player" && Input.GetAxis("Vertical")> 0 && hasButtonTrigger){
            other.gameObject.transform.position = new Vector3(destination.position.x, destination.position.y);
            confiner.m_BoundingShape2D = destinationBoundsPolygon;
        }
    }
}
