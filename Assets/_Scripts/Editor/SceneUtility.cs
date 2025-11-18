using UnityEditor;

#pragma warning disable 618

namespace Kharazmi.Editor
{
    public class SceneUtility
    {
        
        [MenuItem("Scenes/Play", false, 12)]
        public static async void PlayGame()
        {
            var path = EditorBuildSettings.scenes[0].path;
            EditorApplication.SaveCurrentSceneIfUserWantsTo();
            EditorApplication.OpenScene(path);
            EditorApplication.isPlaying = true;
        }
    }
}