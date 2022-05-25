using System;
using UnityEngine;
using UniRx;

namespace GameCore
{
    public class ConverterToVecterLogic : IInputConverterToVecter, IOutputConverterToVecter
    {
        Subject<OutputStructConverterToVecter> convertResultSubject = new Subject<OutputStructConverterToVecter>();

        IObservable<OutputStructConverterToVecter> IOutputConverterToVecter.ConvertResultObservable => convertResultSubject;

        public void ConvertVec(InputStructConverterToVecter inputStructConverterToVecter)
        {
            convertResultSubject.OnNext(new OutputStructConverterToVecter(CTVC(inputStructConverterToVecter)));
        }
        private Vector3 CTVC(InputStructConverterToVecter inputStructConverterToVecter)//ConverterToVecCreate() 
        {
            float offset = (inputStructConverterToVecter.mapScale - 1) / 2;
            return new Vector3(inputStructConverterToVecter.x - offset, 0, offset - inputStructConverterToVecter.y);
        }
    }
}