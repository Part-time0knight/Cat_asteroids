using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;
using System;
using UnityEngine;
using Zenject;

public abstract class AbstractMutator : IInitializable, IDisposable
{


    protected readonly IMutatorsObservable _mutatorObservable;
    protected readonly IMutatorData _mutatorData;

    protected virtual Mutator Id { get; }

    public AbstractMutator(IMutatorsObservable mutatorObservable,
        IMutatorData mutatorData)
    {
        _mutatorObservable = mutatorObservable;
        _mutatorData = mutatorData;
    }

    public virtual void Initialize()
    {
        _mutatorObservable.OnMutatorUpdate += InvokeUpdate;
    }

    public virtual void Dispose()
    {
        _mutatorObservable.OnMutatorUpdate -= InvokeUpdate;
    }

    protected virtual void InvokeUpdate(int id)
    {
        if (id != (int)Id) return;

        bool active = _mutatorData.IsActive(id);
        if (active)
            Set();
        else
            Remove();
    }

    protected abstract void Set();

    protected abstract void Remove();
}
