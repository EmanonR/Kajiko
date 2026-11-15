using UnityEngine;

public class HeatManager : MonoBehaviour
{
    bool policeAlerted = false;
    public float timeSinceAlert = 0;

    public static HeatManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }
    }

    private void Update()
    {
        if (policeAlerted)
            timeSinceAlert += Time.deltaTime;
    }

    public void AlertPolice()
    {
        policeAlerted = true;
    }

    public void ResetAlert()
    {
        policeAlerted = false;
    }
}
