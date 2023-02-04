using UnityEngine;
using UnityEngine.Rendering;

namespace FarmGame
{
    public class TimeManager : MonoBehaviour
    {
        const float SECONDS_IN_DAY = 86400f;
        const float SECONDS_IN_HOUR = 3600f;

        [SerializeField] Volume nightGlobalVolume;
        [SerializeField] float timeScale = 60f;
        [SerializeField] AnimationCurve nightTimeCurve;

        private float time;
        private float hours { 
            get { return time / SECONDS_IN_HOUR; }
        }

        private void Update()
        {
            time += Time.deltaTime * timeScale;
            time %= SECONDS_IN_DAY;
            nightGlobalVolume.weight = nightTimeCurve.Evaluate(hours);
            Debug.Log(hours);
        }
    }
}
