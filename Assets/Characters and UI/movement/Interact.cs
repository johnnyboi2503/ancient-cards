using UnityEngine;

public class Interact : MonoBehaviour
{
    public Collider InteractCollider;
    public Interactable InteractableOBJscript;
    public void OnTriggerStay(Collider collision)
    {
        if(collision.GetComponent<Interactable>() != null)
        {
            InteractableOBJscript = collision.GetComponent<Interactable>();
            Debug.Log(collision.name + " entered");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        InteractableOBJscript = null;
    }
    public void interact()
    {
        if(InteractableOBJscript != null)
        {
            InteractableOBJscript.Interacted();//dont change the name of the function in the interactable scripts AKA Keep it as Interacted();
            Debug.Log("interaction");
        }
    }
}
