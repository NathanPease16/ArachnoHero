using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float rechargeRate;
    [SerializeField] private float timeToOn;

    [Header("States")]
    [SerializeField] private bool powered;
    private bool playerInRadius;

    public bool Powered { get { return powered; } set { powered = value; } }

    [Header("References")]
    [SerializeField] private GameObject parentCharger;
    private Energy energy;

    // Variables
    private float timer;

    void Awake()
    {
        energy = GameObject.Find("Player").GetComponent<Energy>();
        timer = 0;
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

        if(parentCharger != null) {
            timer += Time.deltaTime;

            if(timer > timeToOn) {powered = parentCharger.GetComponent<Charger>().Powered;}
        }
    }
}
