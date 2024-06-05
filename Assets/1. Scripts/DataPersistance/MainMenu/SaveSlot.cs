using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Perfil")]
    [SerializeField] private string profileId;

    [Header("Contenido")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] public TextMeshProUGUI nombrePerfil; // PARA MOSTRARLO EN EL MENU

    [Header("Delete Button")]
    [SerializeField] public Button clearButton;
    public bool hasData { get; private set; } = false;
    public Button saveSlotButton;

    public UiController uiController;

    private void Awake()
    {
        saveSlotButton = this.GetComponent<Button>();
    }

    public void Start()
    {

    }

    private void Update()
    {
        // Esto permite ver el valor en tiempo real en el inspector
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public void SetData(DatosJuego data)
    {
        if (data == null) // CUANDO NO HAY DATOS - MENU SLOTS
        {
            hasData = false;
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
            clearButton.gameObject.SetActive(false);            
        }
        else
        {
            hasData = true;
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true); 
            nombrePerfil.text = data.profileId;
            clearButton.gameObject.SetActive(true);            
        }
    }

    public string GetProfileId()
    {
        return this.profileId;
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
        clearButton.interactable = interactable;
    }

    public string GetProfileIdToDelete()
    {
        Debug.Log("Debug mostrando el nombre de ID que es: " + nombrePerfil.text);
        return nombrePerfil.text;
    }

    // Este método se llama cuando se cambia un valor en el inspector
    private void OnValidate()
    {
        // Actualiza nombrePerfil con profileId en el inspector
        if (nombrePerfil != null)
        {
            nombrePerfil.text = profileId;
        }
    }
}
