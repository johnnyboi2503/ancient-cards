using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneTools
{
    public static GameObject FindInScene(string sceneName, string objectName)
    {
        Scene targetScene = SceneManager.GetSceneByName(sceneName);

        if (!targetScene.isLoaded)
        {
            Debug.LogWarning($"Scene {sceneName} is not loaded.");
            return null;
        }

        foreach (GameObject root in targetScene.GetRootGameObjects())
        {
            if (root.name == objectName)
                return root;

            // Search children too
            Transform child = root.transform.Find(objectName);
            if (child != null)
                return child.gameObject;
        }

        return null;
    }
}
