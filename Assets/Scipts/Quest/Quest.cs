using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public static Action<Quest> EventoQuestCompletado;
    [Header("Info")]
    public string nombre; 
    public string ID;
    public int CantidadObjetivo;

    [Header("Descripcion")]
    [TextArea] public string Descripcion;

    [Header("Recompensa")]
    public int RecompensaOro;
    public float RecompensaExp;
    public QuestRecompensaItem RecompensaItem;

    [HideInInspector] public int CantidadActual;
    [HideInInspector] public bool QuestCompletado;

    public void AñadirProgreso(int cantidad)
    {
        CantidadActual += cantidad;
        VerificarQuestCompletado();
    }
    private void VerificarQuestCompletado()
    {
        if (CantidadActual >= CantidadObjetivo)
        {
            CantidadActual = CantidadObjetivo;
            Questcompletado();
        }
    }

    private void Questcompletado()
    {
        if (QuestCompletado)
        {
            return;
        }
        QuestCompletado = true;
        EventoQuestCompletado?.Invoke(this);
    }

    private void OnEnable()
    {
        QuestCompletado = false;
        CantidadActual = 0;
    }
}

[Serializable]

public class QuestRecompensaItem
{
    public InventarioItem Item;
    public int Cantidad;

}
