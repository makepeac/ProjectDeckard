using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField]
    string characterName;

    [SerializeField]
    List<Conversation> availableConversations;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        foreach (Transform child in this.transform)
        {
            Conversation conversation = child.GetComponent<Conversation>();
            if (conversation)
            {
                this.availableConversations.Add(conversation);
            }
        }
    }

    public string getName()
    {
        return this.characterName;
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            foreach (Conversation convo in this.availableConversations)
            {
                if (convo.canStart())
                {
                    convo.startConversation();
                    return;
                }
            }
        }
    }
}
