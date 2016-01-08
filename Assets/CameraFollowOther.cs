using UnityEngine;
using System.Collections;

public class CameraFollowOther : MonoBehaviour {

    // Use this for initialization
    public float SmoothTime = 1f;
    public float DesiredYPosition = 15;
    public bool UnsmoothedMovement;

    Vector3 currentVelocity = Vector3.zero;    
    void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (Input.mousePosition.x < 10 || Input.mousePosition.x >= Screen.width -10 || Input.mousePosition.y < 10 || Input.mousePosition.y >= Screen.height - 10)
        {
            float deltax = Input.GetAxis("Mouse X");
            float deltay = Input.GetAxis("Mouse Y");


            Vector3 to_move = new Vector3(deltax, -transform.position.y + DesiredYPosition, deltay); // We want the camera to move to DesiredYPosition. 

            if (!UnsmoothedMovement)
            {
                transform.position = Vector3.SmoothDamp(transform.position, to_move + transform.position, ref currentVelocity, SmoothTime);
            }

            else
            {
                transform.position = Vector3.Lerp(transform.position, to_move + transform.position, .03f);
            }
        }

	}
}
