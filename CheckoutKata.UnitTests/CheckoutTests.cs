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

        [Fact]
        public void Given_I_have_selected_to_add_an_item_to_the_basket_Then_the_item_should_be_added_to_the_basket()
        {
            //Arrange
            const string item = "A";
            var checkout = new Checkout();

            //Act
            checkout.Scan(item);

            //Assert
            var totalItems = checkout.TotalItems();
            Assert.Equal(1, totalItems);
        }
    }
}