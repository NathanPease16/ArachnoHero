using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairUI : MonoBehaviour
{
    [Header("References")]
    private Grappling grapple;
    private Animation anim;

    // Variables
    private bool isLocked;

    void Awake()
    {
        grapple = GameObject.Find("Player").GetComponent<Grappling>();
        anim = GetComponent<Animation>();
        isLocked = false;
    }

    void Update()
    {
        bool wasLocked = isLocked;
        isLocked = grapple.CanGrapple();
        if(grapple.IsGrappling()) {
            anim.Play("CrosshairActive");
        } else {
            if(wasLocked != isLocked) {
                if(isLocked) {anim.Play("CrosshairLock");}
                else {anim.Play("CrosshairUnlock");}
            }
        }
    }
}
