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
    PolygonCollider2D commandDeckPolygon;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.transform.position = new Vector3(destination.position.x, destination.position.y);
            confiner.m_BoundingShape2D = commandDeckPolygon;
        }
    }
}
