using UnityEngine;
using UnityEngine.UI; // Importante para acceder a los componentes de UI

public class SuccessBController : MonoBehaviour
{
    public string behaviorName;
    public string iD;
    public int levelBehavior = 1;
    public float levelExperienceValue;
    public Image imageComponent; // Referencia al componente Image
    public float behaviorValueResult = 5;
    public bool lvlUp = false;

    void Start()
    {   
        imageComponent = GetComponent<Image>();
    }

    void Update()
    {

        if (levelExperienceValue > 100 && !lvlUp)
        {                    
            BehaviorLvlUp();
        }


        if (behaviorValueResult == 1)
        {
            OnSuccessBehavior();
        }
        if (behaviorValueResult == 2)
        {
            OnFailBehavior();
        }
        if (behaviorValueResult == 3)
        {
            OnMissBehavior();
        }
    }

    public void BehaviorLvlUp()
    {
        levelBehavior++;
        Debug.Log("Subiendo de nivel: " + this.gameObject.name + " al: " + levelBehavior);
        lvlUp = false;
    }

    // LOGICAS MOMENTANEAS!! DE ACCION SEGUN RESULTADO

    public void OnSuccessBehavior()
    {        
        imageComponent.color = new Color32(0, 255, 0, 255);
    }

    public void OnFailBehavior()
    {     
        imageComponent.color = new Color32(255, 0, 0, 255);
    }

    public void OnMissBehavior()
    {     
        imageComponent.color = new Color32(0, 0, 255, 255);
    }


}
