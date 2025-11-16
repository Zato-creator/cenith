using System.Collections; 
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PersonajeAnimaciones : MonoBehaviour
{
    [SerializeField] private string layerIdel;
    [SerializeField] private string layerCaminar;
    private Animator _animator;
    private PersonajeMovimiento _personajeMovimiento; 


    private readonly int direccionX = Animator.StringToHash("X");
    private readonly int direccionY = Animator.StringToHash("Y");
    private readonly int derrotado = Animator.StringToHash("Derrotado");

    void Start()
    {
        _animator = GetComponent<Animator>();
        _personajeMovimiento = GetComponent <PersonajeMovimiento> ();
    }
    private void Update()
    {
        ActualizarLayer();
        if(_personajeMovimiento.EnMovimiento == false)
        {
            return;
        }
        _animator.SetFloat(direccionX, _personajeMovimiento.DireccionMovimeinto.x);
        _animator.SetFloat(direccionY, _personajeMovimiento.DireccionMovimeinto.y);
    }
    public void RevivirPersonaje()
    {
        ActivarLayer(layerIdel);
        _animator.SetBool(derrotado, false); 
    }
    private void ActivarLayer(string nombreLayer)
    {
        for(int i=0; i< _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i, 0); 

        }
        _animator.SetLayerWeight(_animator.GetLayerIndex(nombreLayer), 1);
    }

    private void ActualizarLayer()
    {
        if (_personajeMovimiento.EnMovimiento)
        {
            ActivarLayer(layerCaminar);
        }
        else
        {
            ActivarLayer(layerIdel);
        }
    }

    private void personajeDerrotadoRespuesta()
    {
        if (_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdel)) ==1)
        {
            _animator.SetBool(derrotado, true);
        }
    }

    private void OnEnable()
    {
        PersonajeVida.EventoPersonajeDerrotado += personajeDerrotadoRespuesta;
    }

    private void OnDisable()
    {
        PersonajeVida.EventoPersonajeDerrotado -= personajeDerrotadoRespuesta;
    }
}
