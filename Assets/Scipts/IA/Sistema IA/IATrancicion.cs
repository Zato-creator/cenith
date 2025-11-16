using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IATrancicion 
{
    public IADecicion Decicion;
    public IAEstado EstadoVerdadero;
    public IAEstado EstadoFalso;
}
