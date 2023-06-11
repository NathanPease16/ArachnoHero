using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredLight : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private bool flickers;
    [SerializeField] private List<GameObject> lights;
    
    [Header("References")]
    [SerializeField] private bool emissive;
    [SerializeField] protected Material offMaterial;
    [SerializeField] protected Material onMaterial;
    protected Charger charger;
    protected new Renderer renderer;

    // Variables
    private bool wasOn;
    private bool isOn;

    void Awake()
    {
        charger = GetComponent<Charger>();
        if(emissive) {renderer = GetComponent<Renderer>();}
        isOn = charger.powered;
    }

    void Start()
    {
        if(emissive) {
            renderer.material = (isOn ? onMaterial : offMaterial);
        }
        foreach(GameObject light in lights) {
            light.SetActive(isOn);
        }
    }

    void Update()
    {
        ManageLight();

        if(isOn && flickers) {

        }
    }

    protected virtual void ManageLight()
    {
        wasOn = isOn;
        isOn = charger.powered;
        if(wasOn != isOn) {
            if(emissive) {
                renderer.material = (isOn ? onMaterial : offMaterial);
            }
            foreach(GameObject light in lights) {
                light.SetActive(isOn);
            }
        }
    }
}
