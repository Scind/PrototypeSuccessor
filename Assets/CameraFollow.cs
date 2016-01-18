using UnityEngine;


public class CameraFollow : MonoBehaviour
{

    public Transform Player;
    public Vector3 BaseOffset = new Vector3(0, 10, -5.7f);
    public float MouseOffsetFactor = 4;
    public float CamSpeedMax = 5;
    public float CamSpeedMin = 2;
    public float CamSpeedFactor = 0.8f;


    // Use this for initialization
    void Start()
    {

        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 MouseOffset = new Vector3(2 * (mousePos.x / Screen.width) - 1, 0f, 2 * (mousePos.y / Screen.height) - 1);
        Vector3 Offset = MouseOffset * MouseOffsetFactor + BaseOffset;

        float Camspeed = CamSpeedFactor * Vector3.Distance(transform.position, Player.position);
        if (Camspeed < CamSpeedMin) Camspeed = CamSpeedMin;
        if (Camspeed > CamSpeedMax) Camspeed = CamSpeedMax; 
        transform.position = Vector3.Lerp(transform.position, Player.position + Offset, Camspeed * Time.deltaTime);

    }

}
