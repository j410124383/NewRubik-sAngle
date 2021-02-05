
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace MyParticlesController_ED
{

    [CustomEditor(typeof(ParticlesControllerBase), true)]
    public class ParticleControllerEditor_ED : Editor
    {

        private SerializedObject serObj;
        private ParticlesControllerBase controllerBase;
        // public SerializedProperty edgeBlend;
        private bool isBaseData;
        private bool isButtonData = true;

        GUIStyle baseDataStyle;

        GUIStyle buttonStyle;

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

                    GUILayout.Space(5);

                    GUI.backgroundColor = new Color(0.4f, 0.8f, 0.8f, 1f);

                    if (GUILayout.Button("UpdateValue") && controllerBase)
                    {
                        controllerBase.UpdateValue();
                    }

                }
            }

            UpdateData();

            EditorUtility.SetDirty(target);

            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// 添加按钮数据
        /// </summary>
        private void UpdateData()
        {
            serObj.Update();

            controllerBase = (ParticlesControllerBase)serObj.targetObject;

            if (!controllerBase)
                return;

            EditorGUILayout.BeginVertical();

            GUILayout.Space(10);


            isButtonData = EditorGUILayout.Foldout(isButtonData, "控制按钮", true, baseDataStyle);
            if (isButtonData)
            {

                GUI.backgroundColor = new Color(0.8f, 0.1f, 0.1f,0.8f);

                if (GUILayout.Button("None") && controllerBase)
                {
                    controllerBase.None();
                }


                GUI.backgroundColor = new Color(0.1f, 0.8f, 0.1f, 0.8f);

                if (GUILayout.Button("Play") && controllerBase)
                {
                    controllerBase.Play();
                }

                GUI.backgroundColor = new Color(0.1f, 0.5f, 0.8f , 0.8f);

                if (GUILayout.Button("Stop") && controllerBase)
                {
                    controllerBase.Stop();
                }
            }

            EditorGUILayout.EndVertical();

        } 


    }
 }
#endif