using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    /*
        THIS IS A SHOCKWAVE ABILITY. IT USES UNITY'S BUILT IN PHYSICS ENGINE TO SIMULATE
        PHYSICS. 
    */

    [Header("Attributes")]
    [SerializeField] private float shockForce;
    [SerializeField] private float energyUsed;
    [SerializeField] private Vector3 weight;

    [Header("References")]
    private new Rigidbody rigidbody;
    private new Transform camera;
    private Energy energy;

    // Variables
    private float currentTime;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        energy = GetComponent<Energy>();
        camera = Camera.main.transform;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > 1f) {Shock();}
    }

    private void Shock()
    {
        bool mouse = Input.GetMouseButtonDown(0);
        bool shift = Input.GetKeyDown(KeyCode.LeftShift);

        if ((mouse || shift) && energy.HasEnoughEnergy(energyUsed))
        {
            Vector3 weightedForward = new Vector3(camera.forward.x * weight.x, camera.forward.y * weight.y, camera.forward.z * weight.z);
            
            if (mouse)
                rigidbody.AddForce(-weightedForward * shockForce, ForceMode.Force);
            else
                rigidbody.AddForce(weightedForward * shockForce, ForceMode.Force);

            energy.UseEnergy(energyUsed);
        }
    }
}
