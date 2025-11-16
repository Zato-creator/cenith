using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedasManager : Singleton<MonedasManager>
{
    [SerializeField] private int monedasTest;
    public int MonedasTotales { get; set; }

    private string KEY_MONEDAS = "MYJUEGO_MONEDAS";

    private void Start()
    {
        PlayerPrefs.DeleteKey(KEY_MONEDAS);
        Cargarmonedas();
    }

    private void Cargarmonedas()
    {
        MonedasTotales = PlayerPrefs.GetInt(KEY_MONEDAS, monedasTest);
    }


    public void AñadirMonedas(int cantidad)
    {
        MonedasTotales += cantidad;
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales);
        PlayerPrefs.Save(); 
    }

    public void RemoverMOnedas(int Cantidad)
    {
        if (Cantidad > MonedasTotales)
        {
            return;
        }

        MonedasTotales -= Cantidad;
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales);
        PlayerPrefs.Save();
    }
}
