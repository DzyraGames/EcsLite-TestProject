Hours spent: 43

A list of components that can be running on the server:
Events:
- ButtonPressedEvent
- ButtonUnpressedEvent
- DoorOpenedEvent
- TargetPositionReachedEvent

Movement:
- AccelerationComponent
- ConstantMoveSpeedComponent
- MoveSpeedComponent
- RotationSpeedComponent
- TargetMoveSpeedComponent
- DirectionComponent
- PingPongMoveComponent
- TargetPositionMoveComponent

Tags:
- DoorComponent
- OpenDoorButtonComponent
- PlayerComponent

Transform:
- PositionComponent
- RotationComponent


A list of systems that can be running on the server:
- AlignInDirectionSystem
- ButtonPressSystem
- MoveToTargetSystem
- OpenDoorSystem
- SpeedAccelerationSystem
