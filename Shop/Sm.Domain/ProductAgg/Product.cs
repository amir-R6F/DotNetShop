﻿using System;
using System.Collections.Generic;
using Shop.Domain;
using Sm.Domain.ProductCategoryAgg;
using Sm.Domain.ProductPictureAgg;

namespace Sm.Domain.ProductAgg
{
    public class Product : EntityBase
    {
        public string Name { get; private set; }

        public string Code { get; private set; }

        public double UnitPrice { get; private set; }

        public bool IsInStock { get; private set; }

        public string ShortDescription { get; private set; }

        public string Description { get; private set; }

        public string Picture { get; private set; }

        public string PictureAlt { get; private set; }

        public string PictureTitle { get; private set; }

        public long CategoryId { get; private set; }

        public string Slug { get; private set; }

        public string Keywords { get; private set; }

        public string MetaDescription { get; private set; }

        public ProductCategory Category { get; private set; }
        public List<ProductPicture> ProductPictures { get; private set; }

        public Product(string name, string code, double unitPrice, string shortDescription,
            string description, string picture, string pictureAlt, string pictureTitle, long categoryId, string slug,
            string keywords, string metaDescription)
        {
            Name = name;
            Code = code;
            UnitPrice = unitPrice;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            CategoryId = categoryId;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            ProductPictures = new List<ProductPicture>();

        }
        
        public void Edit(string name, string code, double unitPrice, string shortDescription,
            string description, string picture, string pictureAlt, string pictureTitle, long categoryId, string slug,
            string keywords, string metaDescription)
        {
            Name = name;
            Code = code;
            UnitPrice = unitPrice;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            CategoryId = categoryId;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            IsInStock = true;
        }

        public void InStock()
        {
            IsInStock = true;
        }
        public void NotInStock()
        {
            IsInStock = false;
        }
    }
}