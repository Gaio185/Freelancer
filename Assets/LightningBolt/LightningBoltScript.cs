using UnityEngine;
using System.Collections.Generic;

namespace DigitalRuby.LightningBolt
{
    /// <summary>
    /// Types of animations for lightning bolts
    /// </summary>
    public enum LightningBoltAnimationMode
    {
        /// <summary>
        /// No animation
        /// </summary>
        None,

        /// <summary>
        /// Pick a random frame
        /// </summary>
        Random,

        /// <summary>
        /// Loop through each frame and restart at the beginning
        /// </summary>
        Loop,

        /// <summary>
        /// Loop through each frame then go backwards to the beginning then forward, etc.
        /// </summary>
        PingPong
    }

    /// <summary>
    /// Allows creation of simple lightning bolts
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class LightningBoltScript : MonoBehaviour
    {
        [Tooltip("The game object where the lightning will emit from. If null, StartPosition is used.")]
        public GameObject StartObject;

        [Tooltip("The start position where the lightning will emit from. This is in world space if StartObject is null, otherwise this is offset from StartObject position.")]
        public Vector3 StartPosition;

        [Tooltip("The game object where the lightning will end at. If null, EndPosition is used.")]
        public GameObject EndObject;

        [Tooltip("The end position where the lightning will end at. This is in world space if EndObject is null, otherwise this is offset from EndObject position.")]
        public Vector3 EndPosition;

        [Range(0.0f, 8.0f)]
        [Tooltip("How many generations? Higher numbers create more line segments. Allows decimal values.")]
        public float Generations = 6.0f;

        [Range(0.01f, 1.0f)]
        [Tooltip("How long each bolt should last before creating a new bolt. In ManualMode, the bolt will simply disappear after this amount of seconds.")]
        public float Duration = 0.05f;

        [Range(0.0f, 1.0f)]
        [Tooltip("How chaotic should the lightning be? (0-1)")]
        public float ChaosFactor = 0.15f;

        [Tooltip("In manual mode, the trigger method must be called to create a bolt.")]
        public bool ManualMode;

        [Range(1, 64)]
        [Tooltip("The number of rows in the texture. Used for animation.")]
        public int Rows = 1;

        [Range(1, 64)]
        [Tooltip("The number of columns in the texture. Used for animation.")]
        public int Columns = 1;

        [Tooltip("The animation mode for the lightning")]
        public LightningBoltAnimationMode AnimationMode = LightningBoltAnimationMode.PingPong;

        [Range(0.01f, 10.0f)]
        [Tooltip("The thickness of the lightning bolt.")]
        public float Thickness = 1.0f; // Thickness of the lightning bolt.

        private LineRenderer lineRenderer;
        private List<KeyValuePair<Vector3, Vector3>> segments = new List<KeyValuePair<Vector3, Vector3>>();
        private int startIndex;
        private Vector2 size;
        private Vector2[] offsets;
        private int animationOffsetIndex;
        private int animationPingPongDirection = 1;
        private bool orthographic;
        private float timer;

        private void Start()
        {
            orthographic = (Camera.main != null && Camera.main.orthographic);
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;

            // Apply initial thickness
            lineRenderer.widthMultiplier = Thickness;

            UpdateFromMaterialChange();
        }

        private void Update()
        {
            orthographic = (Camera.main != null && Camera.main.orthographic);

            // Dynamically update thickness if adjusted in the Inspector
            lineRenderer.widthMultiplier = Thickness;

            if (timer <= 0.0f)
            {
                if (ManualMode)
                {
                    timer = Duration;
                    lineRenderer.positionCount = 0;
                }
                else
                {
                    Trigger();
                }
            }
            timer -= Time.deltaTime;
        }

        public void Trigger()
        {
            Vector3 start, end;

            // Get the start and end positions
            start = StartObject != null ? StartObject.transform.position + StartPosition : StartPosition;
            end = EndObject != null ? EndObject.transform.position + EndPosition : EndPosition;

            timer = Duration + Mathf.Min(0.0f, timer);

            // Generate the lightning
            startIndex = 0;

            // Use Mathf.FloorToInt to handle float Generations properly
            int generationCount = Mathf.FloorToInt(Generations);
            GenerateLightningBolt(start, end, generationCount, generationCount, 0.0f);

            // Update the LineRenderer with the positions
            UpdateLineRenderer(start, end);
        }

        private void GenerateLightningBolt(Vector3 start, Vector3 end, int generation, int totalGenerations, float offsetAmount)
        {
            if (generation < 0 || generation > 8)
            {
                return;
            }
            else if (orthographic)
            {
                start.z = end.z = Mathf.Min(start.z, end.z);
            }

            segments.Add(new KeyValuePair<Vector3, Vector3>(start, end));

            if (generation == 0)
            {
                return;
            }

            Vector3 randomVector;
            if (offsetAmount <= 0.0f)
            {
                offsetAmount = (end - start).magnitude * ChaosFactor;
            }

            while (generation-- > 0)
            {
                int previousStartIndex = startIndex;
                startIndex = segments.Count;

                for (int i = previousStartIndex; i < startIndex; i++)
                {
                    start = segments[i].Key;
                    end = segments[i].Value;

                    Vector3 midPoint = (start + end) * 0.5f;
                    RandomVector(ref start, ref end, offsetAmount, out randomVector);
                    midPoint += randomVector;

                    segments.Add(new KeyValuePair<Vector3, Vector3>(start, midPoint));
                    segments.Add(new KeyValuePair<Vector3, Vector3>(midPoint, end));
                }
                offsetAmount *= 0.5f;
            }
        }

        private void UpdateLineRenderer(Vector3 start, Vector3 end)
        {
            int segmentCount = (segments.Count - startIndex) + 1;
            lineRenderer.positionCount = segmentCount;

            if (segmentCount < 1)
            {
                return;
            }

            int index = 0;
            lineRenderer.SetPosition(index++, start);

            for (int i = startIndex; i < segments.Count; i++)
            {
                lineRenderer.SetPosition(index++, segments[i].Value);
            }

            segments.Clear();
            SelectOffsetFromAnimationMode();
        }

        private void SelectOffsetFromAnimationMode()
        {
            int index;
            if (AnimationMode == LightningBoltAnimationMode.None)
            {
                lineRenderer.material.mainTextureOffset = offsets[0];
                return;
            }
            else if (AnimationMode == LightningBoltAnimationMode.PingPong)
            {
                index = animationOffsetIndex;
                animationOffsetIndex += animationPingPongDirection;

                if (animationOffsetIndex >= offsets.Length)
                {
                    animationOffsetIndex = offsets.Length - 2;
                    animationPingPongDirection = -1;
                }
                else if (animationOffsetIndex < 0)
                {
                    animationOffsetIndex = 1;
                    animationPingPongDirection = 1;
                }
            }
            else if (AnimationMode == LightningBoltAnimationMode.Loop)
            {
                index = animationOffsetIndex++;
                if (animationOffsetIndex >= offsets.Length)
                {
                    animationOffsetIndex = 0;
                }
            }
            else
            {
                index = Random.Range(0, offsets.Length);
            }

            if (index >= 0 && index < offsets.Length)
            {
                lineRenderer.material.mainTextureOffset = offsets[index];
            }
            else
            {
                lineRenderer.material.mainTextureOffset = offsets[0];
            }
        }

        public void UpdateFromMaterialChange()
        {
            size = new Vector2(1.0f / Columns, 1.0f / Rows);
            lineRenderer.material.mainTextureScale = size;

            offsets = new Vector2[Rows * Columns];
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    offsets[x + (y * Columns)] = new Vector2((float)x / Columns, (float)y / Rows);
                }
            }
        }

        public void RandomVector(ref Vector3 start, ref Vector3 end, float offsetAmount, out Vector3 result)
        {
            if (orthographic)
            {
                Vector3 directionNormalized = (end - start).normalized;
                Vector3 side = new Vector3(-directionNormalized.y, directionNormalized.x, directionNormalized.z);
                float distance = Random.Range(-offsetAmount, offsetAmount);
                result = side * distance;
            }
            else
            {
                Vector3 directionNormalized = (end - start).normalized;
                Vector3 side;
                GetPerpendicularVector(ref directionNormalized, out side);

                float distance = Random.Range(0.1f, offsetAmount);
                float rotationAngle = Random.Range(0.0f, 360.0f);

                result = Quaternion.AngleAxis(rotationAngle, directionNormalized) * side * distance;
            }
        }

        private void GetPerpendicularVector(ref Vector3 directionNormalized, out Vector3 side)
        {
            if (directionNormalized == Vector3.zero)
            {
                side = Vector3.right;
            }
            else
            {
                float x = directionNormalized.x;
                float y = directionNormalized.y;
                float z = directionNormalized.z;
                float px, py, pz;

                float ax = Mathf.Abs(x), ay = Mathf.Abs(y), az = Mathf.Abs(z);
                if (ax >= ay && ay >= az)
                {
                    py = 1.0f;
                    pz = 1.0f;
                    px = -(y * py + z * pz) / x;
                }
                else if (ay >= az)
                {
                    px = 1.0f;
                    pz = 1.0f;
                    py = -(x * px + z * pz) / y;
                }
                else
                {
                    px = 1.0f;
                    py = 1.0f;
                    pz = -(x * px + y * py) / z;
                }
                side = new Vector3(px, py, pz).normalized;
            }
        }
    }
}