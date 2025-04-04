using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class WebGLBuilder
{
   [MenuItem("Build/Build WebGL")]
   public static void Build()
   {
       string[] scenes =
       {
           "Assets/Scenes/Game.unity", 
       };

       BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
       buildPlayerOptions.scenes = scenes;
       buildPlayerOptions.locationPathName = "webgl_build";
       buildPlayerOptions.target = BuildTarget.WebGL;
       buildPlayerOptions.options = BuildOptions.None;
       
       var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
       
       BuildSummary summary = report.summary;

       if (summary.result == BuildResult.Succeeded)
       {
           Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
       }

       if (summary.result == BuildResult.Failed)
       {
           Debug.Log("Build failed");
       }
   }
}