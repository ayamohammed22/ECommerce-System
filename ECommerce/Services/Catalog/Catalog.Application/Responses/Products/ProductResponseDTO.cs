﻿using Catalog.Core.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Application.Responses.Products
{
    public class ProductResponseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string ImageFile { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal price { get; set; }
        public ProductBrand Brand { get; set; }
        public ProductType Type { get; set; }
    }
}
