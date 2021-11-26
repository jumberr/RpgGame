// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputMaster.inputactions'

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
            ""name"": ""PlayerMovement"",
            ""id"": ""08b22d77-2a2f-4dc6-9e4d-75b7711dad3e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f8e5f44b-d601-4200-9d82-0fadc7a5f8a4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""172f4ba9-7e21-4fca-beb3-daef09f7b1c0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprinting"",
                    ""type"": ""Button"",
                    ""id"": ""3143a5cc-f778-4f8c-99a9-c64f8ec27993"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""669a8b6a-b961-437c-a010-2e2e72ef612b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rolling"",
                    ""type"": ""Button"",
                    ""id"": ""6b3b57a3-2502-4b37-935b-9dabfca1a51e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""74c62442-f1e5-45b7-b119-45fc8f4e4e19"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""da4da9ef-1450-4722-b7b1-23084afa9ee2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2e07c7bb-3340-4c84-b807-b2af947e6bcc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""23c59362-01a4-4b3a-8819-e60122e1ecd3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""18c477a2-3bf5-4978-ab0f-202bd73212fd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7334a0e7-a1e7-44b8-a1f7-d9b7ed3897b9"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""772a787f-f6b9-4b7b-9f7d-4751c36df21b"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard"",
                    ""action"": ""Sprinting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36821a91-7bbe-48e2-afa5-a58078821330"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13db63bd-07f5-42f0-9f4a-e08dc22b5cd6"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard"",
                    ""action"": ""Rolling"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerFight"",
            ""id"": ""5b441bc6-1c1f-4e89-807b-e48513b26b25"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""2cd00b5b-b2fa-40a8-bd56-0d3862245537"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""04efe2f9-1deb-423c-b140-e86700021d36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""93710eff-776a-43c8-b19f-dd83dad633ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f334aa73-4600-407d-9998-a54f4e00ae5d"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard"",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b817a065-5089-4725-a4f6-e69a50103c2a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""647e2107-4973-49d7-8d9c-1bb87aa7bb99"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse and keyboard"",
            ""bindingGroup"": ""Mouse and keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Move = m_PlayerMovement.FindAction("Move", throwIfNotFound: true);
        m_PlayerMovement_Rotation = m_PlayerMovement.FindAction("Rotation", throwIfNotFound: true);
        m_PlayerMovement_Sprinting = m_PlayerMovement.FindAction("Sprinting", throwIfNotFound: true);
        m_PlayerMovement_Jump = m_PlayerMovement.FindAction("Jump", throwIfNotFound: true);
        m_PlayerMovement_Rolling = m_PlayerMovement.FindAction("Rolling", throwIfNotFound: true);
        // PlayerFight
        m_PlayerFight = asset.FindActionMap("PlayerFight", throwIfNotFound: true);
        m_PlayerFight_Newaction = m_PlayerFight.FindAction("New action", throwIfNotFound: true);
        m_PlayerFight_Block = m_PlayerFight.FindAction("Block", throwIfNotFound: true);
        m_PlayerFight_Attack = m_PlayerFight.FindAction("Attack", throwIfNotFound: true);
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

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Move;
    private readonly InputAction m_PlayerMovement_Rotation;
    private readonly InputAction m_PlayerMovement_Sprinting;
    private readonly InputAction m_PlayerMovement_Jump;
    private readonly InputAction m_PlayerMovement_Rolling;
    public struct PlayerMovementActions
    {
        private @InputMaster m_Wrapper;
        public PlayerMovementActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMovement_Move;
        public InputAction @Rotation => m_Wrapper.m_PlayerMovement_Rotation;
        public InputAction @Sprinting => m_Wrapper.m_PlayerMovement_Sprinting;
        public InputAction @Jump => m_Wrapper.m_PlayerMovement_Jump;
        public InputAction @Rolling => m_Wrapper.m_PlayerMovement_Rolling;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Rotation.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnRotation;
                @Sprinting.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSprinting;
                @Sprinting.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSprinting;
                @Sprinting.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSprinting;
                @Jump.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Rolling.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnRolling;
                @Rolling.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnRolling;
                @Rolling.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnRolling;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @Sprinting.started += instance.OnSprinting;
                @Sprinting.performed += instance.OnSprinting;
                @Sprinting.canceled += instance.OnSprinting;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Rolling.started += instance.OnRolling;
                @Rolling.performed += instance.OnRolling;
                @Rolling.canceled += instance.OnRolling;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // PlayerFight
    private readonly InputActionMap m_PlayerFight;
    private IPlayerFightActions m_PlayerFightActionsCallbackInterface;
    private readonly InputAction m_PlayerFight_Newaction;
    private readonly InputAction m_PlayerFight_Block;
    private readonly InputAction m_PlayerFight_Attack;
    public struct PlayerFightActions
    {
        private @InputMaster m_Wrapper;
        public PlayerFightActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_PlayerFight_Newaction;
        public InputAction @Block => m_Wrapper.m_PlayerFight_Block;
        public InputAction @Attack => m_Wrapper.m_PlayerFight_Attack;
        public InputActionMap Get() { return m_Wrapper.m_PlayerFight; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerFightActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerFightActions instance)
        {
            if (m_Wrapper.m_PlayerFightActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_PlayerFightActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_PlayerFightActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_PlayerFightActionsCallbackInterface.OnNewaction;
                @Block.started -= m_Wrapper.m_PlayerFightActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_PlayerFightActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_PlayerFightActionsCallbackInterface.OnBlock;
                @Attack.started -= m_Wrapper.m_PlayerFightActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerFightActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerFightActionsCallbackInterface.OnAttack;
            }
            m_Wrapper.m_PlayerFightActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
            }
        }
    }
    public PlayerFightActions @PlayerFight => new PlayerFightActions(this);
    private int m_MouseandkeyboardSchemeIndex = -1;
    public InputControlScheme MouseandkeyboardScheme
    {
        get
        {
            if (m_MouseandkeyboardSchemeIndex == -1) m_MouseandkeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse and keyboard");
            return asset.controlSchemes[m_MouseandkeyboardSchemeIndex];
        }
    }
    public interface IPlayerMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnSprinting(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnRolling(InputAction.CallbackContext context);
    }
    public interface IPlayerFightActions
    {
        void OnNewaction(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
}
