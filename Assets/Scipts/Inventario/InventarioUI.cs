using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventarioUI : Singleton<InventarioUI>
{
    [Header("Panel Inventario Descripcion")]
    [SerializeField] private GameObject panelInventarioDescripcion;
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDescripcion;

    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor;

    public int IndexSlotIniciaPorMover { get; private set; }
    public InventarioSlot SlotSeleccionado { get; private set; }

    List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>();
    void Start()
    {
        Inicializarinventario();
        IndexSlotIniciaPorMover = -1;
    }

    private void Update()
    {
        ActualizarSlotSeleccionado();
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (SlotSeleccionado != null )
            {
                IndexSlotIniciaPorMover = SlotSeleccionado.Index;   
            }
        }
    }
    private void Inicializarinventario()
    {
        for (int i = 0; i < Inventario.Instance.NumeroDeSlots; i++)
        {
            InventarioSlot nuevoSlot = Instantiate(slotPrefab, contenedor);
            nuevoSlot.Index = i;
            slotsDisponibles.Add(nuevoSlot);
        }
    }

    private void ActualizarSlotSeleccionado()
    {
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;
        if (goSeleccionado == null)
        {
            return;
        }

        InventarioSlot slot = goSeleccionado.GetComponent<InventarioSlot>();
        if (slot != null)
        {
             SlotSeleccionado = slot;
        }
    }
    public void DibujarItemEnInventario(InventarioItem itemPorAñadir, int cantidad, int itemIndex)
    {
        InventarioSlot slot = slotsDisponibles[itemIndex];
        if (itemPorAñadir != null)
        {
            slot.ActivarSlotUI(true);
            slot.ActualizarSlot(itemPorAñadir, cantidad);
        }
        else
        {
            slot.ActivarSlotUI(false); 
        }
    }
    private void ActualizarInventarioDescripcion(int index)
    {
        if (Inventario.Instance.ItemsInventario[index] != null)
        {
            itemIcono.sprite = Inventario.Instance.ItemsInventario[index].Icono;
            itemNombre.text = Inventario.Instance.ItemsInventario[index].nombre;
            itemDescripcion.text = Inventario.Instance.ItemsInventario[index].Descripcion;
            panelInventarioDescripcion.SetActive(true);
        }
        else
        {
            panelInventarioDescripcion.SetActive(false);
        }
    }

    #region Evento
    private void slotInteraccionRespuesta(TiposDeInteraccion tipo, int index)
    {
        if (tipo == TiposDeInteraccion.Click)
        {
            ActualizarInventarioDescripcion(index);
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

    public void UsarItem()
    {
        if(SlotSeleccionado != null)
        {
            SlotSeleccionado.slotusarItem();  
            SlotSeleccionado.SeleccionarSlot(); 
        }
    } 

}
