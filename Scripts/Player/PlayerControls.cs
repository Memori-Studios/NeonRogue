//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Player/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player Movement"",
            ""id"": ""e861bf4e-d008-49fb-aab1-3ad9df8393ec"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a435eddd-0f9a-4388-8ada-fcc1a79ac9ca"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4b9b6e2f-8e02-421e-923f-244739ff5284"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""64ef1776-026f-40e0-b62d-7f152f4f51d2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4e85a599-02f9-4d4c-87b1-65d234fd740b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""78c948d3-532a-4534-993d-494855bdaa50"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b85a28b0-fa4b-412e-85f9-f4b7127a91fb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5ffc0c70-6667-46ae-8363-06126b2c10df"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ba730b1f-019e-4188-8468-073c69600f2b"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player Actions"",
            ""id"": ""6d52c7c7-efc3-49a8-80c1-618f84cf30ab"",
            ""actions"": [
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""154da0a2-417f-4f2b-ae83-8a59b96c2dad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""9b6c452e-ed48-4d00-b773-df67a357ab54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""22337d97-a242-48e0-8331-c07420d1a16b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_1"",
                    ""type"": ""Button"",
                    ""id"": ""7bf32828-26fe-4dfa-afeb-dbd22c008c20"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_2"",
                    ""type"": ""Button"",
                    ""id"": ""6b3c75d7-297e-4c1c-9748-bff74aef0c99"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_3"",
                    ""type"": ""Button"",
                    ""id"": ""b4df5b70-8e12-49a6-b4f0-db27e4f2e0af"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FiringMode"",
                    ""type"": ""Button"",
                    ""id"": ""11afe4a2-4a40-42b8-9f57-871a19c355b7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""d295eeea-9eff-450e-b1c3-7cbe79df50f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sandevistan"",
                    ""type"": ""Button"",
                    ""id"": ""1f35a549-8401-45c6-abd0-a8a960f6b12d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3465fc73-91f3-43fc-98e1-1b7ed593d453"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""abf7f406-081e-4b18-ad72-0df57859dd06"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7da26ae6-11bb-477a-ac88-3492cccc891c"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4fc9a0a4-0446-40a2-8811-b44275106223"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""287b4e5a-0424-47f2-8d89-3af21a0ca28b"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot_2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6815ee85-e0ed-493f-9213-328dcd73856b"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot_3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27f7d7a2-e90e-48bf-af7d-472221e639d6"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FiringMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c84f906-6591-476c-b3a6-7101aa268f2c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9fbf1b2-e1a4-44c8-81b9-584c24318c77"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sandevistan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Movement
        m_PlayerMovement = asset.FindActionMap("Player Movement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Camera = m_PlayerMovement.FindAction("Camera", throwIfNotFound: true);
        // Player Actions
        m_PlayerActions = asset.FindActionMap("Player Actions", throwIfNotFound: true);
        m_PlayerActions_Escape = m_PlayerActions.FindAction("Escape", throwIfNotFound: true);
        m_PlayerActions_Roll = m_PlayerActions.FindAction("Roll", throwIfNotFound: true);
        m_PlayerActions_Reload = m_PlayerActions.FindAction("Reload", throwIfNotFound: true);
        m_PlayerActions_Slot_1 = m_PlayerActions.FindAction("Slot_1", throwIfNotFound: true);
        m_PlayerActions_Slot_2 = m_PlayerActions.FindAction("Slot_2", throwIfNotFound: true);
        m_PlayerActions_Slot_3 = m_PlayerActions.FindAction("Slot_3", throwIfNotFound: true);
        m_PlayerActions_FiringMode = m_PlayerActions.FindAction("FiringMode", throwIfNotFound: true);
        m_PlayerActions_Fire = m_PlayerActions.FindAction("Fire", throwIfNotFound: true);
        m_PlayerActions_Sandevistan = m_PlayerActions.FindAction("Sandevistan", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player Movement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Camera;
    public struct PlayerMovementActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Camera => m_Wrapper.m_PlayerMovement_Camera;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Camera.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // Player Actions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_Escape;
    private readonly InputAction m_PlayerActions_Roll;
    private readonly InputAction m_PlayerActions_Reload;
    private readonly InputAction m_PlayerActions_Slot_1;
    private readonly InputAction m_PlayerActions_Slot_2;
    private readonly InputAction m_PlayerActions_Slot_3;
    private readonly InputAction m_PlayerActions_FiringMode;
    private readonly InputAction m_PlayerActions_Fire;
    private readonly InputAction m_PlayerActions_Sandevistan;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Escape => m_Wrapper.m_PlayerActions_Escape;
        public InputAction @Roll => m_Wrapper.m_PlayerActions_Roll;
        public InputAction @Reload => m_Wrapper.m_PlayerActions_Reload;
        public InputAction @Slot_1 => m_Wrapper.m_PlayerActions_Slot_1;
        public InputAction @Slot_2 => m_Wrapper.m_PlayerActions_Slot_2;
        public InputAction @Slot_3 => m_Wrapper.m_PlayerActions_Slot_3;
        public InputAction @FiringMode => m_Wrapper.m_PlayerActions_FiringMode;
        public InputAction @Fire => m_Wrapper.m_PlayerActions_Fire;
        public InputAction @Sandevistan => m_Wrapper.m_PlayerActions_Sandevistan;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @Escape.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnEscape;
                @Roll.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRoll;
                @Reload.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnReload;
                @Slot_1.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSlot_1;
                @Slot_1.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSlot_1;
                @Slot_1.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSlot_1;
                @Slot_2.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSlot_2;
                @Slot_2.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSlot_2;
                @Slot_2.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSlot_2;
                @Slot_3.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSlot_3;
                @Slot_3.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSlot_3;
                @Slot_3.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSlot_3;
                @FiringMode.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFiringMode;
                @FiringMode.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFiringMode;
                @FiringMode.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFiringMode;
                @Fire.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFire;
                @Sandevistan.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSandevistan;
                @Sandevistan.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSandevistan;
                @Sandevistan.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSandevistan;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Slot_1.started += instance.OnSlot_1;
                @Slot_1.performed += instance.OnSlot_1;
                @Slot_1.canceled += instance.OnSlot_1;
                @Slot_2.started += instance.OnSlot_2;
                @Slot_2.performed += instance.OnSlot_2;
                @Slot_2.canceled += instance.OnSlot_2;
                @Slot_3.started += instance.OnSlot_3;
                @Slot_3.performed += instance.OnSlot_3;
                @Slot_3.canceled += instance.OnSlot_3;
                @FiringMode.started += instance.OnFiringMode;
                @FiringMode.performed += instance.OnFiringMode;
                @FiringMode.canceled += instance.OnFiringMode;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Sandevistan.started += instance.OnSandevistan;
                @Sandevistan.performed += instance.OnSandevistan;
                @Sandevistan.canceled += instance.OnSandevistan;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
    }
    public interface IPlayerActionsActions
    {
        void OnEscape(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnSlot_1(InputAction.CallbackContext context);
        void OnSlot_2(InputAction.CallbackContext context);
        void OnSlot_3(InputAction.CallbackContext context);
        void OnFiringMode(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnSandevistan(InputAction.CallbackContext context);
    }
}
