using UnityEngine;
/// <summary> PARA ELIMINAR DESPUES DE PRUEBAS 04/06
/// 
/// </summary>


public class ExperienceController : MonoBehaviour
{
    public float experience;
    public float expUp;
    public int level;
    public bool expUpBool = false;

    public void Start()
    {

    }

    private void Update()
    {
        if (experience >= 100)
        {
            level = 2;
        }
        if (experience >= 200)
        {
            level = 3;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            expUpBool = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !expUpBool)
        {
            Debug.Log("Presionando Espacio + Ganar Experiencia");
            Debug.Log("exp +" + expUp);
            ExperienceGained();
        }
    }
    public void ExperienceGained()
    {
        experience += expUp; // Valor a guardar.
        expUpBool = true;
    }
}


/// <summary> PARA ELIMINAR DESPUES DE PRUEBAS 04/06
/// 
/// </summary>