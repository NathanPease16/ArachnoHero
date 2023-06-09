using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float rechargeRate;

    [Header("States")]
    [SerializeField] private bool powered;
    private bool playerInRadius;

    public bool Powered { get { return powered; } set { powered = value; } }

    [Header("References")]
    private Energy energy;

    void Awake()
    {
        energy = GameObject.Find("Player").GetComponent<Energy>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRadius = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
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
