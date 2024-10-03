using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivatable
{
    event Action OnActivate;
    event Action OnDeactivate;
}
