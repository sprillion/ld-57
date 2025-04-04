using UnityEditor;

public class WebGLBuilder {
    [MenuItem("Build/WebGL")]
    public static void Build() {
        BuildPipeline.BuildPlayer(
            levels: new[] { "Assets/Scenes/Game.unity" }, // Укажите ваши сцены
            locationPathName: "webgl_build",
            target: BuildTarget.WebGL,
            options: BuildOptions.None
        );
    }
}