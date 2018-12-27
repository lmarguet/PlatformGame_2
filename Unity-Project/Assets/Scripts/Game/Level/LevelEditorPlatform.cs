using Game.Boost;
using UnityEngine;

namespace Game.Level
{
    public class LevelEditorPlatform : MonoBehaviour
    {
        [SerializeField] private BoostType _boostType;
        [SerializeField] private int _id;

        public BoostType BoostType
        {
            get { return _boostType; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}