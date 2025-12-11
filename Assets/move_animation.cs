using UnityEngine;
using UnityEngine.InputSystem;

public class move_animation : MonoBehaviour
{
    public Animator animator;
    public void OnMove(InputAction.CallbackContext CTX)
    {
        if (CTX.performed)
        {
            animator.SetBool("Moving", true);
        }
        else if (CTX.canceled) 
        {
            animator.SetBool("Moving", false);
        }
    }
}
