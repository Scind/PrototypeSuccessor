using UnityEngine;
using System;
using Assets.ExtensionMethods;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    // Use this for initialization
    // Works best with <= 60 fps. 
    public Transform Player;
    public Vector3 Offset = new Vector3(0, 10f, -5.7f);
    [Range(0, 10f)]
    public float SmoothingTimeMouse = 2f;
    [Range(0, 10)]
    public float SmoothingTimeWalk = .5f;
    
    Vector3 currentVelocity = Vector3.zero;
    Vector3 walkingVelocity = Vector3.zero;
    Vector3 lastMP;

    void Start()
    {
        //Frame the player
        transform.position = Player.position + Offset; // Initial Offset;
        lastMP = new Vector3(Screen.width / 2f, Screen.height / 2f, 0); // Set the first mouse Position to the middle of the screen.
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor in the mid;
        Invoke("unlock", 1f);
    }

    // Has to be optimized!! For now I let it run in FixedUpdate because we can't afford to lose mouse_deltas. Things get buggy.
    void FixedUpdate()
    {
        Vector3 inputMousePos = Input.mousePosition;
        Vector3 mouseDelta = (inputMousePos - lastMP); // Get the delta
        float helper = mouseDelta.y;
        mouseDelta.y = mouseDelta.z;
        mouseDelta.z = helper; // Change the z and the y axis.
        //Offset += Vector3.SmoothDamp(Vector3.zero, mouseDelta, ref currentVelocity, SmoothingTimeMouse);

        Offset += Vector3.Lerp(Vector3.zero, mouseDelta, SmoothingTimeMouse*Time.fixedDeltaTime);
        lastMP = Input.mousePosition;
    }

    void LateUpdate()
    {
       transform.position = Vector3.SmoothDamp(transform.position, Player.position + Offset, ref walkingVelocity, SmoothingTimeWalk);
    }

    void unlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

