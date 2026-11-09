using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetDistToPostion", story: "Get [Distance] between [Self] and [Position]", category: "Action", id: "8a8c77e107305fc659fa6e53b80e4cb2")]
public partial class GetDistToPostionAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Distance;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector3> Position;
    protected override Status OnStart()
    {
        Distance.SetValueWithoutNotify(Vector3.Distance(Self.Value.transform.position, Position));
        return Status.Running;
    }
}

