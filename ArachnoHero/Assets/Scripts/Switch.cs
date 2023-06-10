using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Interact interact;
    private Animator animator;
    private Charger charger;
    private new Transform camera;

    [Header("Properties")]
    [SerializeField] private float timeToFlip;
    public bool canUse { get; private set; }
    public bool active { get; private set; }

    // Variables
    private float timer;

    void Awake()
    {
        charger = GetComponent<Charger>();
        camera = Camera.main.transform;
        Interact.OnUse += UseSwitch;
        charger.hasPower = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void UseSwitch(GameObject obj)
    {
        if(obj == gameObject && timer > timeToFlip) {
            timer = 0;
            active = !active;
            if(active) {
                GetComponent<Animation>().Play("SwitchOn");
            } else {
                GetComponent<Animation>().Play("SwitchOff");
            }
            charger.hasPower = active;
        }
    }
}
