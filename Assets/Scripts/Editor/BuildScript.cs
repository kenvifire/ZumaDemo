using UnityEditor;

namespace Build
{

    public class BuildScript
    {
        static void PerformBuild()
        {
            string[] defaultScene = {"Assets/Scenes/SampleScene.unity"};
            BuildPipeline.BuildPlayer(defaultScene, "./build/",
                BuildTarget.StandaloneOSX, BuildOptions.None);
        }

    }
}