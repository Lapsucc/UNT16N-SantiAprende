using UnityEngine;

public class CheckFoldersMainMenu : MonoBehaviour
{
    public CheckFolders checkFolders;

    private void OnEnable()
    {
        checkFolders.CheckForFolders();
    }
}
