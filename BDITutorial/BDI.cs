using System.Collections.Generic;

namespace BDITutorial
{
    public abstract class BDI<T>
    {
        protected BDI(IEnumerable<Attitude<BeliefType, T>> beliefs)
        {
            Beliefs = new List<Attitude<BeliefType, T>>(beliefs);
            Desires = new List<Attitude<DesireType, T>>();
            Intentions = new List<Attitude<IntentionType, T>>();
        }

        public IEnumerable<Attitude<DesireType, T>> Desires { get; set; }
        public IEnumerable<Attitude<BeliefType, T>> Beliefs { get; set; }
        public IEnumerable<Attitude<IntentionType, T>> Intentions { get; set; }

        protected abstract IEnumerable<Attitude<IntentionType, T>> Deliberate(
            IEnumerable<Attitude<DesireType, T>> desires);

        protected abstract T MeansEndReasoning(IEnumerable<Attitude<IntentionType, T>> intentions);
    }
}