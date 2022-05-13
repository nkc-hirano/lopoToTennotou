using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TitleScene
{
    public class FadeInitializer : MonoBehaviour
    {
        void Start()
        {
            TryGetComponent(out FadeFaçade façade);
            façade.Initialize(0.5f);
        }
    }

}
