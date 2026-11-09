using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RotateToTarget", story: "Rotates [Enemy] towards [Target]", category: "Action", id: "905cc853eae1d53a4b52cd29701dcb00")]
public partial class RotateToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyTree> Enemy;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    protected override Status OnUpdate()
    {
        Enemy.Value.RotateToTargetByTime(Target);
        return Status.Success;
    }
}

