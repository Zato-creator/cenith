using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    //Aray de puntos de movimiento
    [SerializeField] private Vector3[] puntos;

    public Vector3[] Puntos => puntos;
    //Creamos una propiedad para guardar la posicion delpersonaje en cada momento
    public Vector3 PosicionActual { get; set; }
    private bool juegoIniciado;


    private void Start()
    {
        // juego iniciado es verdadero cuando iniciamos a jugar
        juegoIniciado = true;
        // posicion actual es igual a la posicion de este objeto
        PosicionActual = transform.position;
    }

    public Vector3 ObtenerPosicionMovimeinto (int index)
    {
        return PosicionActual + puntos [index];
    } 

    private void OnDrawGizmos()
    {

        if(juegoIniciado == false && transform.hasChanged)
        {
            //Actualizamos la posicion actual del personaje
            PosicionActual= transform.position; 
        }
        // verificar si tenemos puntos que dibujar o si este array no es nulo
        if (puntos == null || puntos.Length <= 0 )
        {
            return;
        }
        // ciclo for que recorrera todos los puntos utilizando Length
        for (int i = 0; i < puntos.Length; i++)
        {
            // Definimos que color utilizamos para los puntos
            Gizmos.color = Color.blue;
            //Dibujamos las esferas en los puntos definimos tambien el radio de las esferas
            Gizmos.DrawWireSphere(puntos[i] + PosicionActual, 0.5f );
            // definimos la cantidad de puntos que tenemos en el Aray para no excedernos
            if (i < puntos.Length - 1)
            {
                // Actualizare el color para mis lineas
                Gizmos.color = Color.gray;
                // Dibujamos una liena de un punto a otro
                Gizmos.DrawLine(puntos[i] + PosicionActual, puntos[i + 1] + PosicionActual);
            }
        }
    }


}
