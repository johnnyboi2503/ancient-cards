using UnityEngine;
using UnityEngine.Events;

public interface Interactable
{
    void Interacted();
    //put whatever you want to happen when you interact with an object in the function
    /* add Interactable to the class of the component or it wont work, the tri example shows how it works
     * public class Lever : MonoBehaviour, Interactable
     * {
     *     public void Interacted()
     *     {
     *          Debug.Log("You pulled the lever!");
     *     }
    }*/

}
