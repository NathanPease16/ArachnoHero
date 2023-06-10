using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleEnergyEffect : MonoBehaviour
{
    [Header("References")]
    private ParticleSystem particles;
    private LineRenderer line;

    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        line = transform.parent.parent.GetComponent<LineRenderer>();
    }

    void Update()
    {
        Render();
    }

    private void Render()
    {
        if (line.positionCount < 2) { particles.Stop(); return; }
        Vector3 pos = line.GetPosition(1);
        float distance = Vector3.Distance(transform.parent.parent.position, pos);

        float x = particles.shape.scale.x;
        float y = particles.shape.scale.y;

        ParticleSystem.ShapeModule shape = particles.shape;
        shape.scale = new Vector3(x, y, distance * (1.0f/transform.localScale.x));

        float avgX = (transform.parent.parent.position.x + pos.x)/2.0f;
        float avgY = (transform.parent.parent.position.y + pos.y)/2.0f;
        float avgZ = (transform.parent.parent.position.z + pos.z)/2.0f;
        Vector3 newPosition = new Vector3(avgX, avgY, avgZ);
        //transform.position = 
        Vector3 direction = pos - transform.parent.parent.position;
        transform.parent.forward = direction;

        particles.Play();
    }
}
