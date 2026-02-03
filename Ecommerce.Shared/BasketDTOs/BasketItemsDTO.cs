using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.BasketDTOs
{
    public record BasketItemsDTO
    (
      int Id,
      [Range(0, double.MaxValue)]decimal Price,
      [Range(0, 100)] int Quantity,
      string ProductName = default!,
      string PictureUrl = default!
    );

}