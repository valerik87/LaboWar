using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class SingletonInitializer : MonoBehaviour {

    private static SingletonAllocator<AttackParabola> AttackParabolaAllocator;

    void Awake()
    {
        AttackParabolaAllocator = SingletonAllocator<AttackParabola>.GetInstance();
        test();
    }

    private static void test()
    {
        AttackParabola parabola = AttackParabolaAllocator.GetFromPool().GetClassReferencer();
        parabola.Setup();
        parabola.Draw();
    }

    void OnDestroy()
    {
        SingletonAllocator<AttackParabola>.GetInstance().Dispose();
    }
}
