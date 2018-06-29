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

    [SerializeField]
    List<PlotFilter> plotFilters = new List<PlotFilter>();

    bool started = false;
    bool completed = false;

    public NPC getNPC()
    {
        return this.npc.GetComponent<NPC>();
    }

    public void startConversation()
    {
        this.started = true;
        this.npcText.transform.position = new Vector3(this.npc.transform.position.x, this.npc.transform.position.y + 0.5f, this.npcText.transform.position.z);
        this.npcText.text = this.currentNpcDialog.npcResponse;
        Invoke("updatePlayerResponses", 2);
    }

    public bool canStart()
    {
        foreach (PlotFilter filter in this.plotFilters)
        {
            if (!filter.isActive())
            {
                return false;
            }
        }
        return true;
    }

    // Use this for initialization
    void Start()
    {
        npc = this.transform.parent.gameObject;
        foreach (Transform child in this.transform)
        {
            this.currentNpcDialog = child.GetComponent<NPCDialog>();
        }
        this.plotFilters = new List<PlotFilter>(this.GetComponents<PlotFilter>());
    }

    void updatePlayerResponses()
    {
        List<PlayerDialog> playerResponses = this.currentNpcDialog.getPlayerResponses();
        for (int i = 0; i < playerResponses.Count; i += 1)
        {
            this.playerResponseTexts[i].text = playerResponses[i].dialogOne + playerResponses[i].getKeyCode().ToString();
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