using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetAnimFloat", story: "Sets [Animation] [string] to [value]", category: "Action", id: "9ed276776febb94f557ad37b79e0f01f")]
public partial class SetAnimFloatAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyTree> Animation;
    [SerializeReference] public BlackboardVariable<string> String;
    [SerializeReference] public BlackboardVariable<float> Value;

    protected override Status OnStart()
    {
        Animation.Value.AnimationFloat(String, Value);
        return Status.Running;
    }
}

