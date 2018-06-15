using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation : MonoBehaviour
{

    [TextArea]
    public string completedResponse = "";
    public NPCDialog currentNpcDialog = null;

    [SerializeField]
    GameObject npc;

    [SerializeField]
    GameObject player;

    [SerializeField]
    Text npcText;

    [SerializeField]
    List<Text> playerResponseTexts = null;

    bool started = true;
    bool completed = false;

    public NPC getNPC()
    {
        return this.npc.GetComponent<NPC>();
    }

    // Use this for initialization
    void Start()
    {
        npc = this.transform.parent.gameObject;
        foreach (Transform child in this.transform)
        {
            this.currentNpcDialog = child.GetComponent<NPCDialog>();
        }
        this.npcText.transform.position = new Vector3(this.npc.transform.position.x, this.npc.transform.position.y + 0.5f, this.npcText.transform.position.z);
        this.npcText.text = this.currentNpcDialog.npcResponse;
        Invoke("updatePlayerResponses", 2);
    }

    void updatePlayerResponses()
    {
        List<PlayerDialog> playerResponses = this.currentNpcDialog.getPlayerResponses();
        for (int i = 0; i < playerResponses.Count; i += 1)
        {
            this.playerResponseTexts[i].text = playerResponses[i].dialogOne + playerResponses[i].getKeyCode().ToString();
            if (i == 0)
            {
                this.playerResponseTexts[i].transform.position = new Vector3(this.player.transform.position.x + 2f,
                    this.player.transform.position.y,
                    this.playerResponseTexts[i].transform.position.z);
            }
            else if (i == 1)
            {
                this.playerResponseTexts[i].transform.position = new Vector3(this.player.transform.position.x - 0.5f,
                    this.player.transform.position.y,
                    this.playerResponseTexts[i].transform.position.z);
            }
            else if (i == 2)
            {
                this.playerResponseTexts[i].transform.position = new Vector3(this.player.transform.position.x,
                    this.player.transform.position.y + 0.5f,
                    this.playerResponseTexts[i].transform.position.z);
            }
        }
    }

    void hidePlayerResponseText()
    {
        foreach (Text text in this.playerResponseTexts)
        {
            text.text = string.Empty;
        }
    }

    void hideNpcResponse()
    {
        this.npcText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.started && !this.completed)
        {
            this.currentNpcDialog.fixedUpdate();
            if (this.currentNpcDialog.getPlayerResponse())
            {
                this.hidePlayerResponseText();
                PlayerDialog playerDialog = this.currentNpcDialog.getPlayerResponse();
                this.currentNpcDialog = playerDialog.getNpcResponse();
                if (this.currentNpcDialog)
                {
                    this.npcText.text = this.currentNpcDialog.npcResponse;
                    Invoke("updatePlayerResponses", 5);
                    Debug.Log(this.currentNpcDialog.npcResponse);
                    this.completed = !this.currentNpcDialog.HasPossibleResponses();

                }
                else
                {
                    this.hideNpcResponse();
                    this.hidePlayerResponseText();
                    this.completed = true;
                }
            }
        }
    }
}