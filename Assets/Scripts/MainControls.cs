// GENERATED AUTOMATICALLY FROM 'Assets/Main Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class MainControls : IInputActionCollection
{
    private InputActionAsset asset;
    public MainControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Main Controls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""a82038d5-2756-47e1-ace0-1676f8f87010"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""id"": ""e48c2236-5e3c-48a9-8490-7d6de5c724c4"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard (Arrows)"",
                    ""id"": ""dab16a9f-3e14-4492-8f31-ea9daabf9199"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5e0be5c9-3c3b-4d94-9b3d-a918db41c6db"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""down"",
                    ""id"": ""38a2d25c-abba-42b1-a887-5bd28becfe28"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c6aa0e2f-2924-4429-b613-2ad4eb7d2ded"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f45a5cdc-4136-4504-863f-b33b5ac253c2"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Gamepad (Dpad)"",
                    ""id"": ""57798076-6e39-4b41-8fda-2e5acab131a0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c9d3adce-bfd4-4079-bb71-f3d4c37cac5b"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ed9225de-1149-4343-bb0c-bfed21298666"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""left"",
                    ""id"": ""72638aff-f917-44fb-af78-1eca9946c8b3"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7d002197-8bc1-4234-9889-2036eb81b528"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                }
            ]
        },
        {
            ""name"": ""Player Switch"",
            ""id"": ""677ac754-8120-4507-a79f-68e6d75372e4"",
            ""actions"": [
                {
                    ""name"": ""Next Player"",
                    ""id"": ""8d7caeed-1f76-415a-a213-9ea8fe637165"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Prev Player"",
                    ""id"": ""9e8f5515-a118-4247-b1bb-1a8cefa29cbd"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""23a8bfad-81b5-4419-88b3-2ac166121cf1"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next Player"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""19b8d2d4-d76e-4761-80c2-481917ab53da"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next Player"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""b73f0350-e530-445d-8b63-b9dc15340969"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Prev Player"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                }
            ]
        },
        {
            ""name"": ""Cast"",
            ""id"": ""7d78ccfe-5ce2-49e6-ad03-1fba1847c4c9"",
            ""actions"": [
                {
                    ""name"": ""Cast"",
                    ""id"": ""b46fc351-b127-42b4-9d44-cdba4699c0f7"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Restart"",
                    ""id"": ""293177ad-c123-4fbc-9296-e0315400764e"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b57d8b6c-8211-4802-9b86-b383a1bf51ae"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""e4411e3c-1277-471a-807d-efdd834adadf"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""e30f49e1-e523-48a5-a335-9c1d96043641"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""eb3b323e-a3d6-4eb2-96b2-e54fea062807"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""fe0e64f0-8af1-4400-8202-99e4eb72a2f4"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""id"": ""64424d38-b7e6-4887-8d3a-4140b171fb91"",
                    ""expectedControlLayout"": """",
                    ""continuous"": true,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Overhead"",
                    ""id"": ""5e8d4c57-bc28-4e29-aad4-c9ff0df80f32"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Help"",
                    ""id"": ""8c7d34cd-ea87-4172-b37f-c05422754828"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""108f7c62-d2f6-4b54-aac6-f4609e796e71"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""374144a3-e688-4876-9293-af633dfb103f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""54dec1eb-a427-46bc-8b4d-b52718c7bc13"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""7b598c5c-a974-4abb-b9af-36a721fe6f71"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d91747db-2760-448c-9733-092a33a8fd70"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f9ea02cc-08c0-49c6-b3ab-98f27c49df27"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""70091b31-4889-4003-bd2c-14e3613baf94"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Overhead"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""26537f95-8295-4927-9af5-74fdb8f6c0bf"",
                    ""path"": ""<Keyboard>/f2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Overhead"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""389b6e5e-daca-4887-9278-1373f219efa8"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Help"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""ba399666-1571-46cf-ad7b-1775b07b7449"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Help"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.GetActionMap("Movement");
        m_Movement_Move = m_Movement.GetAction("Move");
        // Player Switch
        m_PlayerSwitch = asset.GetActionMap("Player Switch");
        m_PlayerSwitch_NextPlayer = m_PlayerSwitch.GetAction("Next Player");
        m_PlayerSwitch_PrevPlayer = m_PlayerSwitch.GetAction("Prev Player");
        // Cast
        m_Cast = asset.GetActionMap("Cast");
        m_Cast_Cast = m_Cast.GetAction("Cast");
        m_Cast_Restart = m_Cast.GetAction("Restart");
        // Camera
        m_Camera = asset.GetActionMap("Camera");
        m_Camera_Rotate = m_Camera.GetAction("Rotate");
        m_Camera_Overhead = m_Camera.GetAction("Overhead");
        m_Camera_Help = m_Camera.GetAction("Help");
    }

    ~MainControls()
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

    public ReadOnlyArray<InputControlScheme> controlSchemes
    {
        get => asset.controlSchemes;
    }

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

    // Movement
    private InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private InputAction m_Movement_Move;
    public struct MovementActions
    {
        private MainControls m_Wrapper;
        public MovementActions(MainControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move { get { return m_Wrapper.m_Movement_Move; } }
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                Move.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                Move.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                Move.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.canceled += instance.OnMove;
            }
        }
    }
    public MovementActions @Movement
    {
        get
        {
            return new MovementActions(this);
        }
    }

    // Player Switch
    private InputActionMap m_PlayerSwitch;
    private IPlayerSwitchActions m_PlayerSwitchActionsCallbackInterface;
    private InputAction m_PlayerSwitch_NextPlayer;
    private InputAction m_PlayerSwitch_PrevPlayer;
    public struct PlayerSwitchActions
    {
        private MainControls m_Wrapper;
        public PlayerSwitchActions(MainControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @NextPlayer { get { return m_Wrapper.m_PlayerSwitch_NextPlayer; } }
        public InputAction @PrevPlayer { get { return m_Wrapper.m_PlayerSwitch_PrevPlayer; } }
        public InputActionMap Get() { return m_Wrapper.m_PlayerSwitch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PlayerSwitchActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerSwitchActions instance)
        {
            if (m_Wrapper.m_PlayerSwitchActionsCallbackInterface != null)
            {
                NextPlayer.started -= m_Wrapper.m_PlayerSwitchActionsCallbackInterface.OnNextPlayer;
                NextPlayer.performed -= m_Wrapper.m_PlayerSwitchActionsCallbackInterface.OnNextPlayer;
                NextPlayer.canceled -= m_Wrapper.m_PlayerSwitchActionsCallbackInterface.OnNextPlayer;
                PrevPlayer.started -= m_Wrapper.m_PlayerSwitchActionsCallbackInterface.OnPrevPlayer;
                PrevPlayer.performed -= m_Wrapper.m_PlayerSwitchActionsCallbackInterface.OnPrevPlayer;
                PrevPlayer.canceled -= m_Wrapper.m_PlayerSwitchActionsCallbackInterface.OnPrevPlayer;
            }
            m_Wrapper.m_PlayerSwitchActionsCallbackInterface = instance;
            if (instance != null)
            {
                NextPlayer.started += instance.OnNextPlayer;
                NextPlayer.performed += instance.OnNextPlayer;
                NextPlayer.canceled += instance.OnNextPlayer;
                PrevPlayer.started += instance.OnPrevPlayer;
                PrevPlayer.performed += instance.OnPrevPlayer;
                PrevPlayer.canceled += instance.OnPrevPlayer;
            }
        }
    }
    public PlayerSwitchActions @PlayerSwitch
    {
        get
        {
            return new PlayerSwitchActions(this);
        }
    }

    // Cast
    private InputActionMap m_Cast;
    private ICastActions m_CastActionsCallbackInterface;
    private InputAction m_Cast_Cast;
    private InputAction m_Cast_Restart;
    public struct CastActions
    {
        private MainControls m_Wrapper;
        public CastActions(MainControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Cast { get { return m_Wrapper.m_Cast_Cast; } }
        public InputAction @Restart { get { return m_Wrapper.m_Cast_Restart; } }
        public InputActionMap Get() { return m_Wrapper.m_Cast; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(CastActions set) { return set.Get(); }
        public void SetCallbacks(ICastActions instance)
        {
            if (m_Wrapper.m_CastActionsCallbackInterface != null)
            {
                Cast.started -= m_Wrapper.m_CastActionsCallbackInterface.OnCast;
                Cast.performed -= m_Wrapper.m_CastActionsCallbackInterface.OnCast;
                Cast.canceled -= m_Wrapper.m_CastActionsCallbackInterface.OnCast;
                Restart.started -= m_Wrapper.m_CastActionsCallbackInterface.OnRestart;
                Restart.performed -= m_Wrapper.m_CastActionsCallbackInterface.OnRestart;
                Restart.canceled -= m_Wrapper.m_CastActionsCallbackInterface.OnRestart;
            }
            m_Wrapper.m_CastActionsCallbackInterface = instance;
            if (instance != null)
            {
                Cast.started += instance.OnCast;
                Cast.performed += instance.OnCast;
                Cast.canceled += instance.OnCast;
                Restart.started += instance.OnRestart;
                Restart.performed += instance.OnRestart;
                Restart.canceled += instance.OnRestart;
            }
        }
    }
    public CastActions @Cast
    {
        get
        {
            return new CastActions(this);
        }
    }

    // Camera
    private InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private InputAction m_Camera_Rotate;
    private InputAction m_Camera_Overhead;
    private InputAction m_Camera_Help;
    public struct CameraActions
    {
        private MainControls m_Wrapper;
        public CameraActions(MainControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate { get { return m_Wrapper.m_Camera_Rotate; } }
        public InputAction @Overhead { get { return m_Wrapper.m_Camera_Overhead; } }
        public InputAction @Help { get { return m_Wrapper.m_Camera_Help; } }
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                Rotate.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotate;
                Rotate.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotate;
                Rotate.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotate;
                Overhead.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnOverhead;
                Overhead.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnOverhead;
                Overhead.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnOverhead;
                Help.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnHelp;
                Help.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnHelp;
                Help.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnHelp;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                Rotate.started += instance.OnRotate;
                Rotate.performed += instance.OnRotate;
                Rotate.canceled += instance.OnRotate;
                Overhead.started += instance.OnOverhead;
                Overhead.performed += instance.OnOverhead;
                Overhead.canceled += instance.OnOverhead;
                Help.started += instance.OnHelp;
                Help.performed += instance.OnHelp;
                Help.canceled += instance.OnHelp;
            }
        }
    }
    public CameraActions @Camera
    {
        get
        {
            return new CameraActions(this);
        }
    }
    public interface IMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IPlayerSwitchActions
    {
        void OnNextPlayer(InputAction.CallbackContext context);
        void OnPrevPlayer(InputAction.CallbackContext context);
    }
    public interface ICastActions
    {
        void OnCast(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnRotate(InputAction.CallbackContext context);
        void OnOverhead(InputAction.CallbackContext context);
        void OnHelp(InputAction.CallbackContext context);
    }
}
