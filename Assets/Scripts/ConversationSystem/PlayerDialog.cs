using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialog : MonoBehaviour
{
    [Header("Option 1")]
    [TextArea]
    public string dialogOne = "";
    [SerializeField]
    NPCDialog linkedDialogOne;

    [SerializeField]
    KeyCode keycode = KeyCode.Underscore;

    public NPCDialog getNpcResponse()
    {
        return this.linkedDialogOne;
    }
    // Use this for initialization
    void Start()
    {
        foreach (Transform child in this.transform)
        {
            NPCDialog dialog = child.GetComponent<NPCDialog>();
            if (dialog)
            {
                this.linkedDialogOne = dialog;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setKeyCode(KeyCode keycode)
    {
        this.keycode = keycode;
    }

    public KeyCode getKeyCode()
    {
        return this.keycode;
    }
}
