using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject ni�o;
    public GameObject psicologa;
    public GameObject camaraPos;
    public FollowCinemachine followCinemachine;
    public MovimientoCamaraController movimientoCam;

    public bool enfocarCamaraPos;
    public bool enfocarPsicologa;
    public bool enfocarNi�o;

    void Start()
    {
        ni�o = GameObject.Find("Ni�o");
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
            EnfocarNi�o();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            EnfocarPsicologa();
        }

        if (enfocarCamaraPos) followCinemachine.SeguirObjeto(camaraPos);
        if (enfocarPsicologa) followCinemachine.SeguirObjeto(psicologa);
        if (enfocarNi�o) followCinemachine.SeguirObjeto(ni�o, true);

    }

    public void EnfocarFollow()
    {
        enfocarCamaraPos = true;
        enfocarNi�o = false;
        enfocarPsicologa = false;

        movimientoCam.enabled = true;
    }

    public void EnfocarNi�o()
    {
        enfocarCamaraPos = false;
        enfocarNi�o = true;
        enfocarPsicologa = false;

        movimientoCam.enabled = false;
    }

    public void EnfocarPsicologa()
    {
        enfocarCamaraPos = false;
        enfocarNi�o = false;
        enfocarPsicologa = true;

        movimientoCam.enabled = false;
    }
}
