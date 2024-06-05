using UnityEngine;

namespace Systems.Persistance
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize<T>(T obj)
        {
            return JsonUtility.ToJson(obj, prettyPrint:true);
        }

        public T Deserialize<T>(string json) {

            return JsonUtility.FromJson<T>(json);
        }
    }
}
