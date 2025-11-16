using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{


    [Header("Stats")]
    [SerializeField] private PersonajeStats Stats;

    [Header("Paneles")]
    [SerializeField] private GameObject PanelStats;
    [SerializeField] private GameObject PanelInventario;
    [SerializeField] private GameObject PanelInspectorQuest;
    [SerializeField] private GameObject PanelPersonajeQuest;

    [Header("Barra")]
    [SerializeField] private Image vidaPlayer;
    [SerializeField] private Image manaPlayer;
    [SerializeField] private Image ExpPlayer;

    [Header("Texto")]
    [SerializeField] private TextMeshProUGUI vidaTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [SerializeField] private TextMeshProUGUI expTMP;
    [SerializeField] private TextMeshProUGUI nivelTMP;
    [SerializeField] private TextMeshProUGUI monedasTMP;


    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI StatDañoTMP;
    [SerializeField] private TextMeshProUGUI StatDefensaTMP;
    [SerializeField] private TextMeshProUGUI StatCriticoTMP;
    [SerializeField] private TextMeshProUGUI StatBloqueoTMP;
    [SerializeField] private TextMeshProUGUI StatVelocidadTMP;
    [SerializeField] private TextMeshProUGUI StatNivelTMP;
    [SerializeField] private TextMeshProUGUI StatExpTMP;
    [SerializeField] private TextMeshProUGUI StatExpRequeridaTMP;
    [SerializeField] private TextMeshProUGUI AtributoFuerzaTMP;
    [SerializeField] private TextMeshProUGUI AtributoInteligenciaTMP;
    [SerializeField] private TextMeshProUGUI AtributoDestrezaTMP;
    [SerializeField] private TextMeshProUGUI AtributosDisponiblesTMP;

    private float vidaActual;
    private float vidaMax;

    private float manaActual;
    private float manaMax;

    private float expActual;
    private float expRequeridaNuevoNivel;


    void Update()
    {
        ActualizarUIPersonaje();
        ActualizarPanelStats();
    }
    public void ActualizarVidaPersonaje(float pVidaActual, float pVidaMax)
    {
        vidaActual = pVidaActual;
        vidaMax = pVidaMax;      

    }

    public void ActualizarManaPersonaje(float pManaActual, float pManaMax)
    {
        manaActual = pManaActual;
        manaMax = pManaMax;

    }

    private void ActualizarUIPersonaje()
    {
        vidaPlayer.fillAmount = Mathf.Lerp(vidaPlayer.fillAmount, vidaActual / vidaMax, 10f * Time.deltaTime);
        manaPlayer.fillAmount = Mathf.Lerp(manaPlayer.fillAmount, manaActual / manaMax, 10f * Time.deltaTime);
        ExpPlayer.fillAmount = Mathf.Lerp(ExpPlayer.fillAmount, expActual / expRequeridaNuevoNivel, 10f * Time.deltaTime);
        monedasTMP.text = MonedasManager.Instance.MonedasTotales.ToString();

        vidaTMP.text = $"{vidaActual}/{vidaMax}";
        manaTMP.text = $"{manaActual}/{manaMax}";
        expTMP.text = $"{((expActual / expRequeridaNuevoNivel)* 100):F2}%";
        nivelTMP.text = $"Nivel {Stats.Nivel}";
    }

    private void ActualizarPanelStats()
    {
        if (PanelStats.activeSelf == false)
        {
            return;
        }
        StatDañoTMP.text = Stats.Daño.ToString();
        StatDefensaTMP.text = Stats.Defensa.ToString();
        StatCriticoTMP.text = $"{Stats.PorcentajeCritico}%";
        StatBloqueoTMP.text = $"{Stats.PorcentajeBloqueo}%";
        StatVelocidadTMP.text = Stats.Velocidad.ToString();
        StatNivelTMP.text = Stats.Nivel.ToString();
        StatExpTMP.text = Stats.ExpActual.ToString();
        StatExpRequeridaTMP.text = Stats.ExpRequeridaSiguienteNivel.ToString();

        AtributoFuerzaTMP.text = Stats.Fuerza.ToString();
        AtributoInteligenciaTMP.text = Stats.Inteligencia.ToString();
        AtributoDestrezaTMP.text = Stats.Destreza.ToString();
        AtributosDisponiblesTMP.text = $"Puntos: {Stats.PuntosDisponibles}";


    }
    public void ActualizarExpPersonaje(float pExpActual, float pExpRequerida)
    {
        expActual = pExpActual;
        expRequeridaNuevoNivel = pExpRequerida;

    }

    #region Paneles

    public void AbrirCerrarPanelStats()
    {
        PanelStats.SetActive(!PanelStats.activeSelf);
    }

    public void AbrirCerrarPanelInventario()
    {
        PanelInventario.SetActive(!PanelInventario.activeSelf);
    }

    public void AbrirCerrarPanelPersonajeQuest()
    {
        PanelPersonajeQuest.SetActive(!PanelPersonajeQuest.activeSelf); 
    }
    #endregion
    public void AbrirCerrarPanelInspectorQuest()
    {
        PanelInspectorQuest.SetActive(!PanelInspectorQuest.activeSelf);
    }

    public void AbrirPanelinteraccion(InteraccionExtraNpc tipoInteraccion)
    {
        switch (tipoInteraccion)
        {
            case InteraccionExtraNpc.Quest:
                AbrirCerrarPanelInspectorQuest();
                break;
            case InteraccionExtraNpc.Tienda:
                break;
            case InteraccionExtraNpc.Crafting:
                break;
        }
    }

}
