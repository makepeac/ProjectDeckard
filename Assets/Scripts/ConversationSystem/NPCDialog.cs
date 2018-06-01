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

    private KeyCode[] responseKeyCodes = { KeyCode.J, KeyCode.K, KeyCode.L };


    PlayerDialog playerResponse;

    public PlayerDialog getPlayerResponse()
    {
        return this.playerResponse;
    }

    public bool HasPossibleResponses() {
        return this.linkedDialogOne.Count > 0;
    }

    public List<PlayerDialog> getPlayerResponses() {
        return this.linkedDialogOne;
    }

    // Use this for initialization
    void Start()
    {
        this.name = "NPC: " + npcResponse;
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
    }

    // Update is called once per frame
    void Update()
    {

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

    void setPlayerResponse(KeyCode keyCode)
    {
        foreach (PlayerDialog dialog in this.linkedDialogOne)
        {
            if (dialog.getKeyCode() == keyCode)
            {
                this.playerResponse = dialog;
                return;
            }
        }
    }
}
