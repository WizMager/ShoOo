using Views.Impl.Ai;

namespace Utils.AiPool
{
    public interface IAiPool
    {
        AAiView GetAi();
        
        void ReleaseAi(AAiView aiView);
    }
}