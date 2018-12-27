using UnityEngine;

namespace Game.Boost
{
    public class BoostScript:MonoBehaviour
    {
        [SerializeField] private BoostType _boostType;

        public BoostType BoostType
        {
            get { return _boostType; }
            set { _boostType = value; }
        }
    }
}