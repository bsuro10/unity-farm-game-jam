using UnityEngine;
using UnityEngine.Rendering;

namespace FarmGame
{
    public class TimeManager : MonoBehaviour
    {
        #region Singleton
        public static TimeManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        const float SECONDS_IN_DAY = 86400f;
        const float SECONDS_IN_HOUR = 3600f;
        const float PHASE_LENGTH_IN_SECONDS = 900f;

        public delegate void OnTimeTick();
        public OnTimeTick onTimeTick;

        [SerializeField] Volume nightGlobalVolume;
        [SerializeField] float timeScale = 60f;
        [SerializeField] AnimationCurve nightTimeCurve;
        [SerializeField] float startAtTime = 28800f;

        private float time;
        private int lastPhase;

        private void Start()
        {
            time = startAtTime;
        }

        private float hours { 
            get { return time / SECONDS_IN_HOUR; }
        }

        private void Update()
        {
            CalculateTime();
            nightGlobalVolume.weight = nightTimeCurve.Evaluate(hours);
            CalculatePhase();
        }

        private void CalculateTime()
        {
            time += Time.deltaTime * timeScale;
            time %= SECONDS_IN_DAY;
        }

        private void CalculatePhase()
        {
            int currentPhase = (int)(time / PHASE_LENGTH_IN_SECONDS);
            if (lastPhase != currentPhase)
            {
                lastPhase = currentPhase;
                if (onTimeTick != null)
                    onTimeTick.Invoke();
            }
        }

    }
}
