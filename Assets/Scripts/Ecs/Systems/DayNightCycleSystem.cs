using Core;
using Ecs.Components;
using Ecs.Mono;
using Ecs.Providers;
using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Ecs.Systems
{
    public class DayNightCycleSystem : EcsUpdateSystem,
        IEcsInitSystem
    {
        private readonly EcsFilter<DayNightCycleComponent>.Exclude<TimerComponent> _switchFilter = null;

        private readonly EcsFilter<DayNightAnimationComponent, TimerComponent> _animateFilter = null;

        private readonly EcsFilter<DayNightAnimationComponent>.Exclude<TimerComponent> _completedTransitionsFilter = null;
        
        private readonly DayNightCycleProvider _dayNightReference = null;

        private readonly EcsWorld _world = null;

        private readonly int _colorId = Shader.PropertyToID("_FogColor");
        
        public DayNightCycleSystem(DayNightCycleProvider dayNightReference)
        {
            _dayNightReference = dayNightReference;
        }

        public override void Run()
        {
            SwitchDaytime();
            AnimateTimeTransition();
        }

        public void Init()
        {
            var instance = GameObject.Instantiate(_dayNightReference);
            instance.Convert(_world);
        }

        private void AnimateTimeTransition()
        {
            if (_animateFilter.IsEmpty() && _completedTransitionsFilter.IsEmpty())
            {
                return;
            }
            
            foreach (var i in _animateFilter)
            {
                ref var timer = ref _animateFilter.Get2(i);
                ref var data = ref _animateFilter.Get1(i);

                float normalized = timer.Current / timer.Target;
                
                data.Light.intensity = Mathf.Lerp(data.StartIntensity, data.TargetIntensity, normalized);
                data.Light.color = Color.Lerp(data.StartLightColor, data.TargetLightColor, normalized);
                    
                data.Fog.SetColor(_colorId, Color.Lerp(data.StartFogColor, data.TargetFogColor, normalized));
            }

            foreach (var i in _completedTransitionsFilter)
            {
                _completedTransitionsFilter.GetEntity(i).Destroy();
            }
        }

        private void SwitchDaytime()
        {
            foreach (var i in _switchFilter)
            {
                ref var cycle = ref _switchFilter.Get1(i);
                ref var entity = ref _switchFilter.GetEntity(i);

                ref var dayTimeTimer = ref entity.Get<TimerComponent>();
                
                dayTimeTimer.Current = 0;
                
                EcsEntity transition =  _world.NewEntity();

                ref var animation = ref transition.Get<DayNightAnimationComponent>();
                animation.Fog = cycle.FogMaterial;
                animation.Light = cycle.DayLight;
                animation.StartFogColor = cycle.FogMaterial.GetColor(_colorId);
                animation.StartIntensity = cycle.DayLight.intensity;
                animation.StartLightColor = cycle.DayLight.color;

                ref var transitionTimer = ref transition.Get<TimerComponent>();
                transitionTimer.Target = cycle.TransitionTime;

                switch (cycle.CurrentTime)
                {
                    case DayTime.Day:
                        cycle.CurrentTime = DayTime.Night;
                        animation.TargetFogColor = cycle.NightFog;
                        animation.TargetIntensity = cycle.NightLightIntensity;
                        dayTimeTimer.Target = cycle.NightTime;
                        animation.TargetLightColor = cycle.NightLightColor;
                        break;
                    case DayTime.Night:
                        cycle.CurrentTime = DayTime.Day;
                        animation.TargetFogColor = cycle.DayFog;
                        animation.TargetIntensity = cycle.DayLightIntensity;
                        dayTimeTimer.Target = cycle.DayTime;
                        animation.TargetLightColor = cycle.DayLightColor;
                        break;
                }
            }
        }
    }
}