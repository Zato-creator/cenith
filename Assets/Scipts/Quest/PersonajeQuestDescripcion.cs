using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonajeQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI tareaobjetivo;
    [SerializeField] private TextMeshProUGUI recompensaOro;
    [SerializeField] private TextMeshProUGUI recompensaExp;

    [Header("Item")]
    [SerializeField] private Image recompensaItemIcono;
    [SerializeField] private TextMeshProUGUI recompensaItemCantidad;


    private void Update()
    {
        if (QuestPorCompletar.QuestCompletado)
        {
            return;
        }

        tareaobjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}";
    }
    public override void ConfigurarQuestUI(Quest questPorCargar)
    {

        base.ConfigurarQuestUI(questPorCargar);
        recompensaOro.text = questPorCargar.RecompensaOro.ToString();  
        recompensaExp.text = questPorCargar.RecompensaExp.ToString();
        tareaobjetivo.text = $"{questPorCargar.CantidadActual}/{questPorCargar.CantidadObjetivo}";

        recompensaItemIcono.sprite = questPorCargar.RecompensaItem.Item.Icono;
        recompensaItemCantidad.text = questPorCargar.RecompensaItem.Cantidad.ToString();
    }

    private void QuestCompletadoRespuesta(Quest questCompletado)
    {
        if (questCompletado.ID  == QuestPorCompletar.ID)
        {
            tareaobjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}";
            gameObject.SetActive(false);        
        }
    }

    private void OnEnable()
    {
        if (QuestPorCompletar.QuestCompletado)
        {
            gameObject.SetActive(false);
        }
        Quest.EventoQuestCompletado += QuestCompletadoRespuesta;
    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuesta;
    }
}
