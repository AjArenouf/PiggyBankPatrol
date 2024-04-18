#if AR_FOUNDATION_PRESENT
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Internal;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets
{
    public class ARInteractorSpawnTrigger : MonoBehaviour
    {
        public enum SpawnTriggerType
        {
            SelectAttempt,
            InputAction,
        }

        [SerializeField]
        [RequireInterface(typeof(IARInteractor))]
        [Tooltip("The AR Interactor that determines where to spawn the object.")]
        Object m_ARInteractorObject;

        public Object arInteractorObject
        {
            get => m_ARInteractorObject;
            set => m_ARInteractorObject = value;
        }

        [SerializeField]
        [Tooltip("The behavior to use to spawn objects.")]
        ObjectSpawner m_ObjectSpawner;

        public ObjectSpawner objectSpawner
        {
            get => m_ObjectSpawner;
            set => m_ObjectSpawner = value;
        }

        [SerializeField]
        [Tooltip("Whether to require that the AR Interactor hits an AR Plane with a horizontal up alignment in order to spawn anything.")]
        bool m_RequireHorizontalUpSurface;

        public bool requireHorizontalUpSurface
        {
            get => m_RequireHorizontalUpSurface;
            set => m_RequireHorizontalUpSurface = value;
        }

        [SerializeField]
        SpawnTriggerType m_SpawnTriggerType;

        public SpawnTriggerType spawnTriggerType
        {
            get => m_SpawnTriggerType;
            set => m_SpawnTriggerType = value;
        }

        [SerializeField]
        InputActionProperty m_SpawnAction = new(new InputAction(type: InputActionType.Button));

        public InputActionProperty spawnAction
        {
            get => m_SpawnAction;
            set
            {
                if (Application.isPlaying)
                    m_SpawnAction.DisableDirectAction();

                m_SpawnAction = value;

                if (Application.isPlaying && isActiveAndEnabled)
                    m_SpawnAction.EnableDirectAction();
            }
        }

        IARInteractor m_ARInteractor;
        XRBaseControllerInteractor m_ARInteractorAsControllerInteractor;
        bool m_EverHadSelection;
        bool m_ObjectSpawned = false;

        void Start()
        {
            if (m_ObjectSpawner == null)
                m_ObjectSpawner = FindObjectOfType<ObjectSpawner>();

            m_ARInteractor = m_ARInteractorObject as IARInteractor;
            m_ARInteractorAsControllerInteractor = m_ARInteractorObject as XRBaseControllerInteractor;
            if (m_SpawnTriggerType == SpawnTriggerType.SelectAttempt && m_ARInteractorAsControllerInteractor == null)
            {
                Debug.LogError("Can only use SelectAttempt spawn trigger type with XRBaseControllerInteractor.", this);
                enabled = false;
            }
        }

        void OnEnable()
        {
            m_SpawnAction.EnableDirectAction();
        }

        void OnDisable()
        {
            m_SpawnAction.DisableDirectAction();
        }

        void Update()
        {
            if (m_ObjectSpawned)
                return;

            var attemptSpawn = false;
            switch (m_SpawnTriggerType)
            {
                case SpawnTriggerType.SelectAttempt:
                    var currentControllerState = m_ARInteractorAsControllerInteractor.xrController.currentControllerState;
                    if (currentControllerState.selectInteractionState.activatedThisFrame)
                        m_EverHadSelection = m_ARInteractorAsControllerInteractor.hasSelection;
                    else if (currentControllerState.selectInteractionState.active)
                        m_EverHadSelection |= m_ARInteractorAsControllerInteractor.hasSelection;
                    else if (currentControllerState.selectInteractionState.deactivatedThisFrame)
                        attemptSpawn = !m_ARInteractorAsControllerInteractor.hasSelection && !m_EverHadSelection;
                    break;

                case SpawnTriggerType.InputAction:
                    if (m_SpawnAction.action.WasPerformedThisFrame())
                        attemptSpawn = true;
                    break;
            }

            if (attemptSpawn && m_ARInteractor.TryGetCurrentARRaycastHit(out var arRaycastHit))
            {
                var arPlane = arRaycastHit.trackable as ARPlane;
                if (arPlane == null)
                    return;

                if (m_RequireHorizontalUpSurface && arPlane.alignment != PlaneAlignment.HorizontalUp)
                    return;

                m_ObjectSpawner.TrySpawnObject(arRaycastHit.pose.position, arPlane.normal);
                m_ObjectSpawned = true;
            }
        }
    }
}
#endif