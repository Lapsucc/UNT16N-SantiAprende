using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public santiProfileManager sProfileID;
    public int santiProfileID;

    private void Start()
    {
        Invoke("GetSantiIdGM", 0.5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("El prefil actual es: " + santiProfileID);
        }
    }

    public void GetSantiIdGM()
    {
        santiProfileID = sProfileID.santiProfileID;
    }
}
