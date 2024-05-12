using UnityEngine;

// Generic singleton class
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            // If the instance is null, find it in the scene
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                // If it's still null, create a new GameObject and add the component
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    instance = singletonObject.AddComponent<T>();
                }
            }

            return instance;
        }
    }
}