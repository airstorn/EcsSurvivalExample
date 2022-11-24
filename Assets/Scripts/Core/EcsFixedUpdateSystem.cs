using Leopotam.Ecs;

namespace Core
{
    public abstract class EcsFixedUpdateSystem : IEcsRunSystem
    {
        public abstract void Run();
    }
}