using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetPlayer", story: "Get [player] with [EnemyTree]", category: "Action", id: "44b1430e1974e09d4484ff4896b786d9")]
public partial class GetPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Player;
    [SerializeReference] public BlackboardVariable<EnemyTree> EnemyTree;

    protected override Status OnStart()
    {
        Transform player = EnemyTree.Value.GetPlayerFromGameManager();

        Player.SetValueWithoutNotify(player);

        return Status.Running;
    }
}

