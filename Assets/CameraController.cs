using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//if you aren't getting highlights, make sure your folder is selected
public class CameraController : MonoBehaviour
{
    public float dampTime = 30.0f;
    private Vector3 velocity = Vector3.zero;

    public GameObject player; //Player game object
    public GameObject Archibald;//Testing object for conversation
    private GameObject CamTarget; //Target for Camera
    private GameObject CamLight; //Reference to spotlight child of main camera
    private Vector3 offset; //Camera offset distance
    private Vector3 DefaultOffset; //Snap-back for offset
    public Vector3 vTarget; //Testing variable for camera focus
    private int iCameraState = 1; //Camera State
    private bool iCameraChange = false; //Switch for Update to check for a camera state change

    Vector3 CenterCameraLockVector(Vector3 playerpoint, Vector3 Targetpoint)
    {
        Vector3 vReturnMedian;
        vReturnMedian.x = Mathf.Lerp(playerpoint.x, Targetpoint.x, 0.5f);
        vReturnMedian.y = Mathf.Lerp(playerpoint.y, Targetpoint.y, 0.5f);
        vReturnMedian.z = Mathf.Lerp(playerpoint.z, Targetpoint.z, 0.5f);
        return vReturnMedian;
    }
    Vector3 CenterCameraLockObject(Vector3 playerpoint, GameObject TargetObject)
    {
        Vector3 vReturnMedian;
        vReturnMedian.x = Mathf.Lerp(playerpoint.x, TargetObject.transform.position.x, 0.5f);
        vReturnMedian.y = Mathf.Lerp(playerpoint.y, TargetObject.transform.position.y, 0.5f);
        vReturnMedian.z = Mathf.Lerp(playerpoint.z, TargetObject.transform.position.z, 0.5f);
        return vReturnMedian;
    }


    //Public switch case with inputs(?? Might need more than one?)
    public void CFocusObject(GameObject oCamTarget)
    {
        iCameraState = 1;
        CamTarget = oCamTarget;
        iCameraChange = true;
    }
    public void CFocusVector(Vector3 vCamTarget)
    {
        iCameraState = 2;
        vTarget = vCamTarget;
        iCameraChange = true;
    }
    public void CMedianVector(Vector3 vCamTarget)
    {
        iCameraState = 3;
        vTarget = vCamTarget;
        iCameraChange = true;
    }
    public void CMedianObject(GameObject oCamTarget)
    {
        iCameraState = 4;
        CamTarget = oCamTarget;
        iCameraChange = true;
    }

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
        DefaultOffset = offset;
    }


    //Start work here
    //Test zoom / offset of camera
    //Investigate opacity / particle effects / other effects on the camera
    //Zoom for walking up doorways?
    //Multilayers of view in proximity, but not outside?
    //Visual effect on the edges of blurring?
    //Colour opacity filter for moods?
    private void Update()
    {

        //These are input debugging for the below values. Must make a public subroutine for each "setting" so we can modularize it.
        if (Input.GetKeyDown(KeyCode.Keypad1)) //Focus player
        {
            iCameraState = 1;
            CamTarget = player;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) //Changes CamTarget between player and archibald
        {
            iCameraState = 2;
            if (CamTarget == player)
            {
                CamTarget = Archibald;
            }
            else
            {
                CamTarget = player;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) //Median between player & (vTarget)
        {
            iCameraState = 3;
            vTarget = Archibald.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4)) //Tethers camera to a median between player and (CamTarget)
        {
            iCameraState = 4;
            if (CamTarget == player)
            {
                CamTarget = Archibald;
            }
            else
            {
                CamTarget = player;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad5)) //Focuses on a specific vector (vTarget)
        {
            iCameraState = 5;
        }
        if (Input.GetKeyDown(KeyCode.Keypad6)) //Focus on a target Object
        {
            iCameraState = 6;
        }

    }

    //Colour opacity effect?
    //Jumps between the player & the centerpoint between two points
    //iCameraState = 3; //Make a variable here for status of zoom



    // Update is called once per frame
    private void LateUpdate()
    {
        switch (iCameraState)
        {
            //Debug.Log("AT CONVERSATION STATE FUNCTION");
            case 1: //Focus on a target Object
                transform.position = CamTarget.transform.position + offset;
                break;

            case 2: //Focus on a target Vector
                transform.position = vTarget + offset;
                break;

            case 3: //Conversation Mode (Vector), tethers camera to a median between player and vector
                transform.position = CenterCameraLockVector(player.transform.position, vTarget) + offset;
                break;

            case 4: //Conversation Mode (Object), tethers camera to a median between player and object
                //transform.position = CenterCameraLockObject(player.transform.position, CamTarget) + offset;
                //transform.position = Vector3.Lerp(transform.position, CamTarget.transform.position + offset, dampTime * Time.smoothDeltaTime);//This works but is slow
                transform.position = Vector3.SmoothDamp(player.transform.position + offset, CenterCameraLockObject(player.transform.position, CamTarget) + offset, ref velocity, 0.3f);
                break;
        }
        //Standard Camera + Zoom
        //transform.position = CamTarget.transform.position + (offset / 2);
        /*
                if (bTrackObject == true)//Tracking gameobject or vector
                {
                    transform.position = oTarget.transform.position + offset;//Template for change?
                }
                else
                {
                    //transform.position = vTarget + offset; //This is for jumping to a particular target vector
                    transform.position = CenterCameraLock(player.transform.position, Archibald.transform.position) + offset; //Jumping to the medium point between two objects
                }
        */
        //transform.position = player.transform.position + offset;//Standard behavior
        /*        switch (iCameraState) //Old testing system
                {
                    case 1: //Standard follow Camera
                        transform.position = player.transform.position + offset;
                        break;
                    case 2: //Focus a seperate character (Archibald)
                        transform.position = Archibald.transform.position + offset;
                        break;
                    case 3: //Lock camera focus on current point
                        break;
                    case 4: //Zoom in
                        transform.position = player.transform.position + offset;
                        break;
        */
    }
}