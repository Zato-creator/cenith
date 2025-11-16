using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "IA/Estado")]
public class IAEstado : ScriptableObject
{
    public IAAccion[] Acciones;
    public IATrancicion[] Tranciciones;

    public void EjecutarEstado(IAController controller)
    {
        EjecutarAcciones(controller);
        RealizarTranciciones(controller);
    }

    private void EjecutarAcciones(IAController controller)
    {
        if (Acciones == null || Acciones.Length <=0 )
        {
            return;
        }
        for (int i = 0; i < Acciones.Length; i++)
        {
            Acciones[i].Ejecutar(controller);
        }
    }

    void RealizarTranciciones(IAController controller)
    {
        if (Tranciciones == null || Tranciciones.Length <=0)
        {
            return ;
        }

        for (int i = 0; i < Tranciciones.Length; i++)
        {
            bool decisionValor = Tranciciones[i].Decicion.Decidir(controller);
            if (decisionValor)
            {
                controller.CambiarEstado(Tranciciones[i].EstadoVerdadero);
            }
            else
            {
                controller.CambiarEstado(Tranciciones[i].EstadoFalso);
            }
        }
    }
}
