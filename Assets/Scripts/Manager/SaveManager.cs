using UnityEngine;

public class SaveManager : MonoBehaviour
{
   public void setFirstPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("World"))
        {
            PlayerPrefs.SetInt("World", 1);
        }
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        if (!PlayerPrefs.HasKey("World1"))
        {
            PlayerPrefs.SetString("World1", "");
        }
    }

    public void newLevelUnlock(int currentWorld, int currentLevel)
    {
        if (PlayerPrefs.GetInt("World") == currentWorld && PlayerPrefs.GetInt("Level") == currentLevel + 1)
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        }
    }

    public void worldFinished()
    {
        PlayerPrefs.SetInt("World", PlayerPrefs.GetInt("World") + 1);
        PlayerPrefs.SetInt("Level", 1);
    }

    public void levelCompleted(int worldId, int levelId)
    {
        string levels = PlayerPrefs.GetString("W" + worldId);
        PlayerPrefs.SetString("W" + worldId, levels + levelId + ";");
    }

    public string getLevelsCompleted(string worldName)
    {
        return PlayerPrefs.GetString(worldName);
    }

    //return 0 when all Level have to be display
    public int getUnlockLevelNumberInWorld(int worldId)
    {
        if (worldId != PlayerPrefs.GetInt("World"))
        {
            return 0;
        } else
        {
            return PlayerPrefs.GetInt("Level");
        }
        
    }

    public int getUnlockWorld()
    {
        return PlayerPrefs.GetInt("World");
    }
}


