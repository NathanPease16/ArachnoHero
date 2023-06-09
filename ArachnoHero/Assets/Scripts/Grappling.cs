using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    /*
        THIS IS A GRAPPLE ABILITY. IT USES UNITY'S BUILT IN PHYSICS ENGINE TO SIMULATE
        PHYSICS. 
    */
    
    [Header("Attributes")]
    [SerializeField] private float maxGrappleDist;
    [SerializeField] private float grappleStrength;
    [SerializeField] private float energyUsed;

    [Header("References")]
    private new Rigidbody rigidbody;
    private new Transform camera;
    private Energy energy;
    private LineRenderer line;

    // Variables
    private Vector3 grapplePoint;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = Camera.main.transform;
        energy = GetComponent<Energy>();
        line = GetComponent<LineRenderer>();

        grapplePoint = Vector3.zero;
    }

    void Update()
    {
        Grapple();
        GrappleLine();
    }

    private void Grapple()
    {
        RaycastHit hit;
        // Start/stop grapple
        if(grapplePoint == Vector3.zero) {
            // Start grapple
            if(Input.GetMouseButtonDown(1) && energy.HasEnoughEnergy(energyUsed)) {
                if(Physics.Raycast(origin: transform.position, direction: camera.forward, hitInfo: out hit, maxDistance: maxGrappleDist)) {
                    grapplePoint = hit.point;
                    line.positionCount = 2;
                } else {
                    Debug.Log("Out of range!");
                }
            }
        } else {
            // Handle grapple
            Vector3 toPoint = grapplePoint - transform.position;
            rigidbody.AddForce(toPoint * grappleStrength);

            // Stop grapple
            if(Input.GetMouseButtonDown(1)) {
                grapplePoint = Vector3.zero;
                line.positionCount = 0;
            }
        }
    }

    private void GrappleLine()
    {
        if(grapplePoint != Vector3.zero) {
            line.SetPositions(new Vector3[] {transform.position, grapplePoint});
        }
    }
}
