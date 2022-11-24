using Zenject;

namespace Utils
{
    public static class ZenjectBindExtension
    {
        public static void BindToBaseClass<TBind, To>(this DiContainer container) where TBind : To
        {
            container.Bind<TBind>().FromNew().AsSingle().NonLazy();
            container.Bind<To>().To<TBind>().FromResolve();
        }
        
        public static void BindToBaseClass<TBind, To>(this DiContainer container, TBind instance) where TBind : To
        {
            container.Bind<TBind>().FromInstance(instance).AsSingle().NonLazy();
            container.Bind<To>().To<TBind>().FromResolve();
        }
    }
}