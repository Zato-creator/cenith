using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


[CreateAssetMenu(menuName = "IA/Desiciones/Detectar personaje       ")]
public class DesicionDetectarPersonaje : IADecicion
{
    public override bool Decidir(IAController controller)
    {
        return DetectarPersonaje(controller);
    }
    private bool DetectarPersonaje(IAController controller)
    {
        Collider2D personajeDetectado = Physics2D.OverlapCircle(controller.transform.position, controller.RangoDeteccion, controller.PersonajeLayerMask);
        if (personajeDetectado != null)
        {
            controller.PersonajeReferencia = personajeDetectado.transform;
            return true;    
        }

        controller.PersonajeReferencia = null;
        return false;
    }
}
