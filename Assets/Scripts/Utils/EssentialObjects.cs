using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmGame
{
    public class EssentialObjects : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
