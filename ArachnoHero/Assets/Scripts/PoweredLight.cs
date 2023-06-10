using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredLight : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private bool flickers;
    [SerializeField] private List<GameObject> lights;
    
    [Header("References")]
    [SerializeField] private Material offMaterial;
    [SerializeField] private Material onMaterial;
    private Charger charger;
    private new Renderer renderer;

    // Variables
    private bool wasOn;
    private bool isOn;

    void Awake()
    {
        charger = GetComponent<Charger>();
        renderer = GetComponent<Renderer>();
        isOn = charger.Powered;
    }

    void Start()
    {
        if(isOn) {
            renderer.material = onMaterial;
        } else {
            renderer.material = offMaterial;
        }
        foreach(GameObject light in lights) {
            light.SetActive(isOn);
        }
    }

    void Update()
    {
        wasOn = isOn;
        isOn = charger.Powered;
        if(wasOn != isOn) {
            if(isOn) {
                renderer.material = onMaterial;
            } else {
                renderer.material = offMaterial;
            }
            foreach(GameObject light in lights) {
                light.SetActive(isOn);
            }
        }

        if(isOn && flickers) {

        }
    }


}
