
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;

namespace RubiksAndAngie
{
    [CustomEditor(typeof(GameControllerBase), true)]
    public class GameControllerEditor : Editor
    {
        private SerializedObject serObj;
        private GameControllerBase controllerBase;


        private bool isBaseData = true;

        GUIStyle baseDataStyle;

        private bool isGoing;

        public void OnEnable()
        {
            serObj = new SerializedObject(target);
        }


        public override void OnInspectorGUI()
        {

            EditorGUILayout.Space(10);

            if (baseDataStyle == null)
            {
                baseDataStyle = new GUIStyle("Foldout")
                {
                    fontSize = 12,
                    fontStyle = FontStyle.Bold,
                };
            }

            EditorGUILayout.BeginVertical();

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                isBaseData = EditorGUILayout.Foldout(isBaseData, "基本数据", true, baseDataStyle);
                if (isBaseData)
                {
                    base.OnInspectorGUI();
                }
            }


            if (GUILayout.Button("生成数据"))
            {
                if (isGoing) return;
                Go();
            }

            EditorGUILayout.EndVertical();

            //当Inspector 面板发生变化时保存数据
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }


        private void Go()
        {
            isGoing = true;
            if (controllerBase == null)
                controllerBase = target as GameControllerBase;


            //创建数据
            GameData sceneData = ScriptableObject.CreateInstance<GameData>();



            if(sceneData != null)
            {
                sceneData.name = SceneManager.GetActiveScene().name;
                sceneData.opNum = FindObjectsOfType<DiamantController>().Length;
            }
            else
            {
                isGoing = false;
                return;
            }
     
            string SceneDataPath = "Assets/Scripts/RubiksAndAngieBase/GameData";

            string path;

            path = SceneDataPath+"/Data";
                  
            //检查保存路径
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            //删除原有文件，生成新文件
            string fullPath = path + "/" + sceneData.name + "_" + "GameData.asset";
            UnityEditor.AssetDatabase.DeleteAsset(fullPath);
            UnityEditor.AssetDatabase.CreateAsset(sceneData, fullPath);
            UnityEditor.AssetDatabase.Refresh();

            sceneData.DataName = sceneData.name;

            controllerBase.gameData = sceneData;
            //isError = false;
            isGoing = false;
        }



    }
}

#endif