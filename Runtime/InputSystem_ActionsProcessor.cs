using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using System;
using System.Reflection;

namespace Kronos.Input
{
    public class InputSystem_ActionsProcessor : MonoBehaviour
    {
        public InputActionAsset InputActions;

        // --- Map: Player ---
        private InputAction moveAction;
        public event Action OnMovePerformed;
        public event Action OnMoveCanceled;
        private InputAction lookAction;
        public event Action OnLookPerformed;
        public event Action OnLookCanceled;
        private InputAction attackAction;
        public event Action OnAttackPerformed;
        public event Action OnAttackCanceled;
        private InputAction interactAction;
        public event Action OnInteractPerformed;
        public event Action OnInteractCanceled;
        private InputAction crouchAction;
        public event Action OnCrouchPerformed;
        public event Action OnCrouchCanceled;
        private InputAction jumpAction;
        public event Action OnJumpPerformed;
        public event Action OnJumpCanceled;
        private InputAction previousAction;
        public event Action OnPreviousPerformed;
        public event Action OnPreviousCanceled;
        private InputAction nextAction;
        public event Action OnNextPerformed;
        public event Action OnNextCanceled;
        private InputAction sprintAction;
        public event Action OnSprintPerformed;
        public event Action OnSprintCanceled;
        private InputAction aimAction;
        public event Action OnAimPerformed;
        public event Action OnAimCanceled;

        // --- Map: UI ---
        private InputAction navigateAction;
        public event Action OnNavigatePerformed;
        public event Action OnNavigateCanceled;
        private InputAction submitAction;
        public event Action OnSubmitPerformed;
        public event Action OnSubmitCanceled;
        private InputAction cancelAction;
        public event Action OnCancelPerformed;
        public event Action OnCancelCanceled;
        private InputAction pointAction;
        public event Action OnPointPerformed;
        public event Action OnPointCanceled;
        private InputAction clickAction;
        public event Action OnClickPerformed;
        public event Action OnClickCanceled;
        private InputAction rightClickAction;
        public event Action OnRightClickPerformed;
        public event Action OnRightClickCanceled;
        private InputAction middleClickAction;
        public event Action OnMiddleClickPerformed;
        public event Action OnMiddleClickCanceled;
        private InputAction scrollWheelAction;
        public event Action OnScrollWheelPerformed;
        public event Action OnScrollWheelCanceled;
        private InputAction trackedDevicePositionAction;
        public event Action OnTrackedDevicePositionPerformed;
        public event Action OnTrackedDevicePositionCanceled;
        private InputAction trackedDeviceOrientationAction;
        public event Action OnTrackedDeviceOrientationPerformed;
        public event Action OnTrackedDeviceOrientationCanceled;

        Vector2 _input;
        Vector2 _look;
        public Vector2 Input => _input;
        public Vector2 Look => _look;
        void Awake() 
        {
            InitializeInputActions();
            Enable();
        }

        public void Enable()
        {
            InputActions.FindActionMap("Player").Enable();
            InputActions.FindActionMap("UI").Enable();
            BindEvents(true);
        }

        public void Disable()
        {
            BindEvents(false);
        }

        public void Update()
        {
            _input = moveAction.ReadValue<Vector2>();
            _look = lookAction.ReadValue<Vector2>();
        }

        void OnDisable()
        {
            Disable();
        }

