using System;
using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    [Serializable]
    public struct OpenDoorButtonComponent
    {
        public ConvertToEntity DoorEntityLink;
    }
}