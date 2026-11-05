using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public static List<Interactable> interactables = new();

    public float interactRange = 10f;
    public Transform player;
    public bool interactedWith;

    public UnityAction OnInteract;

    private void Awake()
    {
        interactables.Add(this);
    }

    private void OnDestroy()
    {
        interactables.Remove(this);
    }

    public virtual void Interact(Player player)
    {
        interactedWith = true;
        this.player = player.transform;
        OnInteract?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
