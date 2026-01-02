using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Popieyes.Input
{
    public class InputProcessor : MonoBehaviour
    {
        #region Variables
        public InputActionAsset InputActions;
        InputAction moveAction;
        InputAction lookAction;
        InputAction interactAction;
        InputAction sprintAction;

        Vector2 _input;
        Vector2 _look;
        public Vector2 Input => _input;
        public Vector2 Look => _look;
        
        #endregion

        #region Events
        public event Action OnInteract;
        public event Action OnSprintPerformed;
        public event Action OnSprintCancelled;
        #endregion

        #region Unity Callbacks
        void OnEnable()
        {
            InputActions.FindActionMap("Player").Enable();  
        }

        void OnDisable()
        {
            InputActions.FindActionMap("Player").Disable();  
        }
        void Awake()
        {
            moveAction = InputSystem.actions.FindAction("Move");
            lookAction = InputSystem.actions.FindAction("Look");
            interactAction = InputSystem.actions.FindAction("Interact");
            sprintAction = InputSystem.actions.FindAction("Sprint");
            Debug.Assert(interactAction != null, "Interact Action is null in InputProcessor");

            sprintAction.performed += OnSprint;
        }

        void Start()
        {
            
        }

        void Update()
        {
            _input = moveAction.ReadValue<Vector2>();
            _look = lookAction.ReadValue<Vector2>();
            if(interactAction.WasPerformedThisFrame())
            {
                OnInteract?.Invoke();
            }
        }


        void FixedUpdate()
        {

        }

        void LateUpdate()
        {
            
        }
        #endregion

        #region Custom Functions
        void OnSprint(InputAction.CallbackContext context)
        {   
            switch(context.phase)
            {
                case InputActionPhase.Performed:
                    OnSprintPerformed.Invoke();
                    Debug.Log("Sprint is triggered");
                    break;
                case InputActionPhase.Canceled:
                    OnSprintCancelled.Invoke();
                    Debug.Log("Sprint is cancelled");
                    break;
                default:
                break;
            }
            
        }
        #endregion
    }
}