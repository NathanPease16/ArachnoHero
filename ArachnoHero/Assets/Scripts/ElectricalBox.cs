using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBox : MonoBehaviour
{
    [Header("Attrib utes")]
    [SerializeField] private float rechargeRate;

    [Header("States")]
    [SerializeField] private bool powered;
    [SerializeField] private bool playerInRadius;

    [Header("References")]
    private Animator animator;
    private Energy energy;

    void Awake()
    {
        animator = GetComponent<Animator>();
        energy = GameObject.Find("Player").GetComponent<Energy>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.Play("ElectricalBox_Open");
            playerInRadius = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.Play("ElectricalBox_Close");
            playerInRadius = false;
        }
    }

    void Update()
    {
        if (playerInRadius && powered)
        {
            energy.Charge(rechargeRate * Time.deltaTime);
        }
    }
}
