using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButton : MonoBehaviour
{
    [SerializeField] private int id; //TODO: make sure there can't be multiple of the same id
    DisplayData display;

    private void Awake()
    {
        display = GetComponentInParent<DisplayData>();
        if (display == null)
            Debug.LogError("The parent of " + transform.name + " is missing a DisplayData compenent"); //TODO: should implement ping to gameobject here
    }

    private void OnMouseDown()
    {
        display.OnDisplayData(id);
    }
}
