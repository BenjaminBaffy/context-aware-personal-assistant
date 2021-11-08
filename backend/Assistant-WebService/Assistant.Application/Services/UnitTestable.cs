using Assistant.Application.Interfaces;

namespace Assistant.Application.Services
{
    public class UnitTestable : IUnitTestable
    {
        public int Add(int left, int right)
        {
            return left + right;
        }

        public int Multiply(int left, int right)
        {
            return left * right;
        }
    }
}