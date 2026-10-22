using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRespawn : MonoBehaviour
{
    public InputAction respawn;
    Vector3 spawnPos;

    private void Awake()
    {
        spawnPos = transform.position;
        respawn.Enable();
    }

    public void LateUpdate()
    {
        if (respawn.triggered)
        {
            CharacterController cc = GetComponent<CharacterController>();

            cc.enabled = false;
            Debug.Log("RESPAWNING");
            transform.position = spawnPos;
            cc.enabled = true;
        }
    }

    void Respawn()
    {
        Debug.Log("RESPAWNING");
        transform.position = spawnPos;
    }
}