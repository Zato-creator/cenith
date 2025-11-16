using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Es un editor customizado
[CustomEditor(typeof(Waypoint))]
public class NewBehaviourScript : Editor
{
   // obtener el objetivo asia quien va dirigido este editor
   Waypoint WaypointTargert => target  as Waypoint;

    private void OnSceneGUI()
    {
        // este metodo es para poder mover nuestros puntos con el puntero del raton
        Handles.color = Color.red;
        
        if (WaypointTargert == null)
        {
            return;
        }
        // utilizaremos un ciclo for que recorra toda la clase punto
        for (int i = 0; i < WaypointTargert.Puntos.Length; i++)
        {
            // primero verificamos cualqueir tipo de cambios que se ha hecho en el editor
            EditorGUI.BeginChangeCheck();   

            //Obtenemos la posicion actuial de cada punto en la escena
            Vector3 puntoActual = WaypointTargert.PosicionActual + WaypointTargert.Puntos[i];
            Vector3 nuevoPunto = Handles.FreeMoveHandle(puntoActual, Quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

            //Crear un texto debajo de cada punto que nos diga cuantos puntos tenemos en la zona
            GUIStyle texto = new GUIStyle();    
            texto.fontStyle = FontStyle.Bold;
            texto.fontSize = 16;
            texto.normal.textColor = Color.black;
            Vector3 alienamiento = Vector3.down * 0.3f + Vector3.right * 0.3f;
            Handles.Label(WaypointTargert.PosicionActual + WaypointTargert.Puntos[i] + alienamiento, $"{i + 1}", texto);


            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move Handle");
                WaypointTargert.Puntos[i] = nuevoPunto - WaypointTargert.PosicionActual ;
            }
        }
    }
}
