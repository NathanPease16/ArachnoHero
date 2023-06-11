using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredDoor : MonoBehaviour
{
    [Header("References")]
    private Animator animator;
    protected Charger charger;

    // Variables
    private bool wasOn;
    private bool isOn;

    void Awake()
    {
        animator = GetComponent<Animator>();
        charger = GetComponent<Charger>();
        isOn = charger.powered;
    }

    void Start()
    {
        if(isOn) {
            Debug.Log("aaa");
            animator.Play("Open");
        } else {
            animator.Play("Close");
        }
    }

    void Update()
    {
        wasOn = isOn;
        isOn = charger.powered;
        if(wasOn != isOn) {
            if(isOn) {
                Debug.Log("aaa");
                animator.Play("Open");
            } else {
                animator.Play("Close");
            }
        }
    }
}
