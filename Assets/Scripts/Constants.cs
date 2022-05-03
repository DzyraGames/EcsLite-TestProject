namespace EcsLiteTestProject
{
    public static class Constants
    {
        public const float MaxRaycastDistance = 100f;
        public const float DefaultMovementSpeed = 5f;

        public static class Tags
        {
            public const string Player = "Player";
            public const string Button = "Button";
        }

        public static class LayerMasks
        {
            public const string Ground = "Ground";
        }
    }
}