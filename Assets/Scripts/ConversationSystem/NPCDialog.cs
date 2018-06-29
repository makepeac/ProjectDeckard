using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    [Header("Option 1")]
    [TextArea]
    public string npcResponse = "";
    [SerializeField]
    List<PlayerDialog> linkedDialogOne = new List<PlayerDialog>();

    private KeyCode[] responseKeyCodes = { KeyCode.A, KeyCode.W, KeyCode.D };

    PlayerDialog playerResponse;

    [SerializeField]
    PlotFilter plotFilter = null;

    // Use this for initialization
    void Start()
    {
        this.linkedDialogOne = new List<PlayerDialog>();
        int i = 0;
        foreach (Transform child in this.transform)
        {
            PlayerDialog dialog = child.GetComponent<PlayerDialog>();
            if (dialog)
            {
                dialog.setKeyCode(responseKeyCodes[i]);
                i += 1;
                this.linkedDialogOne.Add(dialog);
            }
        }
        this.plotFilter = this.GetComponent<PlotFilter>();
    }

    public void fixedUpdate()
    {
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
        return this.linkedDialogOne.Count > 0;
    }

    public List<PlayerDialog> getPlayerResponses()
    {
        return this.linkedDialogOne;
    }

    void setPlayerResponse(KeyCode keyCode)
    {
        foreach (PlayerDialog dialog in this.linkedDialogOne)
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