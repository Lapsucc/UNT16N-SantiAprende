using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GP: GainPercentage
/// </summary>

public class GameManager : MonoBehaviour
{
    [Header("Perfil Santi")]
    public santiProfileManager sProfileID;
    public int santiProfileID;
    public bool isSociable;//__________________________ InterestVarBalueController, AproachDecreseValueController, RandomMovement
    [Header("")]
    public float gainInterestPercentage;//_________ InterestBarValueController(PositiveBarValue()), SantiInteractiveObjectController, Ventana y Stereo Controllers,
    public float lossInterestPercentage;//_________ InterestBarValueController(NegativeBarValue(), ReduceVarBalue(Del Aproach))
    public float santiActionGP;//______________________ 
    public float santiObjectGP;//______________________ SantiInteractiveObjectController
    [Header("Gustos Santi")]
    public bool likesMusic;//__________________________ StereoController
    public float MusicGP;//____________________________ StereoController 
    public bool likesFreeSpace;//_______________________ VentanaController
    public float freeSpaceGP;//_________________________ VentanaController
    public bool likesLights;
    public float lightsGP;
    [Header("Acercamiento")]
    public bool nearPsicologistLost;//_________________ InterestVarBalueController, 
    public float nearPsicologistLostPercentage;//______ InterestVarBalueController, 
    [Header("Valores NavMesh")]
    public float speedNavmeshP;
    public float timerSetDestination;


    private void Start()
    {
        Invoke("GetSantiIdGM", 0.5f);

    }

    void Update()
    {
        if (santiProfileID == 1)
        {
            isSociable = true;
            gainInterestPercentage = 1.2f;
            lossInterestPercentage = 0.5f;
            santiActionGP = 1f;
            santiObjectGP = 1f;
            likesMusic = false;
            MusicGP = 1;
            likesFreeSpace = true;
            freeSpaceGP = 1.2f;
            likesLights = true;
            lightsGP = 1.2f;
            nearPsicologistLost = false;
            nearPsicologistLostPercentage = 1;
        }

        if (santiProfileID == 2)
        {
            isSociable = true;
            gainInterestPercentage = 1.2f;
            lossInterestPercentage = 0.5f;
            santiActionGP = 1f;
            santiObjectGP = 1f;
            likesMusic = true;
            MusicGP = 1;
            likesFreeSpace = false;
            freeSpaceGP = 1.2f;
            likesLights = false;
            lightsGP = 1.2f;
            nearPsicologistLost = true;
            nearPsicologistLostPercentage = 1;
        }

    }

    public void GetSantiIdGM()
    {
        santiProfileID = sProfileID.santiProfileID;

        if (santiProfileID == 0)
        {
            isSociable = true;
            gainInterestPercentage = 1f;
            lossInterestPercentage = 1f;
            santiActionGP = 1f;
            santiObjectGP = 1f;
            likesMusic = true;
            MusicGP = 1;
            likesFreeSpace = true;
            freeSpaceGP = 1f;
            likesLights = true;
            lightsGP = 1f;
            nearPsicologistLost = true;
            nearPsicologistLostPercentage = 1;
            speedNavmeshP = 0.7f;
            timerSetDestination = 5;
        }
    }
}
