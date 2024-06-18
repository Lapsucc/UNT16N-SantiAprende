using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class santiProfileManager : MonoBehaviour, IDataPersistance
{
    public int santiProfileID;    

    private void Start()
    {
        if (PlayerDataStatic.santiProfileID != 0)
        {
            Invoke("ObtainSantProfileId", 0.5f);
        }
        else
        {
            Debug.Log("El valor del STATIC es 0, pero el valor de SantiProfile es: " + santiProfileID);
        }
    }
    public void LoadData(DatosJuego data)
    {
        this.santiProfileID = data.santiProfileId;
    }

    public void SaveData(DatosJuego data)
    {
        data.santiProfileId = this.santiProfileID;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("El prefil actual es desde santiProfileManager: " + santiProfileID);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("El prefil actual es desde santiProfileManager: " + PlayerDataStatic.santiProfileID);
        }
    }

    public void ObtainSantProfileId()
    {
        santiProfileID = PlayerDataStatic.santiProfileID;
    }
}
