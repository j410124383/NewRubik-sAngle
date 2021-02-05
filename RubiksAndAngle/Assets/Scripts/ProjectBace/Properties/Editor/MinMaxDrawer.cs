#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace YProjectBase
{
    /// <summary>
    /// MinMax GUI 绘制
    /// </summary>
    [CustomPropertyDrawer(typeof(MinMaxAttribute))]
    public class MinMaxDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            Assert.IsTrue(property.propertyType == SerializedPropertyType.Vector2, " Only Vetor2 , X is min , Y is max");

            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                //拿到 特性
                MinMaxAttribute minMax = attribute as MinMaxAttribute;

                //获得 总区域数据
                Rect totalValueArea = EditorGUI.PrefixLabel(position, label);

                //获得 左边区域
                Rect leftValueArea = new Rect(totalValueArea.x, totalValueArea.y, 28f, totalValueArea.height);

                //获得 Slider 的区域
                Rect sliderValueArea = new Rect(leftValueArea.xMax + 2, totalValueArea.y, totalValueArea.width - leftValueArea.width * 2 - 8, totalValueArea.height);

                //获得 右边区域
                Rect rightValueArea = new Rect(totalValueArea.xMax - leftValueArea.width - 2, totalValueArea.y, leftValueArea.width, totalValueArea.height);


                //拿到 min  max 值
                float minValue = property.vector2Value.x;
                float maxValue = property.vector2Value.y;

                //绘制 Slider 的区域
                EditorGUI.MinMaxSlider(sliderValueArea, ref minValue, ref maxValue, minMax.minValue, minMax.maxValue);
                property.vector2Value = new Vector2(minValue, maxValue);

                //绘制 左边区域
                EditorGUI.LabelField(leftValueArea, minValue.ToString("F2"));

                //绘制 右边区域
                EditorGUI.LabelField(rightValueArea, maxValue.ToString("F2"));
            }

        }

    }
}

#endif