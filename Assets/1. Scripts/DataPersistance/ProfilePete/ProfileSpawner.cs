using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileSpawner : MonoBehaviour
{
    public Transform newGameSpawn;
    public GameObject playerPrefab;

    private void Start()
    {
        if (ProfileStorage.s_currentProfile == null)
        {
            //Para el tutorial, hace un instanciado del personaje.
            //Pero es la condicion de spawn PERO ES LA CONDICION INICIAL

            Debug.Log("Juego Nuevo");
        }
        else
        {
            //Carga Partida
            Debug.Log("Carga exitosa");

        }
    }
}
