using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Seek Player", story: "Agent seeks player at [speed]", category: "Action", id: "673a338becda917071d7cfdd14c91416")]
public partial class SeekPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Speed;

    private Transform player;
    private Transform self;

    private EnemyHealth _enemyHealth;

    protected override Status OnStart()
    {
        _enemyHealth = GameObject.GetComponent<EnemyHealth>();
        player = PlayerController.Instance.transform;
        self = GameObject.transform;

        if (player == null)
        {
           // Debug.LogError("Player not found!");
            return Status.Failure;
        }
        else
        {
           // Debug.Log("Player encontrado!");
            return Status.Running;
        }
    }

    protected override Status OnUpdate()
    {
        if (player == null)
        {
            return Status.Failure;

        }

        if (_enemyHealth.isKnockbacked)
        {
            return Status.Running;
        }

        Vector3 direction = (player.position - self.position).normalized;
        self.position += direction * Speed * Time.deltaTime;

        return Status.Running;

    }

    protected override void OnEnd()
    {
    }
}

