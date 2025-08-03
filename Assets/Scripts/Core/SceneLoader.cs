// Core/SceneLoader.cs
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

