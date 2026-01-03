using System;
using System.Reflection;
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
        InputAction attackAction;
        InputAction interactAction;
        InputAction crouchAction;
        InputAction jumpAction;
        InputAction sprintAction;

        Vector2 _input;
        Vector2 _look;
        public Vector2 Input => _input;
        public Vector2 Look => _look;
        
        #endregion

        #region Events
        public event Action OnAttackPerformed;
        public event Action OnAttackCanceled;
        public event Action OnInteractPerformed;
        public event Action OnInteractCanceled;
        public event Action OnCrouchPerformed;
        public event Action OnCrouchCanceled;
        public event Action OnJumpPerformed;
        public event Action OnJumpCanceled;
        public event Action OnSprintPerformed;
        public event Action OnSprintCanceled;
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
            InitializeInputActions();

            attackAction.performed += (ctx) => OnAttackPerformed?.Invoke();
            interactAction.performed += (ctx) => OnInteractPerformed?.Invoke();
            sprintAction.performed += (ctx) => OnSprintPerformed?.Invoke();
            crouchAction.performed += (ctx) => OnCrouchPerformed?.Invoke();
            jumpAction.performed += (ctx) => OnJumpPerformed?.Invoke();

            
            attackAction.canceled += (ctx) => OnAttackCanceled?.Invoke();
            interactAction.canceled += (ctx) => OnInteractCanceled?.Invoke();
            sprintAction.canceled += (ctx) => OnSprintCanceled?.Invoke();
            crouchAction.canceled += (ctx) => OnCrouchCanceled?.Invoke();
            jumpAction.canceled += (ctx) => OnJumpCanceled?.Invoke();
            
        }

        void Start()
        {
            
        }

        void Update()
        {
            _input = moveAction.ReadValue<Vector2>();
            _look = lookAction.ReadValue<Vector2>();
        }


        void FixedUpdate()
        {

        }

        void LateUpdate()
        {
            
        }
        #endregion

        #region Custom Functions
        void InitializeInputActions()
        {
            FieldInfo[] fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach(FieldInfo field in fields)
            {
                if(field.Name.EndsWith("Action") && field.FieldType == typeof(InputAction))
                {
                    string actionName = char.ToUpper(field.Name[0]) + field.Name.Substring(1).Replace("Action","");
                    InputAction foundAction = InputSystem.actions.FindAction(actionName);
                    Debug.Assert(foundAction != null, $"[InputProcessor] Action '{actionName}' not found in Input Asset for field '{field.Name}'");
                    field.SetValue(this, foundAction);
                }
            }
        }

       
     
        #endregion
    }
}