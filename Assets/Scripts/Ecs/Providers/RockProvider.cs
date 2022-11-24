using Ecs.Components;
using Ecs.Mono;
using Utils;
using Zenject;

namespace Ecs.Providers
{
    public class RockProvider : MonoConverter<RockComponent>
    {
        [Inject]
        private void Construct(EcsStartup ecs)
        {
            this.Convert(ecs.World);
        }
    }
}