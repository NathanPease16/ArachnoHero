using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float distance;

    [Header("States")]
    private float startY;
    private float currentTime;

    void Awake()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        float y = (0.5f * Mathf.Sin(speed * Mathf.PI * (currentTime - 0.5f)) + 0.5f) * distance;
        currentTime += Time.deltaTime;

        transform.position = new Vector3(transform.position.x, startY + y, transform.position.z);
    }
}
