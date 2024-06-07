using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class Emojis : MonoBehaviour
{
    #region Variables Codigo Original Santiago
    public CameraManager cameraEnfoque;
    //public Behaviours scriptableObject;
    public GameObject panelToShowImage;
    public float animationTime = 3f;
    public float animationTime2 = 1f;
    public Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] GameObject panelkid;
    [SerializeField] GameObject panelkidConfundido;
    [SerializeField] GameObject panelkidNada;
    [SerializeField] NavMeshAgent Kid;
    [SerializeField] NavMeshAgent Woman;
    #endregion
    //     

    private void Start()
    {
        cameraEnfoque.GetComponent<CameraManager>();
    }
    //public void StartAnimation(Behaviours newScriptableObject)
    public void StartAnimation()
    {
        //scriptableObject = newScriptableObject;        
        OnButtonClickWm();
        StartCoroutine(ShowPanelWm());
        StartCoroutine(WaitAndChoosePanel());
    }
     // Duración en segundos que el panel estará activo
    // Método para activar el panel y aplicar la animación de crecimiento
    IEnumerator ShowPanelWm()
    {
        Woman.isStopped = true;
        //OnButtonClickWm();
        // Escala inicial del panel
        Vector3 initialScale = Vector3.zero;
        cameraEnfoque.EnfocarPsicologa();
        // Animación de agrandamiento
        float elapsedTime = 0f;
        while (elapsedTime < animationTime)
        {    
            panelToShowImage.SetActive(true);
            float t = elapsedTime / animationTime2;
            panelToShowImage.transform.localScale = Vector3.MoveTowards(initialScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
            
        }
        panelToShowImage.transform.localScale = targetScale; // Asegura que la escala sea exactamente la deseada al final de la animación
        // Animación de encogimiento
        elapsedTime = 0f;
        
        while (elapsedTime < animationTime)
        {
            float t = elapsedTime / animationTime2;
            panelToShowImage.transform.localScale = Vector3.MoveTowards(targetScale, initialScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }  
        panelToShowImage.transform.localScale = initialScale; // Asegura que la escala sea exactamente la deseada al final de la animación
        panelToShowImage.SetActive(false); // Desactiva el panel al finalizar la animación
        yield break;
    }
    IEnumerator Showpanelkid() // EL NIÑO HIZO LA ACCION
    {
        OnButtonClickKid();
        // Escala inicial del panel
        Vector3 initialScale = Vector3.zero;
        // Animación de agrandamiento
        float elapsedTime = 0f;
        while (elapsedTime < animationTime)
        {
            panelkid.SetActive(true);
            float t = elapsedTime / animationTime2;
            panelkid.transform.localScale = Vector3.MoveTowards(initialScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panelkid.transform.localScale = targetScale; // Asegura que la escala sea exactamente la deseada al final de la animación

        // Animación de encogimiento
        elapsedTime = 0f;
        while (elapsedTime < animationTime)
        {
            float t = elapsedTime / animationTime2;
            panelkid.transform.localScale = Vector3.MoveTowards(targetScale, initialScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panelkid.transform.localScale = initialScale; // Asegura que la escala sea exactamente la deseada al final de la animación
        panelkid.SetActive(false); // Desactiva el panel al finalizar la animación

        //****
        
    }
    IEnumerator ShowpanelkidConfundido()
    {
        // Escala inicial del panel
        Vector3 initialScale = Vector3.zero;

        // Animación de agrandamiento
        float elapsedTime = 0f;
        while (elapsedTime < animationTime)
        {
            panelkidConfundido.SetActive(true);
            float t = elapsedTime / animationTime2;
            panelkidConfundido.transform.localScale = Vector3.MoveTowards(initialScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panelkidConfundido.transform.localScale = targetScale; // Asegura que la escala sea exactamente la deseada al final de la animación

        // Animación de encogimiento
        elapsedTime = 0f;
        while (elapsedTime < animationTime)
        {
            float t = elapsedTime / animationTime2;
            panelkidConfundido.transform.localScale = Vector3.MoveTowards(targetScale, initialScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panelkidConfundido.transform.localScale = initialScale; // Asegura que la escala sea exactamente la deseada al final de la animación
        panelkidConfundido.SetActive(false); // Desactiva el panel al finalizar la animación

        //****
        

    }
    IEnumerator ShowpanelkidNada()
    {
        // Escala inicial del panel
        Vector3 initialScale = Vector3.zero;

        // Animación de agrandamiento
        float elapsedTime = 0f;
        while (elapsedTime < animationTime)
        {
            panelkidNada.SetActive(true);
            float t = elapsedTime / animationTime2;
            panelkidNada.transform.localScale = Vector3.MoveTowards(initialScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panelkidNada.transform.localScale = targetScale; // Asegura que la escala sea exactamente la deseada al final de la animación

        // Animación de encogimiento
        elapsedTime = 0f;
        while (elapsedTime < animationTime)
        {
            float t = elapsedTime / animationTime2;
            panelkidNada.transform.localScale = Vector3.MoveTowards(targetScale, initialScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panelkidNada.transform.localScale = initialScale; // Asegura que la escala sea exactamente la deseada al final de la animación
        panelkidNada.SetActive(false); // Desactiva el panel al finalizar la animación

        //****
        
    }
    
    private IEnumerator WaitAndChoosePanel()                                        // AQUI SE VA APONER LA LOGICA DEL SUCCESS DE LAS ACCIONES
    {
        Kid.isStopped = true;
        // Espera 3 segundos
        yield return new WaitForSeconds(3);

        // Elección aleatoria (0: mismo panel, 1: dos paneles diferentes)
        int randomChoice = Random.Range(0, 3);
        
        yield return new WaitForSeconds(1);
        cameraEnfoque.EnfocarNiño();

        if (randomChoice == 0)
        {
            // Reproduce el mismo panel de PlayerA

            Debug.Log("¡Lo hizo!");
            StartCoroutine(Showpanelkid());
        }

        
        if (randomChoice == 1)
        {
            // Reproduce el mismo panel de PlayerA
            Debug.Log("NO SABE");
            StartCoroutine(ShowpanelkidConfundido());
        }

        
        if (randomChoice == 2)
        {
            // Reproduce el mismo panel de PlayerA
            Debug.Log("NO LO HIZO");
            StartCoroutine(ShowpanelkidNada());
        }

        yield return new WaitForSeconds(4);
        cameraEnfoque.EnfocarFollow();
        Woman.isStopped = false;
        Kid.isStopped = false;
        yield break;
    }
    // Método llamado al presionar el botón
    public void OnButtonClickWm()
    {
        /*
        // Obtener la imagen del Scriptable Object
        Sprite image = scriptableObject.icon;

        // Mostrar la imagen en el panel
        ShowImageWm(image);
        panelToShowImage.SetActive(true);
        */
    }
    public void OnButtonClickKid()
    {
        /*
        // Obtener la imagen del Scriptable Object
        Sprite image = scriptableObject.icon;

        // Mostrar la imagen en el panel
        ShowImageKid(image);
        panelkid.SetActive(true);
        */
    }

    // Método para mostrar la imagen en el panel
    private void ShowImageWm(Sprite imageToShow)
    {
        // Obtener el componente de imagen del panel
        SpriteRenderer panelImage = panelToShowImage.GetComponent<SpriteRenderer>();
        // Asignar la imagen al componente de imagen del panel
        panelImage.sprite = imageToShow;
    }
    private void ShowImageKid(Sprite imageToShow)
    {
        // Obtener el componente de imagen del panel
        SpriteRenderer panelImage = panelkid.GetComponent<SpriteRenderer>();
        // Asignar la imagen al componente de imagen del panel
        panelImage.sprite = imageToShow;
    }
    public void Acciones()
    {

    }
}



