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
    [SerializeField] private float cooldown;

    [Header("States")]
    private float currentTime;

    [Header("References")]
    private new Rigidbody rigidbody;
    private new Transform camera;

    void Awake()
    {
        currentTime = cooldown;
        rigidbody = GetComponent<Rigidbody>();
        camera = Camera.main.transform;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        Debug.Log(currentTime);

        Shock();
    }

    private void Shock()
    {
        if (Input.GetMouseButtonDown(0) && currentTime >= cooldown)
        {
            currentTime = 0;
            rigidbody.AddForce(-camera.forward * shockForce, ForceMode.Force);
        }
    }
}
