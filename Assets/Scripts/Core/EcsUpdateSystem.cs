using Leopotam.Ecs;

namespace Core
{
    public abstract class EcsUpdateSystem : IEcsRunSystem
    {
        public abstract void Run();
    }
}