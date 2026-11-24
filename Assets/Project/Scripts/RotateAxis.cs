using UnityEngine;

public class RotateAxis : MonoBehaviour
{
    public float xSpeed, ySpeed, zSpeed;

    private void Update()
    {
        transform.Rotate(xSpeed, ySpeed, zSpeed);
    }
}
