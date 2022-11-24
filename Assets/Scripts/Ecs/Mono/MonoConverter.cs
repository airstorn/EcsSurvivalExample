using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Mono
{
    public abstract class MonoConverter : MonoBehaviour
    {
        public abstract void Convert(EcsEntity entity);
    }
    
    public abstract class MonoConverter<T> : MonoConverter where T : struct
    {
        [SerializeField]
        private T _value;

        protected T Value => _value;
        
        public override void Convert(EcsEntity entity)
        {
            entity.Replace(_value);
        }
    }
}