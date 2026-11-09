using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveWithoutRotation", story: "Moves [Agent] to [Target] without rotation", category: "Action", id: "85e74b4c265742505a24e37ef92e7296")]
public partial class MoveWithoutRotationAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyTree> Agent;
    [SerializeReference] public BlackboardVariable<Vector3> Target;

    protected override Status OnStart()
    {
        Agent.Value.MoveWithoutRotation(Target);
        return Status.Running;
    }
}

