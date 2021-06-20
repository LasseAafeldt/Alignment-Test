using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class QRCode : MonoBehaviour,IInteractable
{
    public Transform partnerQR;

    [SerializeField] AlignObject objToAlign;

    [SerializeField] private float feedbackTime = .5f;
    [SerializeField] private Material interactedWithMaterial;

    private Renderer rend;
    private Material defaultMaterial;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        defaultMaterial = rend.material;
    }

    public void interact()
    {
        if (!objToAlign.isReady)
            return; //TODO:give feedback for this to users
        Debug.Log("Interacting with " + transform.name);
        StartCoroutine(VisualFeedback(feedbackTime));
        objToAlign.AlignChild(partnerQR, transform.parent);
    }

    IEnumerator VisualFeedback(float seconds)
    {
        rend.material = interactedWithMaterial;
        yield return new WaitForSeconds(seconds);
        rend.material = defaultMaterial;        
    }
}