        private void BindEvents(bool bind)
        {
            if(bind) {
                moveAction.performed += ctx => OnMovePerformed?.Invoke();
                moveAction.canceled += ctx => OnMoveCanceled?.Invoke();
             }
            else {
                    moveAction.performed -= ctx => OnMovePerformed?.Invoke();
                    moveAction.canceled -= ctx => OnMoveCanceled?.Invoke();
             }
            if(bind) {
                lookAction.performed += ctx => OnLookPerformed?.Invoke();
                lookAction.canceled += ctx => OnLookCanceled?.Invoke();
             }
            else {
                    lookAction.performed -= ctx => OnLookPerformed?.Invoke();
                    lookAction.canceled -= ctx => OnLookCanceled?.Invoke();
             }
            if(bind) {
                attackAction.performed += ctx => OnAttackPerformed?.Invoke();
                attackAction.canceled += ctx => OnAttackCanceled?.Invoke();
             }
            else {
                    attackAction.performed -= ctx => OnAttackPerformed?.Invoke();
                    attackAction.canceled -= ctx => OnAttackCanceled?.Invoke();
             }
            if(bind) {
                interactAction.performed += ctx => OnInteractPerformed?.Invoke();
                interactAction.canceled += ctx => OnInteractCanceled?.Invoke();
             }
            else {
                    interactAction.performed -= ctx => OnInteractPerformed?.Invoke();
                    interactAction.canceled -= ctx => OnInteractCanceled?.Invoke();
             }
            if(bind) {
                crouchAction.performed += ctx => OnCrouchPerformed?.Invoke();
                crouchAction.canceled += ctx => OnCrouchCanceled?.Invoke();
             }
            else {
                    crouchAction.performed -= ctx => OnCrouchPerformed?.Invoke();
                    crouchAction.canceled -= ctx => OnCrouchCanceled?.Invoke();
             }
            if(bind) {
                jumpAction.performed += ctx => OnJumpPerformed?.Invoke();
                jumpAction.canceled += ctx => OnJumpCanceled?.Invoke();
             }
            else {
                    jumpAction.performed -= ctx => OnJumpPerformed?.Invoke();
                    jumpAction.canceled -= ctx => OnJumpCanceled?.Invoke();
             }
            if(bind) {
                previousAction.performed += ctx => OnPreviousPerformed?.Invoke();
                previousAction.canceled += ctx => OnPreviousCanceled?.Invoke();
             }
            else {
                    previousAction.performed -= ctx => OnPreviousPerformed?.Invoke();
                    previousAction.canceled -= ctx => OnPreviousCanceled?.Invoke();
             }
            if(bind) {
                nextAction.performed += ctx => OnNextPerformed?.Invoke();
                nextAction.canceled += ctx => OnNextCanceled?.Invoke();
             }
            else {
                    nextAction.performed -= ctx => OnNextPerformed?.Invoke();
                    nextAction.canceled -= ctx => OnNextCanceled?.Invoke();
             }
            if(bind) {
                sprintAction.performed += ctx => OnSprintPerformed?.Invoke();
                sprintAction.canceled += ctx => OnSprintCanceled?.Invoke();
             }
            else {
                    sprintAction.performed -= ctx => OnSprintPerformed?.Invoke();
                    sprintAction.canceled -= ctx => OnSprintCanceled?.Invoke();
             }
            if(bind) {
                aimAction.performed += ctx => OnAimPerformed?.Invoke();
                aimAction.canceled += ctx => OnAimCanceled?.Invoke();
             }
            else {
                    aimAction.performed -= ctx => OnAimPerformed?.Invoke();
                    aimAction.canceled -= ctx => OnAimCanceled?.Invoke();
             }
            if(bind) {
                navigateAction.performed += ctx => OnNavigatePerformed?.Invoke();
                navigateAction.canceled += ctx => OnNavigateCanceled?.Invoke();
             }
            else {
                    navigateAction.performed -= ctx => OnNavigatePerformed?.Invoke();
                    navigateAction.canceled -= ctx => OnNavigateCanceled?.Invoke();
             }
            if(bind) {
                submitAction.performed += ctx => OnSubmitPerformed?.Invoke();
                submitAction.canceled += ctx => OnSubmitCanceled?.Invoke();
             }
            else {
                    submitAction.performed -= ctx => OnSubmitPerformed?.Invoke();
                    submitAction.canceled -= ctx => OnSubmitCanceled?.Invoke();
             }
            if(bind) {
                cancelAction.performed += ctx => OnCancelPerformed?.Invoke();
                cancelAction.canceled += ctx => OnCancelCanceled?.Invoke();
             }
            else {
                    cancelAction.performed -= ctx => OnCancelPerformed?.Invoke();
                    cancelAction.canceled -= ctx => OnCancelCanceled?.Invoke();
             }
            if(bind) {
                pointAction.performed += ctx => OnPointPerformed?.Invoke();
                pointAction.canceled += ctx => OnPointCanceled?.Invoke();
             }
            else {
                    pointAction.performed -= ctx => OnPointPerformed?.Invoke();
                    pointAction.canceled -= ctx => OnPointCanceled?.Invoke();
             }
            if(bind) {
                clickAction.performed += ctx => OnClickPerformed?.Invoke();
                clickAction.canceled += ctx => OnClickCanceled?.Invoke();
             }
            else {
                    clickAction.performed -= ctx => OnClickPerformed?.Invoke();
                    clickAction.canceled -= ctx => OnClickCanceled?.Invoke();
             }
            if(bind) {
                rightClickAction.performed += ctx => OnRightClickPerformed?.Invoke();
                rightClickAction.canceled += ctx => OnRightClickCanceled?.Invoke();
             }
            else {
                    rightClickAction.performed -= ctx => OnRightClickPerformed?.Invoke();
                    rightClickAction.canceled -= ctx => OnRightClickCanceled?.Invoke();
             }
            if(bind) {
                middleClickAction.performed += ctx => OnMiddleClickPerformed?.Invoke();
                middleClickAction.canceled += ctx => OnMiddleClickCanceled?.Invoke();
             }
            else {
                    middleClickAction.performed -= ctx => OnMiddleClickPerformed?.Invoke();
                    middleClickAction.canceled -= ctx => OnMiddleClickCanceled?.Invoke();
             }
            if(bind) {
                scrollWheelAction.performed += ctx => OnScrollWheelPerformed?.Invoke();
                scrollWheelAction.canceled += ctx => OnScrollWheelCanceled?.Invoke();
             }
            else {
                    scrollWheelAction.performed -= ctx => OnScrollWheelPerformed?.Invoke();
                    scrollWheelAction.canceled -= ctx => OnScrollWheelCanceled?.Invoke();
             }
            if(bind) {
                trackedDevicePositionAction.performed += ctx => OnTrackedDevicePositionPerformed?.Invoke();
                trackedDevicePositionAction.canceled += ctx => OnTrackedDevicePositionCanceled?.Invoke();
             }
            else {
                    trackedDevicePositionAction.performed -= ctx => OnTrackedDevicePositionPerformed?.Invoke();
                    trackedDevicePositionAction.canceled -= ctx => OnTrackedDevicePositionCanceled?.Invoke();
             }
            if(bind) {
                trackedDeviceOrientationAction.performed += ctx => OnTrackedDeviceOrientationPerformed?.Invoke();
                trackedDeviceOrientationAction.canceled += ctx => OnTrackedDeviceOrientationCanceled?.Invoke();
             }
            else {
                    trackedDeviceOrientationAction.performed -= ctx => OnTrackedDeviceOrientationPerformed?.Invoke();
                    trackedDeviceOrientationAction.canceled -= ctx => OnTrackedDeviceOrientationCanceled?.Invoke();
             }
        }

        void InitializeInputActions()
        {
            FieldInfo[] fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach(FieldInfo field in fields)
            {
                if(field.Name.EndsWith("Action"))
                {
                    string aName = char.ToUpper(field.Name[0]) + field.Name.Substring(1).Replace("Action", "");
                    field.SetValue(this, InputActions.FindAction(aName));
                }
            }
        }
    }
}
