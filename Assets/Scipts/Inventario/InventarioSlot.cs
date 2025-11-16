using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;


public enum TiposDeInteraccion
{
    Click,
    Usar,
    Equipas,
    Remover
}
public class InventarioSlot : MonoBehaviour
{
    public static Action<TiposDeInteraccion, int> EventoSlotinteraccion;
    [SerializeField] private Image ItemIcono;
    [SerializeField] private GameObject fondoCantidad;
    [SerializeField] private TMPro.TextMeshProUGUI CantidadTMP;
    public int Index { get; set; }

    private Button _Button;
    private void Awake()
    {
        _Button = GetComponent<Button>();
    }
    public void ActualizarSlot(InventarioItem item,  int cantidad)
    {
        ItemIcono.sprite = item.Icono;
        CantidadTMP.text = cantidad.ToString();
    }

    public void ActivarSlotUI(bool estado)
    {
        ItemIcono.gameObject.SetActive(estado);
        fondoCantidad.SetActive (estado);
    }

    public void SeleccionarSlot()
    {
        _Button.Select();
    }

    public void ClickSlot()
    {
        EventoSlotinteraccion?.Invoke(TiposDeInteraccion.Click, Index); 

        // Mover Item
        if (InventarioUI.Instance.IndexSlotIniciaPorMover != -1 )
        {
            if (InventarioUI.Instance.IndexSlotIniciaPorMover != Index)
            {
                // Mover
                Inventario.Instance.MoverItem(InventarioUI.Instance.IndexSlotIniciaPorMover, Index);
            }
        }
    }

    public void slotusarItem()
    {
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventoSlotinteraccion?.Invoke(TiposDeInteraccion.Usar, Index);
        }
    }
 
}
