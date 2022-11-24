using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class PlayerUiSystem : IEcsInitSystem
    {
        [EcsIgnoreInject]
        private GameObject _ui;
        
        public PlayerUiSystem(GameObject ui)
        {
            _ui = ui;
        }
        
        public void Init()
        {
            GameObject.Instantiate(_ui);
        }
    }
}