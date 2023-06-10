using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [Header("References")]
    private new Transform camera;

    [Header("Properties")]
    [SerializeField] private float range;

    public delegate void Use(GameObject obj);
    public static event Use OnUse;

    void Awake() {
        camera = Camera.main.transform;
    }

    void Update() {
        RaycastHit hit;
        if(Input.GetKeyDown(KeyCode.F) && Physics.Raycast(camera.position, camera.forward, out hit, range)) {
            OnUse(hit.transform.gameObject);
        }
    }
}
