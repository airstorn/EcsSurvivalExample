using Core.Interfaces;
using UnityEngine;

namespace Zenject.Pool
{
    public class EcsPoolsUtility
    {
        private IEcsPool[] _pools;
        
        public EcsPoolsUtility(IEcsPool[] pools)
        {
            _pools = pools;
        }

        public bool TryGetPool<T>(T obj, out IEcsPool pool)
        {
            foreach (var vacantPool in _pools)
            {
                if (vacantPool.IsEqual(obj))
                {
                    pool = vacantPool;
                    return true;
                }
            }

            pool = null;
            return false;
        }
    }
}