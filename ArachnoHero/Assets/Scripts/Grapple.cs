using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    // References
    private new Rigidbody rigidbody;

    // Attributes
    [Header("Grappling")]
    [SerializeField] private float maxGrappleDist;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

}
