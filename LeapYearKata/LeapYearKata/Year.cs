namespace LeapYearKata
{
    public class Year
    {
        readonly int value;

        public Year(int value)
        {
            this.value = value;
        }

        public bool IsLeap =>
            value % 400 == 0 ||
            value % 4 == 0 &&
            value % 100 != 0;
    }
}