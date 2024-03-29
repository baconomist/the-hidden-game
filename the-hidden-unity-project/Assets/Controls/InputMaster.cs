// GENERATED AUTOMATICALLY FROM 'Assets/Controls/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""FpsController"",
            ""id"": ""858eb11a-7e90-4c79-9da3-a445cb728b83"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""4d274356-fbb5-43b5-b1f9-593057c9bb15"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""ScaleVector2(x=10000,y=10000)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""9a7117e8-7b57-4145-8cc0-e95f6aefafa3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""91af2f98-f676-42c3-98de-04ed0add6ac3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BoostForward"",
                    ""type"": ""Button"",
                    ""id"": ""a51672e7-7541-4c0d-82a6-031e7c9cef86"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""d8562925-82df-47e4-a11d-10e71fcd4be1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""063189fe-627c-42f4-8d12-05f5441442d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""91050074-ee75-4b61-9371-c4394d3aa74d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""wasd"",
                    ""id"": ""8a9eb0b1-faeb-493d-8ec1-80870da4e28c"",
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
                    ""id"": ""fff4cd71-194b-45be-a294-433697c0321e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e5ed3df9-3c06-4fd6-a192-70e888f453ad"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9fd6e950-bfe1-4138-b514-db9df2683f95"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""dfcbb597-5456-4d2e-ab11-026b689a78b3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""arrowKeys"",
                    ""id"": ""69fe60ee-41d5-4a40-bae7-fb845071efc1"",
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
                    ""id"": ""71504a54-d36b-4116-affe-7e048f2b1cea"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""146d481d-168c-4525-9e41-a5f298330c0a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7c5ee2fd-72a6-4526-88b8-b7a21055941d"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""32c471f7-21c0-48e2-a5d1-4b713f9f8a32"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d3783260-489a-45c8-ab63-313bcce06188"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e2c204f-af66-4e5b-b9ea-ea2745d30aac"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""BoostForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ec86e8e-3093-4e1f-b4a6-28516542d876"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0a7ea14-9099-4781-ac57-959c4b0d7b3b"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TheHiddenAttackController"",
            ""id"": ""13ebb2c5-3aa4-476f-b848-f9ecb7e2576b"",
            ""actions"": [
                {
                    ""name"": ""Primary Attack"",
                    ""type"": ""Button"",
                    ""id"": ""f5fbc97b-e0e1-4cdd-954e-8724a54e4bdf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Secondary Attack"",
                    ""type"": ""Button"",
                    ""id"": ""74b6d608-8c25-45eb-a232-a77ca322f7c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""796380ba-465a-481a-a311-d3714bd6e291"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Primary Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e75bfcee-3608-4f0a-aff5-176b2e01fd38"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Secondary Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // FpsController
        m_FpsController = asset.FindActionMap("FpsController", throwIfNotFound: true);
        m_FpsController_Look = m_FpsController.FindAction("Look", throwIfNotFound: true);
        m_FpsController_Movement = m_FpsController.FindAction("Movement", throwIfNotFound: true);
        m_FpsController_Jump = m_FpsController.FindAction("Jump", throwIfNotFound: true);
        m_FpsController_BoostForward = m_FpsController.FindAction("BoostForward", throwIfNotFound: true);
        m_FpsController_Crouch = m_FpsController.FindAction("Crouch", throwIfNotFound: true);
        m_FpsController_Reload = m_FpsController.FindAction("Reload", throwIfNotFound: true);
        // TheHiddenAttackController
        m_TheHiddenAttackController = asset.FindActionMap("TheHiddenAttackController", throwIfNotFound: true);
        m_TheHiddenAttackController_PrimaryAttack = m_TheHiddenAttackController.FindAction("Primary Attack", throwIfNotFound: true);
        m_TheHiddenAttackController_SecondaryAttack = m_TheHiddenAttackController.FindAction("Secondary Attack", throwIfNotFound: true);
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

    // FpsController
    private readonly InputActionMap m_FpsController;
    private IFpsControllerActions m_FpsControllerActionsCallbackInterface;
    private readonly InputAction m_FpsController_Look;
    private readonly InputAction m_FpsController_Movement;
    private readonly InputAction m_FpsController_Jump;
    private readonly InputAction m_FpsController_BoostForward;
    private readonly InputAction m_FpsController_Crouch;
    private readonly InputAction m_FpsController_Reload;
    public struct FpsControllerActions
    {
        private @InputMaster m_Wrapper;
        public FpsControllerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_FpsController_Look;
        public InputAction @Movement => m_Wrapper.m_FpsController_Movement;
        public InputAction @Jump => m_Wrapper.m_FpsController_Jump;
        public InputAction @BoostForward => m_Wrapper.m_FpsController_BoostForward;
        public InputAction @Crouch => m_Wrapper.m_FpsController_Crouch;
        public InputAction @Reload => m_Wrapper.m_FpsController_Reload;
        public InputActionMap Get() { return m_Wrapper.m_FpsController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FpsControllerActions set) { return set.Get(); }
        public void SetCallbacks(IFpsControllerActions instance)
        {
            if (m_Wrapper.m_FpsControllerActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnLook;
                @Movement.started -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnJump;
                @BoostForward.started -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnBoostForward;
                @BoostForward.performed -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnBoostForward;
                @BoostForward.canceled -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnBoostForward;
                @Crouch.started -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnCrouch;
                @Reload.started -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_FpsControllerActionsCallbackInterface.OnReload;
            }
            m_Wrapper.m_FpsControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @BoostForward.started += instance.OnBoostForward;
                @BoostForward.performed += instance.OnBoostForward;
                @BoostForward.canceled += instance.OnBoostForward;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
            }
        }
    }
    public FpsControllerActions @FpsController => new FpsControllerActions(this);

    // TheHiddenAttackController
    private readonly InputActionMap m_TheHiddenAttackController;
    private ITheHiddenAttackControllerActions m_TheHiddenAttackControllerActionsCallbackInterface;
    private readonly InputAction m_TheHiddenAttackController_PrimaryAttack;
    private readonly InputAction m_TheHiddenAttackController_SecondaryAttack;
    public struct TheHiddenAttackControllerActions
    {
        private @InputMaster m_Wrapper;
        public TheHiddenAttackControllerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryAttack => m_Wrapper.m_TheHiddenAttackController_PrimaryAttack;
        public InputAction @SecondaryAttack => m_Wrapper.m_TheHiddenAttackController_SecondaryAttack;
        public InputActionMap Get() { return m_Wrapper.m_TheHiddenAttackController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TheHiddenAttackControllerActions set) { return set.Get(); }
        public void SetCallbacks(ITheHiddenAttackControllerActions instance)
        {
            if (m_Wrapper.m_TheHiddenAttackControllerActionsCallbackInterface != null)
            {
                @PrimaryAttack.started -= m_Wrapper.m_TheHiddenAttackControllerActionsCallbackInterface.OnPrimaryAttack;
                @PrimaryAttack.performed -= m_Wrapper.m_TheHiddenAttackControllerActionsCallbackInterface.OnPrimaryAttack;
                @PrimaryAttack.canceled -= m_Wrapper.m_TheHiddenAttackControllerActionsCallbackInterface.OnPrimaryAttack;
                @SecondaryAttack.started -= m_Wrapper.m_TheHiddenAttackControllerActionsCallbackInterface.OnSecondaryAttack;
                @SecondaryAttack.performed -= m_Wrapper.m_TheHiddenAttackControllerActionsCallbackInterface.OnSecondaryAttack;
                @SecondaryAttack.canceled -= m_Wrapper.m_TheHiddenAttackControllerActionsCallbackInterface.OnSecondaryAttack;
            }
            m_Wrapper.m_TheHiddenAttackControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PrimaryAttack.started += instance.OnPrimaryAttack;
                @PrimaryAttack.performed += instance.OnPrimaryAttack;
                @PrimaryAttack.canceled += instance.OnPrimaryAttack;
                @SecondaryAttack.started += instance.OnSecondaryAttack;
                @SecondaryAttack.performed += instance.OnSecondaryAttack;
                @SecondaryAttack.canceled += instance.OnSecondaryAttack;
            }
        }
    }
    public TheHiddenAttackControllerActions @TheHiddenAttackController => new TheHiddenAttackControllerActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IFpsControllerActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnBoostForward(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
    }
    public interface ITheHiddenAttackControllerActions
    {
        void OnPrimaryAttack(InputAction.CallbackContext context);
        void OnSecondaryAttack(InputAction.CallbackContext context);
    }
}
