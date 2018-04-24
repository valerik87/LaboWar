using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class SingletonInitializer : MonoBehaviour {

    private static SingletonAllocator<AttackParabola> AttackParabolaAllocator;

    void Awake()
    {
        AttackParabolaAllocator = SingletonAllocator<AttackParabola>.GetInstance();
    }

    private static void test()
    {
    }

    void OnDestroy()
    {
        SingletonAllocator<AttackParabola>.GetInstance().Dispose();
    }
}
