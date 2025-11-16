using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Personaje")]
    [SerializeField] private Personaje personaje;

    [Header("Quest")]
    [SerializeField] private Quest[] questDisponibles;

    [Header("inspector Quest")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestcontenedor;

    [Header("personaje Quest")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestcontenedor;

    [Header("Panel Quest Completado")]
    [SerializeField] private GameObject panelQuestcompletado;
    [SerializeField] private TextMeshProUGUI questRecompensaOro;
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questRecompensaExp; 
    [SerializeField] private TextMeshProUGUI questRecompensaItemCantidad;
    [SerializeField] private Image questRecompensaItemIcono;
    
     public Quest QuestPorReclamar {  get; private set; }


    private void Start()
    {
        CargarQuestEnInspector();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AñadirProgreso("Mata10", 1 );
            AñadirProgreso("Mata25", 1 );
            AñadirProgreso("Mata50", 1 );
        }
    }

    private void CargarQuestEnInspector()
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestcontenedor);
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]);
        }
    }

    private void AñadirQuestPorCompletar (Quest questPorCompletar)
    {
        PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestcontenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    public void AñadirQuest (Quest questPorCompletar)
    {
        AñadirQuestPorCompletar(questPorCompletar);
    }

    public void ReclamarRecompensa()
    {
        if (QuestPorReclamar == null)
        {
            return;
        }

        MonedasManager.Instance.AñadirMonedas(QuestPorReclamar.RecompensaOro);
        personaje.PersonajeExperiencia.AñadirExperiencia(QuestPorReclamar.RecompensaExp);
        Inventario.Instance.AñadirItem(QuestPorReclamar.RecompensaItem.Item, QuestPorReclamar.RecompensaItem.Cantidad);
        panelQuestcompletado.SetActive(false);
        QuestPorReclamar = null;    
    }
    public void ReclkamarRecompensa()
    {

    }
    public void AñadirProgreso(string questID, int cantidad )
    {
        Quest QuestPorActualizar = QuestExiste(questID);

        if (QuestPorActualizar == null)
        {
            Debug.LogWarning($"[QuestManager] No se encontró la Quest con ID: {questID}");
            return;
        }

        QuestPorActualizar.AñadirProgreso(cantidad);
    }

    private Quest QuestExiste(string questID)
    {
        for(int i = 0; i < questDisponibles.Length; i++)
        {
            if (questDisponibles[i].ID == questID)
            {
                return questDisponibles[i]; 
            }
        }
        return null;
    }

    private void MostrarQuestCompletado(Quest questCompletado)
    {
        panelQuestcompletado.SetActive(true);
        questNombre.text = questCompletado.nombre;
        questRecompensaOro.text = questCompletado.RecompensaOro.ToString();
        questRecompensaExp.text = questCompletado.RecompensaExp.ToString();
        questRecompensaItemCantidad.text = questCompletado.RecompensaItem.Cantidad. ToString();
        questRecompensaItemIcono.sprite = questCompletado.RecompensaItem.Item.Icono;
    }

    private void QuestCompletadoRespuesta(Quest questCompletado)
    {
        QuestPorReclamar = QuestExiste(questCompletado.ID);
        if (QuestPorReclamar != null)
        {
            MostrarQuestCompletado(QuestPorReclamar);
        }
    }
    private void OnEnable()
    {
        Quest.EventoQuestCompletado += QuestCompletadoRespuesta;
    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuesta;
    }
}
