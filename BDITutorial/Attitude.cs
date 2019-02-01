namespace BDITutorial
{
    public class Attitude<TA, T>
    {
        public T AttitudeRepresentation;
        public TA Label;

        public Attitude(TA label, T attitudeRepresentation)
        {
            Label = label;
            AttitudeRepresentation = attitudeRepresentation;
        }
    }
}