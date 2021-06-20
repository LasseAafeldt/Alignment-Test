using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignObject : MonoBehaviour
{
    public Transform qrParent;
    public bool isReady = true;
    [SerializeField] private float animateTime = 1f;

    public void AlignChild(Transform childToAlign, Transform targetAlignment)
    {
        Debug.Log("Aligning... " + childToAlign.name + " and " + targetAlignment.name);
        isReady = false;

        //find desired child look rotation
        Quaternion desiredChildLookRotation = targetAlignment.rotation * Quaternion.Inverse(childToAlign.rotation);
        // add parent rotation
        desiredChildLookRotation = desiredChildLookRotation * transform.rotation;

        //find distance to move vector
        Quaternion temp = transform.rotation;
        transform.rotation = desiredChildLookRotation;
        //find vector from child to target
        Vector3 newPosVector = targetAlignment.position - childToAlign.position;
        //reset rotation
        transform.rotation = temp;

        //apply rotation
        StartCoroutine(AlignObjectSmooth(childToAlign, targetAlignment, desiredChildLookRotation, newPosVector, animateTime));

        
    }

    private IEnumerator AlignObjectSmooth(Transform toAlign, Transform target, Quaternion desiredChildLookRotation, Vector3 desiredPosition, float seconds)
    {        
        float completedValue = 0f;
        Vector3 initialPos = transform.position;
        Quaternion initialRot = transform.rotation;

        while(completedValue != 1)
        {
            float increment =  1 / seconds * Time.deltaTime;
            completedValue += increment;
            completedValue = Mathf.Clamp01(completedValue);

            //apply desired rotation and position
            transform.rotation = Quaternion.Lerp(initialRot, desiredChildLookRotation, completedValue);
            transform.position = Vector3.Lerp(initialPos, desiredPosition + initialPos, completedValue);
            yield return new WaitForEndOfFrame();
        }
        isReady = true;
    }
}
