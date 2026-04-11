using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class InputReaderAdapter: MonoBehaviour, PlayerInputs.IPlayerActions, IInput
 {

     private PlayerInputs _inputActions;

     #region Constructor

     public static InputReaderAdapter Create()
     {
        InputReaderAdapter readerAdapter = FindFirstObjectByType<InputReaderAdapter>();
        if (readerAdapter) return readerAdapter;
        
        GameObject obj = new GameObject("InputReader");
        readerAdapter = obj.AddComponent<InputReaderAdapter>();
        return readerAdapter;
     }

     #endregion
     

     #region Unity Methods

     void OnEnable()
     {
         if (_inputActions == null)
         {
             _inputActions = new PlayerInputs();
             _inputActions.Player.SetCallbacks(this);
         }
         _inputActions.Enable();                                // Enable all actions within map.
     }

     void OnDisable()
     {
         _inputActions.Disable();                               // Disable all actions within map.
     }
     void OnDestroy()
     {
         _inputActions.Dispose();                              // Destroy asset object.
     }

     #endregion

     
     #region Movement
     public Vector2 _movementVector;

     // Invoked when "Move" action is either started, performed or canceled.
     public void OnMove(InputAction.CallbackContext context)
     {
         _movementVector = context.ReadValue<Vector2>();
     }

     

     public Vector2 GetMovementVector() => _movementVector;
     #endregion


     #region Shoot

     private bool _isShooting;

     public void OnShoot(InputAction.CallbackContext context)
     {
         _isShooting = context.ReadValueAsButton();
     }

     public bool IsFireActionPressed() => _isShooting;

     #endregion

 }
