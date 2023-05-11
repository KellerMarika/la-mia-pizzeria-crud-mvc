namespace la_mia_pizzeria_static.Models
{
    internal class MinimumLengthAttribute : Attribute
    {
        private int v;

        public MinimumLengthAttribute(int v)
        {
            this.v = v;
        }
    }
}