using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DialogUpdateEditor : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        NPCDialog npcDialog = this.gameObject.GetComponent<NPCDialog>();
        if (npcDialog)
        {
            NPC npc = this.gameObject.GetComponentInParent<Conversation>().getNPC();
            string npcName = npc ? npc.getName() : "NPC";
            this.name = npcName + ": " + npcDialog.npcResponse;
        }
        PlayerDialog playerDialog = this.gameObject.GetComponent<PlayerDialog>();
        if (playerDialog)
        {
            this.name = "PC:" + playerDialog.dialogOne;
        }
    }
}
