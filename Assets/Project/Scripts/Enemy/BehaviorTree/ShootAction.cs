using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Shoot", story: "[EnemyTree] shoots forward", category: "Action", id: "a28189edfbe81c32d5e5358342041d89")]
public partial class ShootAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyTree> EnemyTree;

    protected override Status OnStart()
    {
        EnemyTree.Value.Shoot();
        return Status.Running;
    }
}

