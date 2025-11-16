using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteraccionExtraNpc
{
    Quest,
    Tienda,
    Crafting
}

[CreateAssetMenu]
public class NPCDialogo : ScriptableObject
{
    [Header("Info")]
    public string Nombre;
    public Sprite Icono;
    public bool ContineinteraccionExtra;
    public InteraccionExtraNpc InteraccionExtra;

    [Header("Saludo")]
    [TextArea] public string Saludo;

    [Header("Chat")]
    public DialogoTexto [] Conversacion;

    [Header("Despedida")]
    [TextArea] public string Despedida;
}
[System.Serializable]
public class DialogoTexto
{
    [TextArea] public string Oracion;
}