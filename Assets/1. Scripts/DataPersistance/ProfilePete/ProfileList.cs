using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileList : MonoBehaviour
{
    public Transform profilesHolder;
    public GameObject profileUIBoxPrefab;

    void Start()
    {
        var index = ProfileStorage.GetProfileIndex();

        foreach (var profileName in index.profileFileNames)
        {
            var go = Instantiate(this.profileUIBoxPrefab);
            var uibox = go.GetComponent<ProfileBoxUI>();

            uibox.nameLabel.text = profileName;

            // Clic Load
            uibox.loadBtn.onClick.AddListener(() =>
            {
                Debug.Log("Load!");

                SceneManager.LoadScene("EscenaGYM");
            });

            // Clic Delete

            uibox.deleteBtn.onClick.AddListener(() => 
            {
                Debug.Log("Destroy!");
                ProfileStorage.DeleteProfile(profileName);
                Destroy(go);
            });

            go.transform.SetParent(this.profilesHolder, false);
        }
    }
}
