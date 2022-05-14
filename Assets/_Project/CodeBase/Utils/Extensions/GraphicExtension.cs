using UnityEngine.UI;

namespace _Project.CodeBase.Utils.Extensions
{
    public static class GraphicExtension
    {
        public static T ChangeAlpha<T>(this T graphic, float newAlpha) where T : Graphic
        {
            var color = graphic.color;
            color.a = newAlpha;
            graphic.color = color;
            return graphic;
        }
    }
}