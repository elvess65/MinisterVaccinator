using System;
using UnityEngine;

namespace FrameworkPackages.MinMax
{
    [Serializable]
    public struct MinMax
    {
        [SerializeField]
        private float min;

        [SerializeField]
        private float max;

        public float Min
        {
            get
            {
                return this.min;
            }
            set
            {
                this.min = value;
            }
        }

        public float Max
        {
            get
            {
                return this.max;
            }
            set
            {
                this.max = value;
            }
        }

        public float RandomValue
        {
            get
            {
                return UnityEngine.Random.Range(this.min, this.max);
            }
        }

        public MinMax(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public bool IsInRange(float val)
        {
            return val >= min && val <= max;
        }

        public float Clamp(float value)
        {
            return Mathf.Clamp(value, this.min, this.max);
        }

        public override string ToString() => $"{min}/{max}";
    }
}