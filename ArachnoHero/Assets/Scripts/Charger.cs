using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    [Header("Attributes")]
    private new Animation animation;
    [SerializeField] private float rechargeRate;
    [SerializeField] private float timeToOn;
    [SerializeField] private bool isCheckpoint;
    [SerializeField] private Vector3 respawnPosition;

    [Header("States")]
    public bool hasPower;
    public bool powered;
    private bool playerInRadius;
    private bool hasSparked;

    [Header("References")]
    [SerializeField] private List<GameObject> parentChargers;
    private ParticleSystem sparks;
    private Energy energy;

    // Variables
    private float timer;

    void Awake()
    {
        energy = GameObject.Find("Player").GetComponent<Energy>();
        sparks = GameObject.Find("Sparks").GetComponent<ParticleSystem>();
        if(GetComponent<Animation>()) {animation = GetComponent<Animation>();}
        timer = 0;
    }

    void Start()
    {
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
            if(isCheckpoint)
                energy.respawnPosition = respawnPosition;
        }

        if(parentChargers.Count == 0) {
            bool wasPowered = powered;
            timer += (hasPower ? 1 : -1)*Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, 2*timeToOn);
            powered = hasPower && timer >= timeToOn;
        } else if(parentChargers.Count == 1) {
            bool wasPowered = powered;
            bool ready = parentChargers[0].GetComponent<Charger>().powered && hasPower;
            timer += (ready ? 1 : -1)*Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, 2*timeToOn);
            powered = ready && timer >= timeToOn;
            if(ready && !powered) 
            {
                if (animation != null)
                    animation?.Play("Start");
                if (sparks != null && !hasSparked)
                {
                    hasSparked = true;
                    sparks.Play();
                }
            }
            if(powered && animation != null) {animation.Play("Active");}
            if(wasPowered != powered) 
            {
                if (animation != null)
                {
                    animation.Stop(); 
                    animation.Play("Stop");
                }
                if (sparks != null)
                    hasSparked = false;
            }
        } else if(parentChargers.Count > 1) {
            bool wasPowered = powered;
            bool allPowered = true;
            foreach(GameObject parent in parentChargers) {
                if(!parent.GetComponent<Charger>().powered) {allPowered = false;}
            }
            bool ready = allPowered && hasPower;
            timer += (ready ? 1 : -1)*Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, 2*timeToOn);
            powered = ready && timer >= timeToOn;
            if(ready && !powered) 
            {
                if (animation != null)
                    animation.Play("Start");
                if (sparks != null && !hasSparked)
                {
                    sparks.Play();
                    hasSparked = true;
                }
            }
            if(powered && animation != null) {animation.Play("Active");}
            if(wasPowered != powered) 
            {
                if (animation != null)
                {
                    animation.Stop(); 
                    animation.Play("Stop");
                }
                if (sparks != null)
                    hasSparked = false;
            }
        }
    }
}
