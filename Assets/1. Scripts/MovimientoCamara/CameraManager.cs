using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject niño;
    public GameObject psicologa;
    public GameObject camaraPos;
    public FollowCinemachine followCinemachine;
    public MovimientoCamaraController movimientoCam;

    public bool enfocarCamaraPos;
    public bool enfocarPsicologa;
    public bool enfocarNiño;

    void Start()
    {
        niño = GameObject.Find("Niño");
        psicologa = GameObject.Find("Psicologa");
        camaraPos = GameObject.Find("ObjetoMovimientoCamara");
        followCinemachine = GameObject.Find("FollowCinemachine").GetComponent<FollowCinemachine>();
        movimientoCam = GameObject.Find("ObjetoMovimientoCamara").GetComponent<MovimientoCamaraController>();

        EnfocarFollow();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            EnfocarFollow();
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            EnfocarNiño();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            EnfocarPsicologa();
        }

        if (enfocarCamaraPos) followCinemachine.SeguirObjeto(camaraPos);
        if (enfocarPsicologa) followCinemachine.SeguirObjeto(psicologa);
        if (enfocarNiño) followCinemachine.SeguirObjeto(niño, true);

    }

    public void EnfocarFollow()
    {
        enfocarCamaraPos = true;
        enfocarNiño = false;
        enfocarPsicologa = false;

        movimientoCam.enabled = true;
    }

    public void EnfocarNiño()
    {
        enfocarCamaraPos = false;
        enfocarNiño = true;
        enfocarPsicologa = false;

        movimientoCam.enabled = false;
    }

    public void EnfocarPsicologa()
    {
        enfocarCamaraPos = false;
        enfocarNiño = false;
        enfocarPsicologa = true;

        movimientoCam.enabled = false;
    }
}
