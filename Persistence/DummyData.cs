using Domain;

namespace Persistence
{
    internal static class DummyData
    {
        public static IEnumerable<Book> CreateListBooks(int quantity)
        {
            return new List<Book>
            {
                    new Book
                    {
                        Title = "Charlotte’s Web – E.B. White",
                        Credits = 30.00,
                        Quantity = quantity,
                        ExpirationDate = null,
                        User = null,
                    },
                    new Book
                    {
                        Title = "Plot Summary",
                        Credits = 30.00,
                        Quantity= quantity,
                        ExpirationDate = null,
                        User = null,
                    },
                    new Book
                    {
                        Title = "Mieko and the Fifth Treasure – Eleanor Coerr",
                        Credits = 30.00,
                        Quantity = quantity,
                        ExpirationDate = null,
                        User = null,
                    },
                    new Book
                    {
                        Title = "The Outsiders – S.E. Hinton",
                        Credits = 30.00,
                        Quantity = quantity,
                        ExpirationDate = null,
                        User = null,
                    },
                    new Book
                    {
                        Title = "Charlotte’s Web – E.B. White",
                        Credits = 30.00,
                        Quantity = quantity,
                        ExpirationDate = null,
                        User = null,
                    },
            };
        }
    }
}
