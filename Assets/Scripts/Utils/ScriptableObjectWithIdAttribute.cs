using UnityEngine;

namespace FarmGame
{
    public class ScriptableObjectWithIdAttribute : ScriptableObject
    {
        public string id;

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                AssignNewUID();
            }
        }

        private void AssignNewUID()
        {
            id = System.Guid.NewGuid().ToString();
        }
    }
}
