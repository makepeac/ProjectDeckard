using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialog : MonoBehaviour
{
    [Header("Option 1")]
    [TextArea]
    public string dialogOne = "";
    [SerializeField]
    List<NPCDialog> npcResponse = new List<NPCDialog>();

    [SerializeField]
    KeyCode keycode = KeyCode.Underscore;

    private PlotTrigger plotTrigger;

    // Use this for initialization
    void Start()
    {
        this.plotTrigger = this.GetComponent<PlotTrigger>();
        if (this.npcResponse.Count < 1)
        {
            foreach (Transform child in this.transform)
            {
                NPCDialog dialog = child.GetComponent<NPCDialog>();
                if (dialog)
                {
                    this.npcResponse.Add(dialog);
                }
            }
        }
    }

    /*
        NPC Response
    */

    public NPCDialog getNpcResponse()
    {
        NPCDialog defaultDialog = null;
        foreach (NPCDialog dialog in this.npcResponse)
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
        Plot Trigger
    */

    public bool hasPlotTrigger()
    {
        return this.plotTrigger != null;
    }

    public PlotTrigger getPlotTrigger()
    {
        return this.plotTrigger;
    }

    /*
        Key Code
    */

    public void setKeyCode(KeyCode keycode)
    {
        this.keycode = keycode;
    }

    public KeyCode getKeyCode()
    {
        return this.keycode;
    }
}