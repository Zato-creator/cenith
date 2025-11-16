using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovimiento : WayPointMovimiento
{
    [SerializeField] private DireccionMovimeinto direccion;

    private readonly int caminaAbajo = Animator.StringToHash("CaminarAbajo");
    protected override void RotarPersonaje()
    {
        if (direccion != DireccionMovimeinto.Horizontal)
        {
            return;
        }

        if (PuntoPorMoverse.x > ultimaPosicion.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }


    protected override void RotarVertical()
    {
        if (direccion != DireccionMovimeinto.Vertical)
        {
            return ;
        }

        if (PuntoPorMoverse.y > ultimaPosicion.y)
        {
            _animator.SetBool(caminaAbajo, false);
        }
        else
        {
            _animator.SetBool(caminaAbajo, true);
        }
    }
  
}
