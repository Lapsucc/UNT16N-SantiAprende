using System;
using UnityEngine;

/// <summary>
/// MODIFICACION SCRIPT SANTI - Para ajustar al main menu. 
/// </summary>


[CreateAssetMenu(fileName = "Santi", menuName = "KidProfileMenu", order = 0)]
public class KidProfileMenu : ScriptableObject
{
    [Header("Descripción De Santi")]
    public string Nombre;
    public Sprite Foto;
    public int ID;
    [Multiline(3)]
    public string Descripción;

    [Header("Kid Behaviour")]
    [Range(0, 1)] public float comportamientoSocial;
    [Range(0, 1)] public float interaccionSocial;
    [Range(0, 1)] public float interes;
    [Range(0, 1)] public float comportamientoRutina;
    [Range(0, 1)] public float necesidadApoyo;

    [Header("Animaciones")]
    public string[] anim;

    [Header("Interes")]
    [Range(0, 1)] public float Interes;

}

