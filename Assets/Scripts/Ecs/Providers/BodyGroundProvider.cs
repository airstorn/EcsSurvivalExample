using System;
using Ecs.Components;
using Ecs.Mono;
using UnityEngine;

namespace Ecs.Providers
{
    public class BodyGroundProvider : MonoConverter<BodyGroundComponent>
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Value.Trigger.bounds.center + (Value.Direction * Value.Distance), Value.Radius);
        }
    }
}