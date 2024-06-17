using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KidDataController : MonoBehaviour
{
    private ProfilesMenuController Niño;
    private KidProfileMenu inf;
    public Image FotoPerfil;
    public TMP_Text Descr;
    public TMP_Text Name;
    public Image[] perfil = new Image[5];
    public void ViewInfoKid(KidProfileMenu newniñoSelector)
    {
        inf = newniñoSelector;
        InfoKid();
    }
    public void InfoKid()
    {
        Descr.text = inf.Descripción;
        Name.text = inf.Nombre;
        FotoPerfil.sprite = inf.Foto;

        StartCoroutine(Values(perfil[0], inf.comportamientoSocial));
        StartCoroutine(Values(perfil[1], inf.interaccionSocial));
        StartCoroutine(Values(perfil[2], inf.interes));
        StartCoroutine(Values(perfil[3], inf.comportamientoRutina));
        StartCoroutine(Values(perfil[4], inf.necesidadApoyo));
    }
    private IEnumerator Values(Image img, float value)
    {
        while (img.fillAmount != value)
        {
            img.fillAmount = Mathf.MoveTowards(img.fillAmount, value, 5 * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }
}
