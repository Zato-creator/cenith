using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TiposItems
{
    Armas,
    Pociones,
    Pergaminos,
    Tesoros
}
public class InventarioItem : ScriptableObject
{
    [Header("Parametros")]
    public string ID;
    public string nombre;
    public Sprite Icono;
    [TextArea]public string Descripcion;

    [Header("Informacion")] 
    public TiposItems Tipo;
    public bool EsConsumible;
    public bool EsAcumulable;
    public int AcumulacionMax;

    [HideInInspector] public int Cantidad;

    public InventarioItem CopiarItem()
    {
        InventarioItem nuevaInstancia = Instantiate(original: this);
        return nuevaInstancia;
    }

    public virtual bool UsarItem()
    {
        return true;
    }
    public virtual bool EquiparItem()
    {
        return true;
    }
    public virtual bool RemoverItem()
    {
        return true;
    }
}
