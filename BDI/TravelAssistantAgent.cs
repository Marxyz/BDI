using System.Collections.Generic;
using System.Linq;

namespace BDITutorial
{
    public class TravelAssistantAgent<T> : BDI<T> where T : Dictionary<string, string>
    {
        public TravelAssistantAgent(IEnumerable<Attitude<BeliefType, T>> beliefs) : base(beliefs)
        {
        }

        public T GetPlan(IEnumerable<Attitude<DesireType, T>> desires)
        {
            return MeansEndReasoning(Deliberate(desires));
        }

        protected override IEnumerable<Attitude<IntentionType, T>> Deliberate(
            IEnumerable<Attitude<DesireType, T>> desires)
        {
            return LookForTours(desires.ToList());
        }

        private IEnumerable<Attitude<IntentionType, T>> LookForTours(List<Attitude<DesireType, T>> desires)
        {
            var visitDesire = desires.First(d => d.Label == DesireType.Visit);
            var dateDesire = desires.First(d => d.Label == DesireType.Date);
            var maxBudgetDesire = desires.First(d => d.Label == DesireType.Budget);

            var citiesToVisit = visitDesire.AttitudeRepresentation["visiting"].Split(",");
            var dateFrom = dateDesire.AttitudeRepresentation["from"];
            var days = int.Parse(dateDesire.AttitudeRepresentation["days"]);

            var maxBudget = double.Parse(maxBudgetDesire.AttitudeRepresentation["max"]);

            var tourPackages = Beliefs.Where(b => b.Label == BeliefType.TourPackages);

            var result = new List<Attitude<IntentionType, T>>();

            foreach (var tourPackage in tourPackages)
            {
                var data = tourPackage.AttitudeRepresentation;
                var starts = data["starts"];
                var daysTour = int.Parse(data["days"]);
                var cities = data["cities"].Split(',');
                var price = double.Parse(data["price"]);
                if (daysTour <= days &&
                    cities.Intersect(citiesToVisit).Count() == cities.Length &&
                    starts == dateFrom &&
                    price < maxBudget)
                    result.Add(new Attitude<IntentionType, T>(IntentionType.BookTourPackage,
                        tourPackage.AttitudeRepresentation));
            }

            return result;
        }


        protected override T MeansEndReasoning(IEnumerable<Attitude<IntentionType, T>> intentions)
        {
            var enumerable = intentions.ToList();
            return enumerable.FirstOrDefault() == null ? null : enumerable.First().AttitudeRepresentation;
        }
    }
}