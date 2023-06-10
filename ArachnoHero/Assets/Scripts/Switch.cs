using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [Header("References")]
    private Animator animator;
    private Energy energy;
    private Charger charger;
    private GameObject fuse;
    private new Transform camera;

    [Header("Properties")]
    [SerializeField] private float range;
    [SerializeField] private float timeToFlip;
    public bool canUse { get; private set; }
    public bool active { get; private set; }

    // Variables
    private float timer;

    void Awake()
    {
        charger = GetComponent<Charger>();
        camera = Camera.main.transform;
    }

    void Update()
    {
        RaycastHit switchHit;
        timer += Time.deltaTime;
        if(Physics.Raycast(camera.position, camera.forward, out switchHit, range)) {
            canUse = switchHit.transform.gameObject.GetComponent<Switch>() && timer > timeToFlip;
        }
        if(Input.GetKeyDown(KeyCode.F) && canUse) {
            timer = 0;
            active = !active;
            if(active) {
                GetComponent<Animation>().Play("SwitchOn");
            } else {
                GetComponent<Animation>().Play("SwitchOff");
            }
            charger.Powered = active;
        }
    }
}
