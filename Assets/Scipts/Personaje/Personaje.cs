using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    [SerializeField] private PersonajeStats stats;

    public PersonajeExperiencia PersonajeExperiencia  {  get; private set; }
    public PersonajeVida PersonajeVida {get; private set;}
    public PersonajeAnimaciones PersonajeAnimaciones { get; private set; }
    public personajMana personajMana { get; private set; }

    private void Awake()
    {
        PersonajeVida = GetComponent<PersonajeVida>();
        PersonajeAnimaciones = GetComponent<PersonajeAnimaciones>();
        personajMana = GetComponent<personajMana>();
        PersonajeExperiencia = GetComponent<PersonajeExperiencia>();
    }
    public void Restaurtarpersonaje()
    {
        PersonajeVida.restaurarPersonaje(); 
        PersonajeAnimaciones.RevivirPersonaje();
        personajMana.RestablecerMana();
    }

    private void AtributoRespuesta(TipoAtributo tipo)
    {
        if(stats.PuntosDisponibles<=0)
        {
            return;
        }
        switch (tipo)
        {
            case TipoAtributo.Fuerza:
                stats.Fuerza++;
                stats.AñadirPuntosAtributoFuerza();
                break;
            case TipoAtributo.Inteligencia:
                stats.Inteligencia++;
                stats.AñadirPuntosAtributoInteligencia();
                break;
            case TipoAtributo.Destreza:
                stats.Destreza++;
                stats.AñadirPuntosAtributoDestreza();
                break;
        }

        stats.PuntosDisponibles -= 1;
    }
    private void OnEnable()
    {
        AtributoButton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {
        AtributoButton.EventoAgregarAtributo -= AtributoRespuesta;
    }

}
