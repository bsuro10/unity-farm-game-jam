using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmGame
{
    public class EssentialObjectsLoader : MonoBehaviour
    {

        [SerializeField] GameObject essentialObjectsPrefab;

        private void Awake()
        {
            EssentialObjects existingEssentialObjects = FindObjectOfType<EssentialObjects>();
            if (existingEssentialObjects == null)
                Instantiate(essentialObjectsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }

    }
}
