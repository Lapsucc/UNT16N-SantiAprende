using SingletonExample;
using System;
using Systems.Persistance;
using UnityEngine.SceneManagement;

namespace Systems.Persistence
{
    [Serializable] public class GameData
    {
        public string Name;
        public string currentLevelName;

    }

    public class SaveLoadSystem : PersistentSingleton<SaveLoadSystem> {
        
        public GameData gameData;

        IDataService dataService;

        protected override void Awake()
        {
            base.Awake();
            dataService = new FileDataService(new JsonSerializer());
        }

        public void NewGame()
        {
            gameData = new GameData
            {
                Name = "New Game",
                currentLevelName = "EscenaGYM"
            };
            SceneManager.LoadScene(gameData.currentLevelName);
        }

        public void SaveGame() => dataService.Save(gameData);
        

        public void LoadGame(string gameName)
        { 
            gameData = dataService.Load(gameName);

            if (String.IsNullOrWhiteSpace(gameData.currentLevelName))
            {
                gameData.currentLevelName = "EscenaGYM";
            }

            SceneManager.LoadScene(gameData.currentLevelName);
        }

        public void ReloadGame() => LoadGame(gameData.Name);

        public void DeleteGame(string gameName)
        { 
            dataService.Delete(gameName);        
        }
    }   
}