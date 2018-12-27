using UnityEngine;

namespace Game.Level
{
    public class PlatformView : MonoBehaviour
    {
        [SerializeField] private GameObject _platform;
        [SerializeField] private MeshRenderer _renderer;

        public int Index { get; set; }

        public MeshRenderer Renderer
        {
            get { return _renderer; }
        }

        public void SetSize(float size)
        {
            var scale = _platform.transform.localScale;
            scale.x = size;
            scale.z = size;
            _platform.transform.localScale = scale;
        }
    }
}