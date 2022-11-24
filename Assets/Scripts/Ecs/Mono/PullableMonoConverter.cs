using Core.Interfaces;

namespace Ecs.Mono
{
    public interface IEcsPoolTagProvider
    {
        public IEcsPoolTag Tag { get; }
    }
    public class PullableMonoConverter<T> : MonoConverter<T> ,
        IEcsPoolTagProvider
        where T : struct, IEcsPoolTag
    {
        public IEcsPoolTag Tag => Value;
    }
}