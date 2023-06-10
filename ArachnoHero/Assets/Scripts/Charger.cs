using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    [Header("Attributes")]
    private new Animation animation;
    [SerializeField] private float rechargeRate;
    [SerializeField] private float timeToOn;

    [Header("States")]
    private bool powered;
    public bool hasPower;
    private bool playerInRadius;

    public bool Powered { get { return powered; } set { powered = value; } }
    public bool HasPower { get { return powered; } set { powered = value; } }

    [Header("References")]
    [SerializeField] private List<GameObject> parentChargers;
    private Energy energy;

    // Variables
    private float timer;

    void Awake()
    {
        energy = GameObject.Find("Player").GetComponent<Energy>();
        if(GetComponent<Animation>()) {animation = GetComponent<Animation>();}
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
        if(parentChargers.Count == 0) {
            powered = hasPower;
        } else if(parentChargers.Count == 1) {
            bool ready = parentChargers[0].GetComponent<Charger>().hasPower && hasPower;
            timer += (ready ? 1 : -1)*Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, 2*timeToOn);
            powered = timer >= timeToOn;
            if(ready && !powered && animation != null) {animation.Play("Start");}
            if(powered && animation != null) {animation.Play("Active");}
        } else if(parentChargers.Count > 1) {
            bool allPowered = true;
            foreach(GameObject parent in parentChargers) {
                if(!parent.GetComponent<Charger>().hasPower) {allPowered = false;}
            }
            bool ready = allPowered && hasPower;
            timer += (ready ? 1 : -1)*Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, 2*timeToOn);
            powered = timer >= timeToOn;
            if(ready && !powered && animation != null) {animation.Play("Start");}
            if(powered && animation != null) {animation.Play("Active");}
        }
    }
}
