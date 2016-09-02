using System.Diagnostics;

namespace DML_MobgameClient.DomainViewModels.DragonsDomain
{
    [DebuggerDisplay("First Parent = {FirstParent.Name}, Second Parent = {SecondParent.Name}, Odds = {ProbabilityOfBreed}, Exprected Time = {ExpectedTime}")]
    public class DragonRecipe
    {
        public Dragon FirstParent { get; }
        public Dragon SecondParent { get; }
        public string ProbabilityOfBreed { get; }
        public string ExpectedTime { get; }

        public DragonRecipe(Dragon firstParent, Dragon secondParent, string probabilityOfBreed, string expectedTime)
        {
            FirstParent = firstParent;
            SecondParent = secondParent;
            ProbabilityOfBreed = probabilityOfBreed;
            ExpectedTime = expectedTime;
        }
    }
}
