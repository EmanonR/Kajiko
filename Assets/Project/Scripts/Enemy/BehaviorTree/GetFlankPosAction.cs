using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetFlankPos", story: "Gets [Flank] of [Player] with [Script]", category: "Action", id: "f23e0fe9857f06a92ac731446fc4b50a")]
public partial class GetFlankPosAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Flank;
    [SerializeReference] public BlackboardVariable<Transform> Player;
    [SerializeReference] public BlackboardVariable<EnemyTree> Script;

    protected override Status OnStart()
    {
        Flank.SetValueWithoutNotify(Script.Value.GetFlankOfTransform(Player, 10));
        return Status.Running;
    }
}

