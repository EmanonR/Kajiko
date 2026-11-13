using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DrawLineBetweenTargets : MonoBehaviour
{
    public List<Transform> transforms = null;
    bool loop;

    LineRenderer lineRenderer;

    private void Awake()
    {
        if (lineRenderer == null)
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (transforms.Count == 0) return;

        List<Vector3> positions = new();

        positions.Clear();

        foreach (var item in transforms)
        {
            positions.Add(item.position);
        }

        lineRenderer.SetPositions(positions.ToArray());
        lineRenderer.loop = loop;
    }
}
