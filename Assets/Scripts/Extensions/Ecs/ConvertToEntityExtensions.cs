using Voody.UniLeo.Lite;

namespace EcsLiteTestProject
{
    public static class ConvertToEntityExtensions
    {
        public static bool TryGetEntity<T>(this T entityLink, out int entity) where T : ConvertToEntity
        {
            int? tempEntity = entityLink.TryGetEntity();
            if (tempEntity != null)
            {
                entity = (int) tempEntity;
                return true;
            }
            
            entity = -1;
            return false;
        }
    }
}