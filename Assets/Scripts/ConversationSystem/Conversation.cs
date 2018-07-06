using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation : MonoBehaviour
{
    public List<NPCDialog> completedResponses = null;
    public NPCDialog currentNpcDialog = null;

    [SerializeField]
    GameObject npc;

    [SerializeField]
    Text npcText;

    [SerializeField]
    List<Text> playerResponseTexts = null;

    [SerializeField]
    List<PlotFilter> plotFilters = new List<PlotFilter>();

    [SerializeField]
    bool started = false;

    [SerializeField]
    bool completed = false;

    // Use this for initialization
    private void Awake()
    {
        npc = this.transform.parent.gameObject;
        NPCDialog startingDialog = null;
        foreach (Transform child in this.transform)
        {
            startingDialog = child.GetComponent<NPCDialog>();
            if (startingDialog && !startingDialog.isOnlyOnConversationCompletion())
            {
                this.currentNpcDialog = startingDialog;
                break;
            }
        }
        if (!this.currentNpcDialog)
        {
            this.completed = true;
        }
        this.plotFilters = new List<PlotFilter>(this.GetComponents<PlotFilter>());
    }

    // Update is called once per frame
    private void Update()
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

    //////////////////////////////////////////////////////////////
    // NPC
    //////////////////////////////////////////////////////////////

    public NPC getNPC()
    {
        return this.npc.GetComponent<NPC>();
    }

    public void startConversation()
    {
        this.started = true;
        this.npcText.transform.position = new Vector3(this.npc.transform.position.x, this.npc.transform.position.y + 0.5f, this.npcText.transform.position.z);
        if (this.completed)
        {
            this.npcText.text = this.getCompletedResponse();
        }
        else
        {
            this.npcText.text = this.currentNpcDialog.npcResponse;
            Invoke("updatePlayerResponses", 2);
        }
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

    private void hideNpcResponse()
    {
        this.npcText.text = string.Empty;
    }

    private string getCompletedResponse()
    {
        string defaultDialog = null;
        foreach (NPCDialog dialog in this.completedResponses)
        {
            if (dialog.isNpcResponse() && dialog.hasPlotFilter())
            {
                return dialog.npcResponse;
            }
            else if (dialog.isNpcResponse())
            {
                defaultDialog = dialog.npcResponse;
            }
        }
        return defaultDialog;
    }

    //////////////////////////////////////////////////////////////
    // PLAYER
    //////////////////////////////////////////////////////////////

    private void updatePlayerResponses()
    {
        List<PlayerDialog> playerResponses = this.currentNpcDialog.getPlayerResponses();
        for (int i = 0; i < playerResponses.Count; i += 1)
        {
            this.playerResponseTexts[i].text = playerResponses[i].dialogOne + playerResponses[i].getKeyCode().ToString();
        }
    }

    private void hidePlayerResponseText()
    {
        foreach (Text text in this.playerResponseTexts)
        {
            text.text = string.Empty;
        }
    }
}