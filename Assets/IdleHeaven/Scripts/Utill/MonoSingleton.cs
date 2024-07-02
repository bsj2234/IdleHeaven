using UnityEngine;
using UnityEngine.Assertions;

//싱글턴
//하나의 인스턴스만 존재하는 클래스
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance { get; set; }

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                Assert.IsNotNull(instance, "There is no instancable object");
            }
            Assert.IsNotNull(instance, "There is no instancable object");
            return instance;
        }
    }
}