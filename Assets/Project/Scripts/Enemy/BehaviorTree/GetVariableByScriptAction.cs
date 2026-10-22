using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Reflection;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetVariableByScript", story: "Sets [Variable] of [Script] by [String]", category: "Action", id: "7d56d25c2373e4540fdcc2c68f8cfb89")]
public partial class GetVariableByScriptAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Variable;
    [SerializeReference] public BlackboardVariable<EnemyTree> Script;
    [SerializeReference] public BlackboardVariable<string> String;
    protected override Status OnStart()
    {
        FieldInfo field = Script.Value.GetType().GetField(
            String.Value,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        Variable.SetValueWithoutNotify((float)field.GetValue(Script.Value));
        return Status.Running;
    }
}

