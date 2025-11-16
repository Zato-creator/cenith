using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaConfiner : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Camara;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Camara.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Camara.gameObject.SetActive(false);
    }
}
