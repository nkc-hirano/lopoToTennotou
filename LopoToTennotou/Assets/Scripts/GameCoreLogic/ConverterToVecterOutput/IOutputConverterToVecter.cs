using System;

namespace GameCore
{
    public interface IOutputConverterToVecter
    {
        IObservable<OutputStructConverterToVecter> ConvertResultObservable { get; }
    }
}
