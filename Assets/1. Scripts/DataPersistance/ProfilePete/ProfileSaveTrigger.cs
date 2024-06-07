using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Cinemachine.DocumentationSortingAttribute;

public class ProfileSaveTrigger : MonoBehaviour
{
    public static ProfileData s_currentProfile;
    public string nombrePerfil;

    void Start()
    {
        if (s_currentProfile != null)
        {
            nombrePerfil = s_currentProfile.name;
        }
        else
        {
            Debug.LogError("s_currentProfile no está inicializado!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (s_currentProfile != null)
            {
                Debug.Log("Presionando P de Salvar");
                //ProfileStorage.StorePlayerProfile();
            }
            else
            {
                Debug.LogError("s_currentProfile no está inicializado!");
            }
        }
    }


    public void GetPosition()
    {

    }
}

