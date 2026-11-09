using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetDistance", story: "Returns distance between [Self] and [Target] to [Variable]", category: "Action", id: "ae590e57d22aef03e405bc926a1288c8")]
public partial class GetDistanceAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyTree> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Variable;

    protected override Status OnStart()
    {
        float dist = Self.Value.GetDistance(Target);

        Variable.SetValueWithoutNotify(dist);

        return Status.Running;
    }
}

