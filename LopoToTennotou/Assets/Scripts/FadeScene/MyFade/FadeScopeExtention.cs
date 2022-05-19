using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeScopeExtention
{
    public static bool IsExcute(this FadeScope scope)
    {
        return FadeScope.IsExCuting;
    }
}
