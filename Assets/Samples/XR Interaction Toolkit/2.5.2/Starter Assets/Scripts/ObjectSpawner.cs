using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using System.Collections.Generic;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        Camera m_CameraToFace;
        public GameObject Collar;
        public Button collar2;
        public Button animationtest;
        public GameObject visibleButton;
        public Button bone2;

        private Transform m_CurrentSpawnedObject;
        private Animator m_ObjectAnimator;

        [SerializeField]
        List<GameObject> m_ObjectPrefabs = new List<GameObject>();

        public List<GameObject> objectPrefabs
        {
            get => m_ObjectPrefabs;
            set => m_ObjectPrefabs = value;
        }

        [SerializeField]
        GameObject m_SpawnVisualizationPrefab;

        public GameObject spawnVisualizationPrefab
        {
            get => m_SpawnVisualizationPrefab;
            set => m_SpawnVisualizationPrefab = value;
        }

        [SerializeField]
        int m_SpawnOptionIndex = -1;

        public int spawnOptionIndex
        {
            get => m_SpawnOptionIndex;
            set => m_SpawnOptionIndex = value;
        }

        public bool isSpawnOptionRandomized => m_SpawnOptionIndex < 0 || m_SpawnOptionIndex >= m_ObjectPrefabs.Count;

        [SerializeField]
        bool m_OnlySpawnInView = true;

        public bool onlySpawnInView
        {
            get => m_OnlySpawnInView;
            set => m_OnlySpawnInView = value;
        }

        [SerializeField]
        float m_ViewportPeriphery = 0.15f;

        public float viewportPeriphery
        {
            get => m_ViewportPeriphery;
            set => m_ViewportPeriphery = value;
        }

        [SerializeField]
        bool m_ApplyRandomAngleAtSpawn = true;

        public bool applyRandomAngleAtSpawn
        {
            get => m_ApplyRandomAngleAtSpawn;
            set => m_ApplyRandomAngleAtSpawn = value;
        }

        [SerializeField]
        float m_SpawnAngleRange = 45f;

        public float spawnAngleRange
        {
            get => m_SpawnAngleRange;
            set => m_SpawnAngleRange = value;
        }

        [SerializeField]
        bool m_SpawnAsChildren;

        public bool spawnAsChildren
        {
            get => m_SpawnAsChildren;
            set => m_SpawnAsChildren = value;
        }

        public event Action<GameObject> objectSpawned;

        bool m_ObjectSpawned = false;

        void Awake()
        {
            EnsureFacingCamera();
            collar2.onClick.AddListener(ToggleTorusVisibility);
            animationtest.onClick.AddListener(TriggerAnimation);
            bone2.onClick.AddListener(PlayLoopingAnimation);
            visibleButton.SetActive(false);
        }

        void EnsureFacingCamera()
        {
            if (m_CameraToFace == null)
                m_CameraToFace = Camera.main;
        }

        public void RandomizeSpawnOption()
        {
            m_SpawnOptionIndex = -1;
        }

        public bool TrySpawnObject(Vector3 spawnPoint, Vector3 spawnNormal)
        {
            if (m_ObjectSpawned)
            {
                Debug.Log("Object has spawned already");
            }

            if (m_OnlySpawnInView)
            {
                var inViewMin = m_ViewportPeriphery;
                var inViewMax = 1f - m_ViewportPeriphery;
                var pointInViewportSpace = m_CameraToFace.WorldToViewportPoint(spawnPoint);
                if (pointInViewportSpace.z < 0f || pointInViewportSpace.x > inViewMax || pointInViewportSpace.x < inViewMin ||
                    pointInViewportSpace.y > inViewMax || pointInViewportSpace.y < inViewMin)
                {
                    return false;
                }
            }

            Transform childTransform = null;

            var objectIndex = isSpawnOptionRandomized ? UnityEngine.Random.Range(0, m_ObjectPrefabs.Count) : m_SpawnOptionIndex;
            var newObject = Instantiate(m_ObjectPrefabs[objectIndex]);

            if (newObject != null)
            {
                childTransform = newObject.transform.Find("Torus");

                if (childTransform != null)
                {
                    childTransform.gameObject.SetActive(false);
                }
            }

            if (m_SpawnAsChildren)
                newObject.transform.parent = transform;

            newObject.transform.position = spawnPoint;
            EnsureFacingCamera();

            var facePosition = m_CameraToFace.transform.position;
            var forward = facePosition - spawnPoint;
            BurstMathUtility.ProjectOnPlane(forward, spawnNormal, out var projectedForward);
            newObject.transform.rotation = Quaternion.LookRotation(projectedForward, spawnNormal);

            if (m_SpawnVisualizationPrefab != null)
            {
                var visualizationTrans = Instantiate(m_SpawnVisualizationPrefab).transform;
                visualizationTrans.position = spawnPoint;
                visualizationTrans.rotation = newObject.transform.rotation;
            }

            m_ObjectSpawned = true;
            m_CurrentSpawnedObject = newObject.transform;

            objectSpawned?.Invoke(newObject);

            visibleButton.SetActive(true);

            return true;
        }

        public void ToggleTorusVisibility()
        {
            if (m_CurrentSpawnedObject != null)
            {
                Transform childTransform = m_CurrentSpawnedObject.Find("Torus");
                if (childTransform != null)
                {
                    childTransform.gameObject.SetActive(!childTransform.gameObject.activeSelf);
                }
            }
        }

        public void TriggerAnimation()
        {
            if (m_CurrentSpawnedObject != null)
            {
                m_ObjectAnimator = m_CurrentSpawnedObject.GetComponent<Animator>();

                if (m_ObjectAnimator != null)
                {
                    m_ObjectAnimator.SetTrigger("ButtonPressed");
                }
                else
                {
                    Debug.LogWarning("Animator component not found on the instantiated object.");
                }
            }
            else
            {
                Debug.LogWarning("No object instantiated to trigger animation.");
            }
        }

        public void PlayLoopingAnimation()
        {
            if (m_CurrentSpawnedObject != null)
            {
                m_ObjectAnimator = m_CurrentSpawnedObject.GetComponent<Animator>();

                if (m_ObjectAnimator != null)
                {
                    // Assuming you have another trigger called "TreatGiven" in your animation controller
                    m_ObjectAnimator.SetTrigger("TreatGiven");
                }
                else
                {
                    Debug.LogWarning("Animator component not found on the instantiated object.");
                }
            }
            else
            {
                Debug.LogWarning("No object instantiated to trigger animation.");
            }
        }

        public void ShowCollar()
        {
            if (Collar != null)
            {
                Collar.SetActive(true);
            }
        }
    }
}
