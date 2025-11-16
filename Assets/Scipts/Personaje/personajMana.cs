using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personajMana : MonoBehaviour
{
    [SerializeField] private float ManaInicial;
    [SerializeField] private float ManaMax;
    [SerializeField] private float RegeneracionPorSegundo;

    public float ManaActual { get; private set; }
    public bool SePuedeRestaurar => ManaActual < ManaMax;


    private PersonajeVida _personajeVida;
    private void Awake()
    {
        _personajeVida = GetComponent<PersonajeVida>();
    }
    void Start()
    {
        ManaActual = ManaInicial;
        ActualizarBarraMana();

        InvokeRepeating(nameof(RegenerarMana), 1,1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UsarMana(10f);
        }
    }
    public void UsarMana(float cantidad)
    {
        if (ManaActual >= cantidad)
        {
            ManaActual -= cantidad;
            ActualizarBarraMana();

        }
    }

    public void RestaurarMana(float cantidad)
    {
        if(ManaActual >= ManaMax)
        {
            return;
        }

        ManaActual += cantidad;

        if (ManaActual > ManaMax)
        {
            ManaActual = ManaMax;
        }
        UiManager.Instance.ActualizarExpPersonaje(ManaActual, ManaMax);
    }


    private void RegenerarMana()
    {
        if (_personajeVida.Salud > 0f && ManaActual < ManaMax)
        {
            ManaActual += RegeneracionPorSegundo;
            ActualizarBarraMana();
        }
    }

    public void RestablecerMana()
    {
        ManaActual = ManaInicial;
        ActualizarBarraMana();
    }
    private void ActualizarBarraMana()
    {
        UiManager.Instance.ActualizarManaPersonaje(ManaActual, ManaMax);
    }
}
