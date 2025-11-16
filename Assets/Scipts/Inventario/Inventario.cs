using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    [Header("Items")]
    [SerializeField] private InventarioItem[] itemsInventario;
    [SerializeField] private Personaje personaje;
    [SerializeField] private int numeroDeSlots;


    public Personaje Personaje => personaje;
    public int NumeroDeSlots => numeroDeSlots;
    public InventarioItem[] ItemsInventario => itemsInventario;

    private void Start()
    {
        itemsInventario = new InventarioItem[numeroDeSlots];
    }

    public void AñadirItem (InventarioItem itemPorAñadir, int cantidad)
    {
        if(itemPorAñadir == null)
        {
            return;
        }

        List<int> indexes = VerificarExistencia(itemPorAñadir.ID);
        if (itemPorAñadir.EsAcumulable)
        {
            if (indexes.Count > 0)
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    if (itemsInventario[indexes[i]].Cantidad < itemPorAñadir.AcumulacionMax)
                    {
                        itemsInventario[indexes[i]].Cantidad += cantidad;
                        if (itemsInventario[indexes[i]].Cantidad > itemPorAñadir.AcumulacionMax)
                        {
                            int diferencia = itemsInventario[indexes[i]].Cantidad - itemPorAñadir.AcumulacionMax;
                            itemsInventario[indexes[i]].Cantidad = itemPorAñadir.AcumulacionMax;
                            AñadirItem(itemPorAñadir, diferencia);
                        }

                        InventarioUI.Instance.DibujarItemEnInventario(itemPorAñadir, itemsInventario[indexes[i]].Cantidad, indexes[i]);
                        return;
                    }
                }
            }
        }

        if (cantidad <= 0)
        {
            return;
        }

        if (cantidad > itemPorAñadir.AcumulacionMax)
        {
            AñadirItemEnSlotDisponible(itemPorAñadir, itemPorAñadir.AcumulacionMax);
            cantidad -= itemPorAñadir.AcumulacionMax;
            AñadirItem(itemPorAñadir, cantidad);
        }
        else
        {
            AñadirItemEnSlotDisponible(itemPorAñadir, cantidad);
        }

    }
    private List<int> VerificarExistencia(string itemID)
    {
        List<int> indexesDelItem = new List<int>();
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] != null)
            {
                if (itemsInventario[i].ID == itemID)
                {
                    indexesDelItem.Add(i);
                }
            }
            
        }
        return indexesDelItem;
    }

    private void AñadirItemEnSlotDisponible(InventarioItem item, int cantidad)
    {
        for (int i = 0;i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] == null)
            {
                itemsInventario[i] = item.CopiarItem();
                itemsInventario[i].Cantidad = cantidad;
                InventarioUI.Instance.DibujarItemEnInventario(item, cantidad, i);
                return;
            }
        }
    }

    private void EliminaItem(int index)
    {
        itemsInventario[index].Cantidad--;
        if (itemsInventario[index].Cantidad <= 0)
        {
            itemsInventario[index].Cantidad = 0;
            itemsInventario[index] = null;
            InventarioUI.Instance.DibujarItemEnInventario(null, 0, index);
        }
        else
        {
            InventarioUI.Instance.DibujarItemEnInventario(ItemsInventario[index], itemsInventario[index].Cantidad, index);
                
        }
    }

    public void MoverItem(int indexInicial, int indexFinal)
    {
        if (itemsInventario[indexInicial] == null || itemsInventario[indexFinal] != null)
        {
            return;
        }
        // copiar el item en el slot final
        InventarioItem itemPorMover = itemsInventario[indexInicial].CopiarItem();
        itemsInventario[indexFinal] = itemPorMover;
        InventarioUI.Instance.DibujarItemEnInventario(itemPorMover, itemPorMover.Cantidad, indexFinal);

        //Borramos item de slot inicial
        itemsInventario[indexInicial] = null;
        InventarioUI.Instance.DibujarItemEnInventario(null, 0, indexInicial);

    }

    private void UsarItem(int index)
    {
        if (itemsInventario[index] == null)
        {
            return;
        }
        if (itemsInventario[index].UsarItem())
        {
            EliminaItem(index);
        }
    }

    #region Eventos

    private void slotInteraccionRespuesta(TiposDeInteraccion tipo, int index)
    {
        switch (tipo)
        {
            case TiposDeInteraccion.Usar:
                UsarItem(index);
                break;
            case TiposDeInteraccion.Equipas:
                break;
            case TiposDeInteraccion.Remover:
                break;
        }
    }
    private void OnEnable()
    {
        InventarioSlot.EventoSlotinteraccion += slotInteraccionRespuesta;
    }

    private void OnDisable()
    {
        InventarioSlot.EventoSlotinteraccion -= slotInteraccionRespuesta;
    }
    #endregion
}
