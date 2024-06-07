using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary> Comentarios -
/// CREACION, CARGA, BORRAR el archivo .json
/// </summary>

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    
    private readonly string encryptionCodeWord = "word";        // ENCRIPTAR DATOS - TUTO
    private readonly string backupExtension = ".bak";           // Back Up de los datos.

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {       
        this.dataDirPath = Path.Combine(Application.dataPath, "DatosDeUsuario", "Slots"); // Ruta de almacenamiento y creacion de los slots.
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }
    // ************************************************************************************************************************************************************
    public DatosJuego Load(string profileId, bool allowRestoreFromBackup = true)
    {
        if (profileId == null)
        {
            return null;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        DatosJuego loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<DatosJuego>(dataToLoad);
            }
            catch (Exception e)
            {
                if (allowRestoreFromBackup)
                {
                    Debug.LogError("Error al cargar el juego guardado, se intentara hacer cargar un Back Up.\n" + e);
                    bool rollbackSuccess = AttemptRollBack(fullPath);
                    if (rollbackSuccess)
                    {
                        loadedData = Load(profileId, false);
                    }
                }
                else
                {
                    Debug.Log("Ocurrio un error al tratar de cargar el archivo back up");
                }
            }
        }
        return loadedData;
    }
    // ************************************************************************************************************************************************************
    public void save(DatosJuego data, string profileId)
    {
        if (profileId == null)
        {
            return;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        string backupFilePath = fullPath + backupExtension;

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);

            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

            DatosJuego verifiedGameData = Load(profileId);
            if (verifiedGameData != null)
            {
                File.Copy(fullPath, backupFilePath, true);
            }
            else
            {
                throw new Exception("El save file no se puede verificar y el back up no puede ser creado");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error al tratar de salvar" + fullPath + "\n" + e);
        }
    }
    // ************************************************************************************************************************************************************
    public void Delete(string profileId)
    {
        if (profileId == null)
        {
            return;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        try
        {
            if (File.Exists(fullPath))
            {
                Directory.Delete(Path.GetDirectoryName(fullPath), true);
            }
            else
            {
                Debug.LogWarning("Intento borrar data, pero no se encontro que borrar: " + fullPath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("No se pudo borrar el data profileId: " + profileId + "at path: " + fullPath + "\n" + e);
        }
    }
    // ************************************************************************************************************************************************************
    public Dictionary<string, DatosJuego> LoadAllProfiles()
    {
        Dictionary<string, DatosJuego> profileDictionary = new Dictionary<string, DatosJuego>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();

        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Saltando Directorio al cargar todos los perfiles por que no contiene datos: " + profileId);
                continue;
            }

            DatosJuego profileData = Load(profileId);
            if (profileData != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("Intento cargar el perfil pero algo salio mal, Perfil: " + profileId);
            }
        }
        return profileDictionary;
    }
    // ************************************************************************************************************************************************************
    public string GetMostRecentlyUpdatedProfileId()
    {
        string mostRecentProfileId = null;
        Dictionary<string, DatosJuego> profilesGameData = LoadAllProfiles();
        foreach (KeyValuePair<string, DatosJuego> pair in profilesGameData)
        {
            string profileId = pair.Key;
            DatosJuego gameData = pair.Value;

            if (gameData == null)
            {
                continue;
            }

            if (mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);

                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileId = profileId;
                }
            }
        }
        return mostRecentProfileId;
    }
    // ************************************************************************************************************************************************************
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }

    private bool AttemptRollBack(string fullPath)
    {
        bool success = false;
        string backupFilePath = fullPath + backupExtension;
        try
        {
            if (File.Exists(backupFilePath))
            {
                File.Copy(backupFilePath, fullPath, true);
                success = true;
                Debug.LogWarning("Se tuvo que cargar el back up: " + backupFilePath);
            }
            else
            {
                throw new Exception("Intento cargar un back up, pero no hay archivos para hacerlo");
            }
        }
        catch (Exception e)
        {
            Debug.Log("Ocurrio un error al tratar de cargar el back up en: " + backupFilePath + "\n" + e);
        }
        return success;
    }
}

/// <summary> Recursos - Tutorial Carga datos .json
/// Tutorial: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-tMGkSApPdu4hlUBagKial&index=1&t=0s
/// </summary>