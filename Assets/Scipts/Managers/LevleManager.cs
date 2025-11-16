using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevleManager : MonoBehaviour
{
    [SerializeField] private Personaje personaje;
    [SerializeField] private Transform puntoDeReapararicion;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (personaje.PersonajeVida.Derrotado)
            {
                personaje.transform.localPosition = puntoDeReapararicion.position;
                personaje.Restaurtarpersonaje();

            }
        }
    }
    
}
