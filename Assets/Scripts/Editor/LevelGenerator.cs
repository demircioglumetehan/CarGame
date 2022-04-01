using CarGame.Scripts.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace CarGame.Scripts.Editor
{
    public class LevelGenerator : EditorWindow
    {
        [MenuItem("CarGame/LevelGenerator")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<LevelGenerator>("Level Generator");
        }
        int level = 1;
        float carvelocity = 5f;
        float carrotationspeed = 2f;
        void OnGUI()
        {
            GUILayout.Label("Level Generator", EditorStyles.boldLabel);
            if (GUILayout.Button("Generate New Level"))
            {
                GenerateNewLevel();
            }

            level = EditorGUILayout.IntField("Level", level);

            if (GUILayout.Button("Open Level"))
            {
                OpenLevel(level);
            }
            GUILayout.BeginHorizontal();
            GUILayout.Label("Car Velocity", EditorStyles.boldLabel);
            carvelocity = EditorGUILayout.Slider(carvelocity, 0, 10);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Car rotation speed", EditorStyles.boldLabel);
            carrotationspeed = EditorGUILayout.Slider(carrotationspeed, 0, 100);
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Save"))
            {
                SaveVariables();
            }

        }
        /// <summary>
        /// Saves car rotation and car speed to currently active scene.
        /// </summary>
        private void SaveVariables()
        {
            var carmovementmanager = FindObjectOfType<CarMovementManager>();
            if (carmovementmanager != null)
            {
                carmovementmanager.SetRotationSpeedAndMovingSpeedFromInspector(carvelocity, carrotationspeed);
                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), EditorSceneManager.GetActiveScene().path);
            }

        }
        private void GenerateNewLevel()
        {
            Scene newScene = GenerateNewScene();
            GenerateLevelPrefab();
            AddSceneToBuildScenes(newScene);
        }

        /// <summary>
        /// Adds the newly generated scene to build index
        /// </summary>
        /// <param name="scene"></param>
        private void AddSceneToBuildScenes(Scene scene)
        {
            var scenePath = EditorSceneManager.GetActiveScene().path;
            List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
            foreach (var buildscene in EditorBuildSettings.scenes)
            {
                editorBuildSettingsScenes.Add(buildscene);
            }
            if (!string.IsNullOrEmpty(scenePath))
                editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
            EditorSceneManager.SaveScene(scene, scenePath);
        }
        /// <summary>
        /// Generates new scene with level name
        /// </summary>
        /// <returns></returns>
        private Scene GenerateNewScene()
        {
            string[] path = EditorSceneManager.GetActiveScene().path.Split(char.Parse("/"));
            Debug.Log(EditorSceneManager.GetActiveScene().path);
            path[path.Length - 1] = "Level" + (SceneManager.sceneCountInBuildSettings + 1) + ".unity";
            var newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            newScene.name = "Level" + (SceneManager.sceneCountInBuildSettings + 1);
            EditorSceneManager.SaveScene(newScene, string.Join("/", path));
            return newScene;
        }
        /// <summary>
        /// Generates the levelprefab on new scene
        /// </summary>
        private void GenerateLevelPrefab()
        {
            GameObject LevelPrefab = Resources.Load<GameObject>("Prefabs/LevelPrefab");
            GameObject LevelManager = Resources.Load<GameObject>("Prefabs/LevelManager");
            GameObject MenuReferences = Resources.Load<GameObject>("Prefabs/MenuReferences");
            var levelpref=Instantiate(LevelPrefab);
            levelpref.name = "LevelPrefab";
            var levelmanager=Instantiate(LevelManager);
            levelmanager.name = "LevelManager";
            var menuref= Instantiate(MenuReferences);
            menuref.name = "MenuReferences";
        }
        /// <summary>
        /// Opens given level(scene)
        /// </summary>
        /// <param name="level"></param>
        private void OpenLevel(int level)
        {
            var maximumlevel = SceneManager.sceneCountInBuildSettings;
            if (level > maximumlevel || level < 0)
            {
                Debug.LogError("The level does not exist!");
                return;
            }

            string[] path = EditorSceneManager.GetActiveScene().path.Split(char.Parse("/"));
            path[path.Length - 1] = "Level" + level + ".unity";
            EditorSceneManager.OpenScene(string.Join("/", path), OpenSceneMode.Single);

        }
    }

}
