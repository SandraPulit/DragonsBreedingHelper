namespace DML_MobgameClient.DomainViewModels.DragonsDomain
{
    public class BreedingResult
    {
        public Dragon Child { get; }
        public string BreedingTime { get; }
        public string Odds { get; }
        public string Expect { get; }

        public BreedingResult(Dragon child, string breedingTime, string odds, string expect)
        {
            Child = child;
            BreedingTime = breedingTime;
            Odds = odds;
            Expect = expect;
        }
    }
}