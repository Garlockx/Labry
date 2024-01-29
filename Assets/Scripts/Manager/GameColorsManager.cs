using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameColorsManager : MonoBehaviour
{
    /*
     POSSIBLE CELLS COLORS     
     */
    Dictionary<(string, string), Color> gameColors = new Dictionary<(string, string), Color>() {
            {("W0", "FirstColor"), new Color(0.47f, 0.84f, 0.06f) },
            {("W0", "SecondColor"), new Color(0.96f, 0.64f, 0.10f) },
            {("W0", "ThirdColor"),  new Color(0.83f, 0.15f, 0.15f) },
            {("W1", "FirstColor"), new Color(0.47f, 0.84f, 0.06f) },
            {("W1", "SecondColor"), new Color(0.96f, 0.64f, 0.10f) },
            {("W1", "ThirdColor"),  new Color(0.83f, 0.15f, 0.15f) }
    };


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    //Get color of the world by passing it's name 
    public Color getColor(string colorName)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string worldName;
        if (sceneName.Contains("L"))
        {
            worldName = sceneName.Substring(0, sceneName.IndexOf("L"));
        } else
        {
            worldName = sceneName;
        }
        return gameColors[(worldName, colorName)];
    }


    // TODO : Player can change game color in settings
    public void setGameColors()
    {

    }
}
