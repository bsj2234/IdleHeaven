//using UnityEngine;

//public class ColorMaterialManager : MonoSingleton<ColorMaterialManager>
//{
//    [SerializeField] Material GrayMaterial;
//    [SerializeField] Material GreenMaterial;
//    [SerializeField] Material BlueMaterial;
//    [SerializeField] Material MagentaMaterial;
//    [SerializeField] Material YellowMaterial;

//    public Material GetColorMaterial(Color color)
//    {
//        if (color == null) return null;

//        if (color == Color.gray)
//        {
//            return GrayMaterial;
//        }
//        if (color == Color.green)
//        {
//            return GreenMaterial;
//        }
//        if (color == Color.blue)
//        {
//            return BlueMaterial;
//        }
//        if(color == Color.magenta)
//        {
//            return MagentaMaterial;
//        }
//        if(color == Color.yellow)
//        {
//            return YellowMaterial;
//        }
//        return null;
//    }
//}
