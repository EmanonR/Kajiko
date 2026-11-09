using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetHP", story: "Sets [variable] with [EnemyScript]", category: "Action", id: "0c43245c96b1f04061096e9c5a1dbe42")]
public partial class GetHpAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Variable;
    [SerializeReference] public BlackboardVariable<EnemyTree> EnemyScript;

    protected override Status OnStart()
    {
        Variable.SetValueWithoutNotify(EnemyScript.Value.GetHP());

        return Status.Running;
    }
}

