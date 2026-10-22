using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveAgentToTarget", story: "Moves [Agent] To [Target]", category: "Action", id: "50277b0be2b53acda55406bd8288b2c1")]
public partial class MoveAgentToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyTree> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    protected override Status OnStart()
    {
        Agent.Value.SetMoveTarget(Target.Value.position);
        return Status.Running;
    }
}

