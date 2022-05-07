using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private Transform[] transforms;

    public Transform[] Transforms { get => transforms; set => transforms = value; }

    // Update is called once per frame
    void Update()
    {
        //Follow the transform.
        LineRenderer renderer = GetComponent<LineRenderer>();
        Vector3[] positions = new Vector3[transforms.Length];
        for (int i = 0; i < transforms.Length; i++) positions[i] = transforms[i].position;
        renderer.SetPositions(positions);
    }
}
