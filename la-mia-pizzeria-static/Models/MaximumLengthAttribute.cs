namespace la_mia_pizzeria_static.Models
{
    internal class MaximumLengthAttribute : Attribute
    {
        public MaximumLengthAttribute(int v)
        {
            V = v;
        }

        public int V { get; }
    }
}