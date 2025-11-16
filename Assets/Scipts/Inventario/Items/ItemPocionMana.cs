using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Pocion Mana")]
public class ItemPocionMana : InventarioItem
{
    [Header("Pocion info")]
    public float MPRestauracion;


    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.personajMana.SePuedeRestaurar)
        {
            Inventario.Instance.Personaje.personajMana.RestaurarMana(MPRestauracion);
            return true;
        }

        return false;   
    }
}
