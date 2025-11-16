using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum DireccionMovimeinto
{
    Horizontal,
    Vertical
}
public class WayPointMovimiento : MonoBehaviour
{

    
    [SerializeField] protected float velocidad;

    public Vector3 PuntoPorMoverse => _waipoint.ObtenerPosicionMovimeinto(puntoActualIndex);

    protected Waypoint _waipoint;
    protected Animator _animator;
    protected int puntoActualIndex;
    protected Vector3 ultimaPosicion;
    void Start()
    {
        puntoActualIndex = 0;
        _animator = GetComponent<Animator>();
        _waipoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        Moverpersonaje();
        RotarPersonaje();
        RotarVertical();
        if (ComprobarPuntoActualAlcanzado())
        {
            ActualizarIndexMovimiento();
        }
    }



    private void Moverpersonaje()
    {
        transform.position = Vector3.MoveTowards(transform.position, PuntoPorMoverse, velocidad * Time.deltaTime);
    }

    private bool ComprobarPuntoActualAlcanzado()
    {
        float distanciaHaciaPuntoActual = (transform.position - PuntoPorMoverse).magnitude;
        if(distanciaHaciaPuntoActual < 0.1f)
        {
            ultimaPosicion = transform.position;
            return true; 
        }
        return false;
    }

    private void ActualizarIndexMovimiento()
    {
        if (puntoActualIndex == _waipoint.Puntos.Length -1 )
        {
            puntoActualIndex = 0;
        }
        else
        {
            if (puntoActualIndex < _waipoint.Puntos.Length - 1)
            {
                puntoActualIndex++;
            }
        }
    }

    protected virtual void RotarPersonaje()
    {
        
    }

    protected virtual void RotarVertical()
    {

    }
}
