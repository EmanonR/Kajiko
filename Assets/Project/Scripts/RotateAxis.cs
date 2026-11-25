using UnityEngine;

public class RotateAxis : MonoBehaviour
{
    public float xSpeed, ySpeed, zSpeed;

    private void Update()
    {
        transform.Rotate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime);
    }
}
