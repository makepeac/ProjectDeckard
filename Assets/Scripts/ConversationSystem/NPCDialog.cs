using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    [Header("Option 1")]
    [TextArea]
    public string npcResponse = "";

    [SerializeField]
    List<PlayerDialog> playerResponses = new List<PlayerDialog>();

    [SerializeField]
    List<NPCDialog> npcContinuations = new List<NPCDialog>();

    private KeyCode[] responseKeyCodes = { KeyCode.A, KeyCode.W, KeyCode.D };

    PlayerDialog playerResponse;

    [SerializeField]
    PlotFilter plotFilter = null;

    [SerializeField]
    bool triggerWhenComplete = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        this.playerResponses = new List<PlayerDialog>();
        int i = 0;
        foreach (Transform child in this.transform)
        {
            PlayerDialog dialog = child.GetComponent<PlayerDialog>();
            if (dialog)
            {
                dialog.setKeyCode(responseKeyCodes[i]);
                i += 1;
                this.playerResponses.Add(dialog);
            }
            NPCDialog npcContinuation = child.GetComponent<NPCDialog>();
            if (npcContinuation)
            {
                this.npcContinuations.Add(npcContinuation);
            }
        }
        this.plotFilter = this.GetComponent<PlotFilter>();
    }

    public void fixedUpdate()
    {
        Debug.Log(this.)
        if (!this.playerResponse)
        {
            if (Input.GetKeyDown(responseKeyCodes[0]))
            {
                Debug.Log("Hit J key");
                this.setPlayerResponse(responseKeyCodes[0]);
            }
            if (Input.GetKeyDown(responseKeyCodes[1]))
            {
                Debug.Log("Hit K key");
                this.setPlayerResponse(responseKeyCodes[1]);
            }
            if (Input.GetKeyDown(responseKeyCodes[2]))
            {
                Debug.Log("Hit L key");
                this.setPlayerResponse(responseKeyCodes[2]);
            }
        }
    }

    public bool isOnlyOnConversationCompletion()
    {
        return this.triggerWhenComplete;
    }

    public NPCDialog getNPCCountinuation()
    {
        NPCDialog defaultDialog = null;
        foreach (NPCDialog dialog in this.npcContinuations)
        {
            if (dialog.isNpcResponse() && dialog.hasPlotFilter())
            {
                return dialog;
            }
            else if (dialog.isNpcResponse())
            {
                defaultDialog = dialog;
            }
        }
        return defaultDialog;
    }

    /*
        PLOT FILTERS
    */

    public bool isNpcResponse()
    {
        return this.plotFilter ? this.plotFilter.isActive() : true;
    }

    public bool hasPlotFilter()
    {
        return this.plotFilter != null;
    }

    /*
        PLAYER RESPONSES
    */

    public PlayerDialog getPlayerResponse()
    {
        return this.playerResponse;
    }

    public bool HasPossibleResponses()
    {
        return this.playerResponses.Count > 0 || this.npcContinuations.Count > 0;
    }

    public List<PlayerDialog> getPlayerResponses()
    {
        return this.playerResponses;
    }

    void setPlayerResponse(KeyCode keyCode)
    {
        foreach (PlayerDialog dialog in this.playerResponses)
        {
            if (dialog.getKeyCode() == keyCode)
            {
                this.playerResponse = dialog;
                if (this.playerResponse.hasPlotTrigger())
                {
                    PlotTrigger trigger = this.playerResponse.getPlotTrigger();
                    trigger.getPlot().setPlotValue(trigger.getNewValue());
                }
                return;
            }
        }
    }
}