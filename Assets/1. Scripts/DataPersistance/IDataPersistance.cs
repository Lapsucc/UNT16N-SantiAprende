using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistance
{
    void LoadData(DatosJuego data);
    void SaveData(DatosJuego data);
}

/// <summary> Recursos - Tutorial Carga datos .json
/// Tutorial: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-tMGkSApPdu4hlUBagKial&index=1&t=0s
/// </summary>