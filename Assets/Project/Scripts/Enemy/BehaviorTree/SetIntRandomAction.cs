using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetIntRandom", story: "Set [Int] to Random [Max]", category: "Action", id: "8f3d76b89e89dc97721fe9276a0bec2b")]
public partial class SetIntRandomAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Int;
    [SerializeReference] public BlackboardVariable<int> Max;

    protected override Status OnStart()
    {
        Int.SetValueWithoutNotify(UnityEngine.Random.Range(0, Max + 1));
        return Status.Running;
    }
}

