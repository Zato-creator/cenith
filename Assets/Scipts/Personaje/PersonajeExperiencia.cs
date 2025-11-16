using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats Stats;

    [Header("Config")]
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;

    private float expActual;
    private float expActualTemp;
    private float expRequeridaSiguienteNivel;

    void Start()
    {
        Stats.Nivel = 1;
        expRequeridaSiguienteNivel = expBase;
        Stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
        ActualizarBarraExperiencia();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AñadirExperiencia(2f);
        }
    }
    public void AñadirExperiencia(float expObtenida)
    {
        if (expObtenida > 0f) // Experiencia obtenida por mostruo es de 10 
        {
            float expRestanteNuevoNivel = expRequeridaSiguienteNivel - expActualTemp;
            if (expObtenida >= expRestanteNuevoNivel)
            {
                expObtenida -= expRestanteNuevoNivel;
                expActual += expObtenida;
                ActualizarNivel();
                AñadirExperiencia(expObtenida);
            }
            else
            {
                expActual += expObtenida;
                expActualTemp += expObtenida;
                if (expActualTemp == expRequeridaSiguienteNivel)
                {
                    ActualizarNivel();
                }
            }
        }

        Stats.ExpActual = expActual;

        ActualizarBarraExperiencia();
    }

    private void ActualizarNivel ()
    {

        if(Stats.Nivel < nivelMax)
        {
            Stats.Nivel ++;
            expActualTemp = 0f;
            expRequeridaSiguienteNivel *= valorIncremental;
            Stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
            Stats.PuntosDisponibles += 3;
        }
    } 
    private void ActualizarBarraExperiencia()
    {
        UiManager.Instance.ActualizarExpPersonaje(expActualTemp, expRequeridaSiguienteNivel); 
    }
}
