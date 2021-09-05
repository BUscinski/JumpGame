using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-100)]
public class CameraLooseFollow : MonoBehaviour
{
    [SerializeField] private float posLerpCoeff = 0.05f;
    [SerializeField] private float lookAtLerpCoeff = 0.1f;
    [SerializeField] private GameObject objectToTrack;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 lookAtOffset;
    private Vector3 lastFinalLookAtPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 lookAtTargetPosition = objectToTrack.transform.position + lookAtOffset;
        Vector3 cameraTargetPosition = objectToTrack.transform.position + positionOffset;
        this.transform.position = Vector3.Lerp(this.transform.position, cameraTargetPosition, posLerpCoeff);
        Vector3  finalLookAtPosition = Vector3.Lerp(lastFinalLookAtPosition, lookAtTargetPosition, lookAtLerpCoeff);
        lastFinalLookAtPosition = finalLookAtPosition;
        this.transform.LookAt(lastFinalLookAtPosition);
    }
}
