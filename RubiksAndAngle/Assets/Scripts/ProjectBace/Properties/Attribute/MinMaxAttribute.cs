using UnityEngine;

namespace YProjectBase
{
    /// <summary>
    /// MinMax 特性
    /// 只支持 Vector2
    /// </summary>
    public class MinMaxAttribute : PropertyAttribute
    {
        public float minValue = 0f;
        public float maxValue = 1f;

        public MinMaxAttribute(float min, float max)
        {
            this.minValue = min;
            this.maxValue = max;
        }
    }

}