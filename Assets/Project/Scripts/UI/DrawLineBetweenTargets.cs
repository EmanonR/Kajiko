using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]

[RequireComponent(typeof(LineRenderer))]
public class DrawLineBetweenTargets : MonoBehaviour
{
    public List<Transform> transforms = null;
    public bool loop;

    LineRenderer lineRenderer;

    private void Update()
    {
        if (lineRenderer == null) 
            lineRenderer = GetComponent<LineRenderer>();

        if (transforms.Count == 0) return;

        List<Vector3> positions = new();

        foreach (var item in transforms)
        {
            positions.Add(item.position);
        }

        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
        lineRenderer.loop = loop;
    }
}
