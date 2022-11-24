using Core;
using Ecs.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class ToolEquipmentSystem : EcsUpdateSystem,
        IEcsInitSystem
    {
        private readonly EcsFilter<ToolContainerComponent, EquipToolEvent, ToolPresenterComponent> _filter = null; 
        private readonly EcsFilter<ToolContainerComponent, ToolPresenterComponent> _test = null; 

        public override void Run()
        {
            if (_filter.IsEmpty())
            {
                return;
            }
            
            foreach (var i in _filter)
            {
                ref var data = ref _filter.Get2(i);
                ref var container = ref _filter.Get1(i);
                ref var presenter = ref _filter.Get3(i);

                container.Current = data.Tool;

                var view = GameObject.Instantiate(container.Current.CharacterView, presenter.Parent);
                
                view.transform.localPosition = Vector3.zero;
                view.transform.localRotation = Quaternion.identity;
                view.transform.localScale = Vector3.one;

                if (presenter.CurrentInstance != null)
                {
                    GameObject.Destroy(presenter.CurrentInstance);
                    presenter.CurrentInstance = null;
                }

                presenter.CurrentInstance = view;
            }
        }

        public void Init()
        {
            foreach (var i in _test)
            {
                ref var entity = ref _test.GetEntity(i);
                ref var defaultTool = ref _test.Get1(i);

                ref var equip = ref entity.Get<EquipToolEvent>();

                equip.Tool = defaultTool.Current;
            }
        }
    }
}