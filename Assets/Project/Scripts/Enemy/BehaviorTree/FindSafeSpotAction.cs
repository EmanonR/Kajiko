using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindSafeSpot", story: "Gets [SafeSpot] by [Script] with [Distance]", category: "Action", id: "9fe1b6863874edd3d153e99d07ce72d1")]
public partial class FindSafeSpotAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> SafeSpot;
    [SerializeReference] public BlackboardVariable<EnemyTree> Script;
    [SerializeReference] public BlackboardVariable<float> Distance;
    protected override Status OnStart()
    {
        SafeSpot.SetValueWithoutNotify(Script.Value.GetClosestSafePoint(Distance));
        return Status.Running;
    }
}

