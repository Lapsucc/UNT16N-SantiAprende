using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfilesMenuController : MonoBehaviour
{
    [Header("Perfiles")]
    [SerializeField] private List<KidProfileMenu> profiles = new();

    [Header("Instancias")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform perfParent;

    [SerializeField] private SaveSlotsMenu saveSlotsMenu;
    public GameObject profileMenu;

    

    private void Start()
    {
        foreach (var item in profiles)
        {
            // Instanciar el prefab
            GameObject btn = Instantiate(prefab, perfParent);
            // Debug.Log("Instantiated button for profile: " + item.name);

            // Configurar la imagen y el texto del botón
            btn.transform.GetChild(0).GetComponent<Image>().sprite = item.Foto;
            btn.transform.GetChild(1).GetComponent<TMP_Text>().text = item.name;

            // Obtener el componente Button
            Button click = btn.GetComponent<Button>();

            if (click != null)
            {                
                KidProfileMenu currentItem = item;                
                click.onClick.AddListener(() => ShowProfileName(currentItem));
                click.onClick.AddListener(() => ChooseProfile(currentItem));
                //Debug.Log("Button listener added for: " + currentItem.name);
            }
            else
            {
                //Debug.LogError("No Button component found on the instantiated prefab for profile: " + item.name);
            }
        }
    }

    private void ChooseProfile(KidProfileMenu profileId)
    {              
        PlayerDataStatic.santiProfileID = profileId.ID;
        saveSlotsMenu.ActivateMenu(false);
        profileMenu.SetActive(false);
        //SceneManager.LoadSceneAsync("EscenaGYM");
        //saveSlotMenu.SetActive(true);
    }

    private void ShowProfileName(KidProfileMenu profile)
    {
        
        Debug.Log("Profile Name: " + profile.name);
    }
}
