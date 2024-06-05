using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Con el IDATAPersistance - Se hace el grabar y cargar partida

public class MovimientoCamaraController : MonoBehaviour, IDataPersistance // Aqui se añade el IDATA
{
    public float speedX = 5.0f; 
    public float speedZ = 7.0f; 
    private Rigidbody rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>(); // Asegura que haya un Rigidbody
        }
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // ************ SAVE AND LOAD


    public void LoadData(DatosJuego data)
    {
        this.transform.position = data.camPos;
    }

    public void SaveData(DatosJuego data)
    {
        data.camPos = this.transform.position;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadSceneAsync("MainMenu");        
        }
    }

    // ************ 

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(0f, 0f, 0f);

        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -speedX;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement.x = speedX; 
        }

        if (Input.GetKey(KeyCode.W))
        {
            movement.z = speedZ; 
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement.z = -speedZ; 
        }
       
        rb.velocity = transform.TransformDirection(movement);
    }
}
