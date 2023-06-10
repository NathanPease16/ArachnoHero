using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBox : MonoBehaviour
{
    [Header("References")]
    private Animator animator;
    private Energy energy;
    private Charger charger;
    private GameObject fuse;

    void Awake()
    {
        animator = GetComponent<Animator>();
        energy = GameObject.Find("Player").GetComponent<Energy>();
        charger = GetComponent<Charger>();
        fuse = transform.Find("Fuse").gameObject;
    }

    void Start()
    {
        fuse.SetActive(false);
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.Play("ElectricalBox_Open");

            if (energy.HasFuse)
                StartCoroutine(OpenWithFuse());
            else
                animator.Play("ElectricalBox_Open");
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.Play("ElectricalBox_Close");
        }
    }

    private IEnumerator OpenWithFuse()
    {
        animator.Play("ElectricalBox_Open");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        fuse.SetActive(true);
        
        charger.hasPower = true;
    }
}
