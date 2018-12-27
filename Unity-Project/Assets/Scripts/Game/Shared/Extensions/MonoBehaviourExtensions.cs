using UnityEngine;

namespace Game.Shared.Extensions
{
    public static class MonoBehaviourExtensions
    {

        public static T SetPosition<T>(this T view, Vector3 position) where T:MonoBehaviour
        {
            view.transform.position = position;
            return view;
        }
        
        public static T SetPositionX<T>(this T view, float x) where T:MonoBehaviour
        {
            var position = view.transform.position;
            position.x = x;
            view.transform.position = position;
            return view;
        }
        
        public static T SetPositionY<T>(this T view, float y) where T:MonoBehaviour
        {
            var position = view.transform.position;
            position.y = y;
            view.transform.position = position;
            return view;
        }
        
        public static T SetPositionZ<T>(this T view, float z) where T:MonoBehaviour
        {
            var position = view.transform.position;
            position.z = z;
            view.transform.position = position;
            return view;
        }
    }
}