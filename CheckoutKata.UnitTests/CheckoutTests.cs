using System.Collections.Generic;
using Xunit;

namespace CheckoutKata.UnitTests
{
    public class CheckoutTests
    {
        private readonly Dictionary<string, decimal> _stockKeepingUnits;
        private readonly PromotionsCalculator _promotionsCalculator;

        public CheckoutTests()
        {
            _stockKeepingUnits = new Dictionary<string, decimal>
            {
                {"A", 10},
                {"B", 15},
                {"C", 40},
                {"D", 55}
            };

            var promotions = new List<Promotion>
            {
                new("B", 3, 5),
                new("D", 2, (decimal) (110 * 0.25))
            };

            _promotionsCalculator = new PromotionsCalculator(promotions);
        }

        [Fact]
        public void Given_there_are_no_items_in_the_basket_the_total_items_count_should_be_zero()
        {
            //Arrange
            var checkout = new Checkout(_stockKeepingUnits, _promotionsCalculator);

            //Act
            var totalItems = checkout.TotalItems();

            //Assert
            Assert.Equal(0, totalItems);
        }

        [Fact]
        public void Given_I_have_selected_to_add_an_item_to_the_basket_Then_the_item_should_be_added_to_the_basket()
        {
            //Arrange
            const string item = "A";
            var checkout = new Checkout(_stockKeepingUnits, _promotionsCalculator);

            //Act
            checkout.Scan(item);

            //Assert
            var totalItems = checkout.TotalItems();
            Assert.Equal(1, totalItems);
        }

        [Theory]
        [InlineData("A", 10)]
        [InlineData("B", 15)]
        [InlineData("C", 40)]
        [InlineData("D", 55)]
        public void Given_an_item_have_been_added_to_the_basket_Then_the_total_cost_of_the_basket_should_be_calculated(
            string item, decimal unitPrice)
        {
            //Arrange
            var checkout = new Checkout(_stockKeepingUnits, new PromotionsCalculator(new List<Promotion>()));
            checkout.Scan(item);

            //Act
            var totalCost = checkout.TotalCost();

            //Assert
            Assert.Equal(unitPrice, totalCost);
        }

        [Theory]
        [InlineData("AA", 20)]
        [InlineData("BB", 30)]
        [InlineData("CC", 80)]
        [InlineData("DD", 110)]
        public void
            Given_multiple_items_have_been_added_to_the_basket_Then_the_total_cost_of_the_basket_should_be_calculated(
                string items, decimal expectedTotalCost)
        {
            //Arrange
            var checkout = new Checkout(_stockKeepingUnits, new PromotionsCalculator(new List<Promotion>()));

            foreach (var item in items)
                checkout.Scan(item.ToString());

            //Act
            var actualTotalCost = checkout.TotalCost();

            //Assert
            Assert.Equal(expectedTotalCost, actualTotalCost);
        }

        [Theory]
        [InlineData("BBB", 40)]
        [InlineData("DD", 82.50)]
        public void Given_I_have_added_enough_items_for_a_promotion_the_promotion_should_be_applied(string items,
            decimal expectedTotal)
        {
            //Arrange
            var checkout = new Checkout(_stockKeepingUnits, _promotionsCalculator);

            //Act
            foreach (var item in items)
            {
                checkout.Scan(item.ToString());
            }

            //Assert
            var actualTotalCost = checkout.TotalCost();
            Assert.Equal(expectedTotal, actualTotalCost);
        }

        [Theory]
        [InlineData("BBBBBB", 80)]
        [InlineData("DDDD", 165)]
        public void A_promotion_should_be_applied_for_every_multiple_required_to_achieve_a_given_promotion(string items,
            decimal expectedTotal)
        {
            //Arrange
            var checkout = new Checkout(_stockKeepingUnits, _promotionsCalculator);

            //Act
            foreach (var item in items)
            {
                checkout.Scan(item.ToString());
            }

            //Assert
            var actualTotalCost = checkout.TotalCost();
            Assert.Equal(expectedTotal, actualTotalCost);
        }
    }
}