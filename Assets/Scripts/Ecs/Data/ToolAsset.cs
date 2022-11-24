using UnityEngine;

namespace Ecs.Data
{
    [CreateAssetMenu(fileName = "New Tool Asset", menuName = "Game/Tools/New Tool", order = 0)]
    public class ToolAsset : ScriptableObject
    {
        [SerializeField]
        private GameObject _characterView;

        [SerializeField]
        private float _hitDelay;

        [SerializeField]
        private float _reloadTime;

        public GameObject CharacterView => _characterView;

        public float HitDelay => _hitDelay;

        public float ReloadTime => _reloadTime;
    }
}