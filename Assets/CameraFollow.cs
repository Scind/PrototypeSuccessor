using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour {

    // Use this for initialization
    public Transform Player;
    public Vector3 Offset = new Vector3(0, 10f, -5.7f);
    [Range(0,10)]
    public float SmoothingTimeMouse = 2f;
    [Range(0,10)]
    public float SmoothingTimeWalk = .5f;

    const float a = 62f;
    const float b = 38f;
    Vector3 currentVelocity = Vector3.zero;
    Vector3 walkingVelocity = Vector3.zero;
    Vector3 lastMP;

	void Start () {
        //Frame the player
        //Cursor.lockState = CursorLockMode.Locked;
        transform.position = Player.position + Offset;
        lastMP = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        Cursor.lockState = CursorLockMode.Locked;
        Invoke( "unlock", 1f); // This has to be done due to a bug in Unity with delta time. 
	}

    // Update is called once per frame
    // Has to be optimized!!
    void LateUpdate()
    {

        Vector3 inputMousePos = Input.mousePosition;
        Vector3 move_to = (inputMousePos - lastMP);
        float helper = move_to.y;
        move_to.y = move_to.z;
        move_to.z = helper;
        move_to += transform.position;


        Offset += Vector3.SmoothDamp(transform.position, move_to, ref currentVelocity, SmoothingTimeMouse) - transform.position;
        transform.position = Vector3.SmoothDamp(transform.position,Player.position + Offset, ref walkingVelocity, SmoothingTimeWalk);


        lastMP = Input.mousePosition;
    }

    void unlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }
}

