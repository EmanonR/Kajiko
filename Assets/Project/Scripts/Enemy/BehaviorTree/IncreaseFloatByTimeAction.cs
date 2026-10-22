using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "IncreaseFloatByTime", story: "Increase [Float] by deltaTime", category: "Action", id: "deebf0179103fc32ff26c551834b512f")]
public partial class IncreaseFloatByTimeAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Float;

    protected override Status OnStart()
    {
        Float.SetValueWithoutNotify(Float.Value + Time.deltaTime);
        return Status.Running;
    }
}

