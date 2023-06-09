using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairUI : MonoBehaviour
{
    [Header("References")]
    private Grappling grapple;

    void Awake()
    {
        grapple = GameObject.Find("Player").GetComponent<Grappling>();
    }

    void Update()
    {

    }
}
