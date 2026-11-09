using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetAnimFloatByTime", story: "Sets [Anim] [String] to [Float] by [Speed]", category: "Action", id: "0a38dc6e0304e8cd4d302c97556278ec")]
public partial class SetAnimFloatByTimeAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyTree> Anim;
    [SerializeReference] public BlackboardVariable<string> String;
    [SerializeReference] public BlackboardVariable<float> Float;
    [SerializeReference] public BlackboardVariable<float> Speed;

    protected override Status OnStart()
    {
        Anim.Value.LerpAnimationFloat(String, Float, Speed);
        return Status.Running;
    }
}

