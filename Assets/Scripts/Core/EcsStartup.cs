using System;
using Core;
using Ecs.Components;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

public class EcsStartup : IInitializable, ITickable, IFixedTickable, IDisposable
{
    private EcsWorld _world;

    private EcsSystems _updateSystems;
    
    private EcsSystems _fixedUpdateSystems;

    public EcsWorld World => _world;
    
    public EcsStartup(
        EcsWorld world,
        IEcsPreInitSystem[] preInitSystem, 
        IEcsInitSystem[] initSystems, 
        EcsUpdateSystem[] updateSystems, 
        EcsFixedUpdateSystem[] fixedUpdateSystem,
        OneFramesData[] oneFramesData)
    {
        _world = world;

        _updateSystems = new EcsSystems(_world);
        _fixedUpdateSystems = new EcsSystems(_world);

        foreach (var ecsPreInitSystem in preInitSystem)
        {
            AddPreInitSystem(ecsPreInitSystem);
        }
        
        foreach (var system in initSystems)
        {
            AddInitSystem(system);
        }

        foreach (var system in updateSystems)
        {
            AddUpdateSystem(system);
        }
        
        foreach (var system in fixedUpdateSystem)
        {
            AddFixedUpdateSystem(system);
        }

        foreach (var data in oneFramesData)
        { 
            data.ApplyOneFrames(this);
        }
        
        AddInjection(this);
    }

    private void AddPreInitSystem(IEcsPreInitSystem system)
    {
        _updateSystems.Add(system);
    }
    
    private void AddInitSystem(IEcsInitSystem system)
    {
        _updateSystems.Add(system);
    }

    private void AddUpdateSystem(EcsUpdateSystem system)
    {
        _updateSystems.Add(system);
    }
    
    private void AddFixedUpdateSystem(EcsFixedUpdateSystem system)
    {
        _updateSystems.Add(system);
    }

    public void AddOneFrame<T>() where T : struct
    {
        _updateSystems.OneFrame<T>();
    }
    
    public void AddInjection<T>(T obj)
    {
        _updateSystems.Inject(obj);
        _fixedUpdateSystems.Inject(obj);
    }

    public void Initialize()
    {
        _updateSystems.Init();
        _fixedUpdateSystems.Init();
    }

    public void Tick()
    {
        _updateSystems.Run();
    }
    
    public void FixedTick()
    {
        _fixedUpdateSystems.Run();
    }

    public void Dispose()
    {
        _updateSystems.Destroy();
        _fixedUpdateSystems.Destroy();
        _world.Destroy();
    }
}
