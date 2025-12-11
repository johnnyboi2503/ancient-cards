using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class movement : MonoBehaviour
{
    public Vector2 LookInput;
    public Vector2 MoveInput;
    public float Jump;
    public bool HasJumped;
    public float Speed;
    public float Sensitivity;
    public Rigidbody RB;
    public void OnLook(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            LookInput = ctx.ReadValue<Vector2>();
            RotatePlayer();
        }
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
         MoveInput = ctx.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!HasJumped) 
        {
            HasJumped = true;
            RB.linearVelocity = new Vector3 (RB.linearVelocity.x, Jump, RB.linearVelocity.z);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        HasJumped = false;
    }
    public void FixedUpdate()
    {
        MovePlayer();
    }
    public void RotatePlayer()
    {

        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + (LookInput.x * Sensitivity), 0);
    }
    public void MovePlayer()
    {
        Vector3 Direction = (transform.forward * MoveInput.x * Speed) + (transform.right * -MoveInput.y * Speed);
        RB.linearVelocity = new Vector3 (Direction.x,RB.linearVelocity.y,Direction.z);
    }
}
