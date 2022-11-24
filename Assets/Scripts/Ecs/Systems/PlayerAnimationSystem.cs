using Core;
using Ecs.Components;
using Ecs.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class PlayerAnimationSystem : EcsUpdateSystem
    {
        private readonly EcsFilter<AnimatorComponent, GatheringEvent> _gatheringStarted = null;
        private readonly EcsFilter<AnimatorComponent, GatheringCanceledEvent> _gatheringCanceled = null;
        
        private readonly EcsFilter<AnimatorComponent, InputComponent, MovementComponent, PlayerTag> _filter = null;
        
        public override void Run()
        {
            ProcessGathering();
            ProcessMovement();
        }

        private void ProcessGathering()
        {
            foreach (var i in _gatheringStarted)
            {
                ref var animator = ref _gatheringStarted.Get1(i);
                animator.Animator.SetBool(animator.AttackKey, true);
            } 
            
            foreach (var i in _gatheringCanceled)
            {
                ref var animator = ref _gatheringCanceled.Get1(i);
                animator.Animator.SetBool(animator.AttackKey, false);
            }
        }
        
        private void ProcessMovement()
        {
            foreach (var i in _filter)
            {
                ref var animator = ref _filter.Get1(i);
                ref var input = ref _filter.Get2(i);
                ref var data = ref _filter.Get3(i);

                float speedNormalized = data.Controller.velocity.magnitude / data.Speed; 
                
                animator.Animator.SetFloat(animator.SpeedKey, speedNormalized);
            }
        }
    }
}