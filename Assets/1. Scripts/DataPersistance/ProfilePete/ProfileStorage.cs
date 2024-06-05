using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using System;

public static class ProfileStorage
{
    public static ProfileData s_currentProfile;

    // Nueva ruta de almacenamiento
    private static string s_indexPath = Application.dataPath + "/DatosDeUsuario/IndexPerfiles/_ProfileIndex_.xml";

    // **************************
    public static void CreateNewProfileName(string nombre)
    {
        s_currentProfile = new ProfileData(nombre);

        string path = Application.dataPath + "/DatosDeUsuario/IndexPerfiles/" + s_currentProfile.filename;
        SaveFile<ProfileData>(path, s_currentProfile);

        var index = GetProfileIndex();
        index.profileFileNames.Add(s_currentProfile.filename);
        SaveFile<ProfileIndex>(s_indexPath, index);
    }

    public static void StorePlayerProfile(string name)
    {
        s_currentProfile.filename = name;

        var path = Application.dataPath + "/DatosDeUsuario/IndexPerfiles/" + s_currentProfile.filename;
        SaveFile<ProfileData>(path, s_currentProfile);
    }

    // ************************* COMPROBAR SI EL ARCHIVO O NOMBRE DE PERFIL YA EXISTE

    public static bool ProfileExists(string profileName)
    {
        var index = GetProfileIndex();
        string filename = profileName.Replace(" ", " ") + ".xml";
        return index.profileFileNames.Contains(filename);
    }

    // *********************

    public static void LoadProfile(string filename)
    {
        var path = Application.dataPath + "/DatosDeUsuario/IndexPerfiles/" + filename;
        s_currentProfile = LoadFile<ProfileData>(path);
    }

    public static ProfileIndex GetProfileIndex()
    {
        if (!File.Exists(s_indexPath))
        {
            return new ProfileIndex();
        }

        return LoadFile<ProfileIndex>(s_indexPath);
    }

    public static void UpdateProfileIndex()
    {
        var index = new ProfileIndex();
        var profileFiles = Directory.GetFiles(Application.dataPath + "/DatosDeUsuario/IndexPerfiles/", "*.xml");

        foreach (var file in profileFiles)
        {
            var filename = Path.GetFileName(file);
            if (filename != "_ProfileIndex_.xml")
            {
                index.profileFileNames.Add(filename);
            }
        }

        SaveFile<ProfileIndex>(s_indexPath, index);
        Debug.Log("Index actualizado con los archivos presentes en el directorio.");
    }

    static void SaveFile<T>(string path, T data)
    {
        using (var profileWriter = new StreamWriter(path))
        {
            var profileSerializer = new XmlSerializer(typeof(T));
            profileSerializer.Serialize(profileWriter, data);
        }
    }

    static T LoadFile<T>(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning($"El archivo {path} no existe.");
            return default(T);
        }

        try
        {
            using (var profileReader = new StreamReader(path))
            {
                var serializer = new XmlSerializer(typeof(T));
                var obj = (T)serializer.Deserialize(profileReader);
                return obj;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error al cargar el archivo {path}: {ex.Message}");
            return default(T);
        }
    }

    public static void DeleteProfile(string filename)
    {
        var path = Application.dataPath + "/DatosDeUsuario/IndexPerfiles/" + filename;
        File.Delete(path);
        Debug.Log("Borrando: " + filename);

        var index = LoadFile<ProfileIndex>(s_indexPath);
        index.profileFileNames.Remove(filename);

        SaveFile<ProfileIndex>(s_indexPath, index);
    }
}
