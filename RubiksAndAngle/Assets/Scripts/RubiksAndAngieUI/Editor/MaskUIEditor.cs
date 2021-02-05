

#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RubiksAndAngie
{
    [CustomEditor(typeof(MarkPanelUI), false)]
    public class MaskUIEditor : Editor
    {
        private SerializedObject serObj;
        private MarkPanelUI controllerBase;

        private bool isBaseData = true;
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

            if (controllerBase == null)
                controllerBase = (MarkPanelUI)serObj.targetObject;

            if (!controllerBase)
                return;

            EditorGUILayout.BeginVertical();

            GUILayout.Space(10);


            isButtonData = EditorGUILayout.Foldout(isButtonData, "控制按钮", true, baseDataStyle);
            if (isButtonData)
            {

                GUI.backgroundColor = new Color(0.1f, 0.5f, 0.8f, 0.8f);
                if (GUILayout.Button("正放遮罩动画") && controllerBase)
                {
                    controllerBase.TestForwardPlay();
                }


                GUI.backgroundColor = new Color(0.1f, 0.5f, 0.8f, 0.8f);
                if (GUILayout.Button("倒放遮罩动画") && controllerBase)
                {
                    controllerBase.TestBackPlay();
                }

                GUI.backgroundColor = new Color(0.8f, 0.1f, 0.1f, 0.8f);
                if (GUILayout.Button("停止遮罩动画") && controllerBase)
                {
                    controllerBase.TestStop();
                }

            }

            EditorGUILayout.EndVertical();

        }

    }
}

#endif
