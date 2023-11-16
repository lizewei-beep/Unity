using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class sign : MonoBehaviour
{
    public GameObject signSprite;
    private IInteractable targetItem;



   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Interactable"))
        {
            Debug.Log("6666!");
            targetItem = other.GetComponent<IInteractable>();
            targetItem.TriggerAction();
 
        }
    }

}
