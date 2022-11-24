using Core;
using Core.Input;
using Ecs.Mono;
using Ecs.Providers;
using Ecs.Systems;
using UnityEngine;

namespace Zenject.Installers
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField]
        private InputType _type;
        
        [SerializeField]
        private JoystickInputProvider _mobileInput;
        
        public override void InstallBindings()
        {
            switch (_type)
            {
                case InputType.Keyboard:
                    BindKeyboardInput();
                    break;
                case InputType.Mobile:
                    BindMobileInput();
                    break;
            }
            CreateSystem();
        }

        private void BindKeyboardInput()
        {
            Container.Bind<KeyboardInputSystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<EcsUpdateSystem>().To<KeyboardInputSystem>().FromResolve();
        }

        private void BindMobileInput()
        {
            JoystickInputSystem system = new JoystickInputSystem(_mobileInput);

            Container.BindInterfacesAndSelfTo<JoystickInputSystem>().FromInstance(system).AsSingle().NonLazy();
        }

        private void CreateSystem()
        {
            Container.Bind<PlayerInputSystem>().FromNew().AsSingle().NonLazy();
          
            Container.Bind<EcsUpdateSystem>().To<PlayerInputSystem>().FromResolve();
        }
    }
}