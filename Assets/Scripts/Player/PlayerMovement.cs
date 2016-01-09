using UnityEngine;
using System.Collections;
using System;

public static class MouseButton
{
    public const int Left = 0;
    public const int Right = 1;
    public const int Middle = 2;
}


/// <summary>
/// TODO: Optimize this script as it is the most performance intensive.
/// </summary>
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    float camRayLength = 500f;
    int floorMask;
    Animator anim;
    CharacterController cc;
    bool walking = false;

    Vector3 destination;

    [Tooltip("If the player's position has a distance of BreakRange to its destination, stop the movement.")]
    public float BreakRange = .3f;
    [Tooltip("How fast should the player walk? Correlates with Fps. If you want to make him walk faster, please use this, not Fps. NOTE: Currently not DeltaTime buffered")]
    public float Speed = 5f;
    [Range(0, 100)]
    [Tooltip("How fast should the player turn? NOTE: Currently not DeltaTime buffered")]
    public float TurnSpeed = 5f;
    public float Gravity = 9.81f;

    public Transform GotoIndicator; // Just for debugging


    float sqrBreakRange { get { return BreakRange * BreakRange; } }
    public bool Walking { get { return walking; } }

    Rigidbody rb;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        destination = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.detectCollisions = false;
    }

    void Update()
    {
        moveandturn();
        animate();
    }

    void moveandturn()
    {
        //Copied form the Survival Shooter
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            //End copy
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerToMouse), Time.deltaTime * TurnSpeed); //Smoothly turn to the position. However Time.deltaTime should be applied.


            if (Input.GetMouseButton(MouseButton.Left))
            {
                destination = floorHit.point;
                walking = true;
            }

        }

        if (walking)
        {
            moveToDestination();
        }

    }

    void moveToDestination()
    {
        // get the movement vector
        Vector3 to_move = (destination - transform.position);
        if (to_move.sqrMagnitude <= sqrBreakRange) // Test if movement should be stopped
        {
            walking = false;
        }
        else
        {
            cc.SimpleMove(to_move.normalized * Speed);
            walking = true;
        }
    }


    void animate()
    {
        anim.SetBool("IsWalking", walking);
    }

}
