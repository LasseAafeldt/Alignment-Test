using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float interactionRange = 5f;


    private void Update()
    {
        InteractWith();
    }

    void InteractWith()
    {
        if (Input.GetMouseButtonDown(0)) //left click
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                var selection = hit.transform;
                //Debug.Log("Interacted with " + selection.name);

                if(selection.GetComponentInChildren<IInteractable>() != null)
                {
                    selection.GetComponentInChildren<IInteractable>().interact();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        var ray = cam.ScreenPointToRay(Input.mousePosition);        
        Gizmos.DrawRay(ray.origin, ray.direction * interactionRange);
    }
}
