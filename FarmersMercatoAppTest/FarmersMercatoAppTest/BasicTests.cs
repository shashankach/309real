using System.Reflection;

public class BasicTests
{

    private static void Main(string[] args) { }

    public Double TotalPrice(List<Models.Product> products)
    {
        Double total = 0;

        if (products != null)
        {
            foreach (Models.Product product in products)
            {
                total += product.TotalPrice;
            }
        }

        return total;
    }
}