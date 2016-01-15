using UnityEngine;


public class LerpCameraFollow : MonoBehaviour
{

    public Transform Player;
    public Vector3 Offset = new Vector3(0, 10, -5.7f);
    public float MouseMoveSpeed = 1f;
    public float MoveSpeed = 1f;
    public float Insensitivity = 5f;

    Vector3 lastMP = Vector3.zero;

    // Use this for initialization
    void Start()
    {

        transform.position = Player.position + Offset; // Initial Offset;
        lastMP = new Vector3(Screen.width / 2f, Screen.height / 2f, 0); // Set the first mouse Position to the middle of the screen.
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor in the mid;
        Invoke("unlock", 1f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 mousePos = Input.mousePosition;

        
        Vector3 mouseDelta = (mousePos - lastMP);
        float helper = mouseDelta.y;
        mouseDelta.y = mouseDelta.z;
        mouseDelta.z = helper; // Change the z and the y axis.
        Offset += Vector3.Lerp(Vector3.zero, mouseDelta, MouseMoveSpeed * Time.deltaTime); // Compute offset based on the mouseDelta; More finegrained version;
        transform.position = Vector3.Lerp(transform.position, Player.position + Offset, MoveSpeed * Time.deltaTime);
        lastMP = mousePos;

    }

    void unlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
