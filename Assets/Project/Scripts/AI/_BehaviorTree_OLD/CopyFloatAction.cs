using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CopyFloat", story: "Copies [Float1] to [Float2]", category: "Action", id: "626b5acefaba398f1e680cedd3792b91")]
public partial class CopyFloatAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Float1;
    [SerializeReference] public BlackboardVariable<float> Float2;

    protected override Status OnStart()
    {
        Float2.SetValueWithoutNotify(Float1.Value);
        return Status.Running;
    }
}

