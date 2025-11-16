using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Seguir Personaje")]
public class AccionSeguirPersonaje : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        SeguirPersonaje(controller);
    }

    private void SeguirPersonaje(IAController Controller)
    {
        if (Controller.PersonajeReferencia == null)
        {
            return;
        }

        Vector3 dirHaciaPersonaje = Controller.PersonajeReferencia.position - Controller.transform.position;
        Vector3 direccion = dirHaciaPersonaje.normalized;   
        float distancia = dirHaciaPersonaje.magnitude;

        if (distancia >= 1.30f)
        {
            Controller.transform.Translate(direccion * Controller.VelocidadMovimeinto * Time.deltaTime);
        }
    }
}
