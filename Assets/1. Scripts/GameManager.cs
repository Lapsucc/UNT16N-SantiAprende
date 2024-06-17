using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int profileID;
    void Start()
    {
        profileID = PlayerDataStatic.profileID;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("El prefil actual es: " + profileID);
        }
    }
}
