using UnityEngine;

namespace YProjectBase
{
    public class EnumMaskAttribute : PropertyAttribute
    {
        public string enumName;

        public EnumMaskAttribute() { }

        public EnumMaskAttribute(string name)
        {
            enumName = name;
        }
    }

}