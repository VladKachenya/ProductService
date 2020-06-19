namespace ProductManager.Data
{
    public class DataMapper
    {
        public Product ToProduct(ProductDto productDto)
        {
            return new Product
            {
                Number = productDto.Number,
                MinQty = productDto.MinQty,
                Category = productDto.Category,
                State = (Status)productDto.State,
                Qty = productDto.Qty
            };
        }

        public ProductDto ToProductDto(Product product)
        {

            return new ProductDto
            {
                Number = product.Number,
                MinQty = product.MinQty,
                Category = product.Category,
                State = (int)product.State,
                Qty = product.Qty
            };
        }
    }
}
