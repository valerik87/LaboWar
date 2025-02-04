﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    interface ClassReferencer<T>
    {
        T GetClassReferencer();

        void EnableGameObject();
        void DisableGameObject();
    }
}
