using Xunit;

namespace CheckoutKata.UnitTests
{
    public class CheckoutTests
    {
        [Fact]
        public void Given_there_are_no_items_in_the_basket_the_total_items_count_should_be_zero()
        {
            //Arrange
            var checkout = new Checkout();

            //Act
            var totalItems = checkout.TotalItems();

            //Assert
            Assert.Equal(0, totalItems);
        }
    }
}