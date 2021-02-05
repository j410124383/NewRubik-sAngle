
#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace RubiksAndAngie
{
    [CustomEditor(typeof(BodyControllerBase), true)]
    public class BodyControllerEditor : Editor
    {

        private SerializedObject serObj;
        private BodyControllerBase controllerBase;

        private bool isBaseData = true;
        private bool isButtonData = true;

        GUIStyle baseDataStyle;
        GUIStyle buttonStyle;

        [HideInInspector] [SerializeField] Material defaultM;
        [HideInInspector] [SerializeField] Material normal;
        [HideInInspector] [SerializeField] Material flow;
        [HideInInspector] [SerializeField] Material reset;
        [HideInInspector] [SerializeField] Material none;

        public void OnEnable()
        {
            serObj = new SerializedObject(target);
            defaultM = AssetDatabase.LoadAssetAtPath<Material>("Assets/LeanClass/AP1/LV5/Material/Unlit_Blinn_PhongShader.mat");
            normal = AssetDatabase.LoadAssetAtPath<Material>("Assets/Res/Materials/A_normal.mat");
            flow = AssetDatabase.LoadAssetAtPath<Material>("Assets/Res/Materials/A_flow.mat");
            reset = AssetDatabase.LoadAssetAtPath<Material>("Assets/Res/Materials/A_Reset.mat");
            none = AssetDatabase.LoadAssetAtPath<Material>("Assets/Res/Materials/A_Shader Forge_None.mat");
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

            UpdateData();

            EditorGUILayout.EndVertical();

            //当Inspector 面板发生变化时保存数据
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }


        /// <summary>
        /// 添加按钮数据
        /// </summary>
        private void UpdateData()
        {
            serObj.Update();

            if(controllerBase == null)
                controllerBase = (BodyControllerBase)serObj.targetObject;

            if (!controllerBase)
                return;

            EditorGUILayout.BeginVertical();

            GUILayout.Space(10);

            
            isButtonData = EditorGUILayout.Foldout(isButtonData, "控制按钮", true, baseDataStyle);
            if (isButtonData)
            {

                //GUI.backgroundColor = new Color(0.8f, 0.1f, 0.1f, 0.8f);
                GUI.backgroundColor = new Color(0.1f, 0.5f, 0.8f, 0.8f);
                if (GUILayout.Button("创建本体和影子") && controllerBase)
                {
                    //Undo.RecordObject(target, "Create Body And Shadow");
                    controllerBase.InitBodyAndShadow();
                }

                //GUI.backgroundColor = new Color(0.1f, 0.8f, 0.1f, 0.8f);

                if (GUILayout.Button("创建本体") && controllerBase)
                {
                    //Undo.RegisterFullObjectHierarchyUndo(target, "Create Body");
                    controllerBase.InitAixObjs();
                }

                //GUI.backgroundColor = new Color(0.1f, 0.5f, 0.8f, 0.8f);

                if (GUILayout.Button("创建影子") && controllerBase)
                {
                    controllerBase.InitAixShadowObj();
                    //Undo.RecordObject(target, "Create Shadow");
                }

                GUI.backgroundColor = new Color(0.8f, 0.1f, 0.1f, 0.8f);
                if (GUILayout.Button("清除全部本体和影子") && controllerBase)
                {
                    controllerBase.ClearAllAixObj();
                  //  Undo.RecordObject(target, "Delete Body And Shadow");
                }
                
                if (GUILayout.Button("清除全部影子") && controllerBase)
                {
                    controllerBase.ClearAllAixShadowObj();
                 //   Undo.RecordObject(target, "Delete All Shadow");
                }

                if (normal == null || flow == null || reset == null || none == null || defaultM == null)
                {
                    normal = AssetDatabase.LoadAssetAtPath<Material>("Assets/Res/Materials/A_normal.mat");
                    flow = AssetDatabase.LoadAssetAtPath<Material>("Assets/Res/Materials/A_flow.mat");
                    reset = AssetDatabase.LoadAssetAtPath<Material>("Assets/Res/Materials/A_Reset.mat");
                    none = AssetDatabase.LoadAssetAtPath<Material>("Assets/Res/Materials/A_Shader Forge_None.mat");
                    defaultM = AssetDatabase.LoadAssetAtPath<Material>("Assets/LeanClass/AP1/LV5/Material/Unlit_Blinn_PhongShader.mat");
                }

                if (GUILayout.Button("更新材质") && controllerBase)
                {
                    if (normal == null || flow == null || reset == null || none == null || defaultM == null) return;
                    UpdatedMaterial(controllerBase);
                }

            }

            EditorGUILayout.EndVertical();

        }


        public void UpdatedMaterial(BodyControllerBase _controllerBase)
        {
            MeshRenderer renderer;
            for (int i = 0; i < _controllerBase.AixObj.Count; i++)
            {
                if (_controllerBase.AixObj[i] == null || _controllerBase.AixObj[i].ModelObj == null) continue;
              
                if (!_controllerBase.AixObj[i].ModelObj.TryGetComponent<MeshRenderer>(out renderer)) continue;

                if (_controllerBase.AixObj[i].ModelObj.CompareTag("Normal"))
                    renderer.material = normal;
                else if (_controllerBase.AixObj[i].ModelObj.CompareTag("Flow"))
                    renderer.material = flow;
                else if (_controllerBase.AixObj[i].ModelObj.CompareTag("Reset"))
                    renderer.material = reset;
                else
                    renderer.material = defaultM;
                
            }

            SkinnedMeshRenderer skrenderer;
            for (int i = 0; i < _controllerBase.ShadowAixObj.Count; i++)
            {
                if (_controllerBase.ShadowAixObj[i] == null || _controllerBase.ShadowAixObj[i].ModelObj == null) continue;

                if (!_controllerBase.ShadowAixObj[i].ModelObj.TryGetComponent<SkinnedMeshRenderer>(out skrenderer)) continue;

                skrenderer.material = none;
            }

        }

    }
}

#endif
