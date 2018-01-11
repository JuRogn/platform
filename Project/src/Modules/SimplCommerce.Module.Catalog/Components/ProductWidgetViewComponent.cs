﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.Catalog.Models;
using SimplCommerce.Module.Catalog.Services;
using SimplCommerce.Module.Catalog.ViewModels;
using SimplCommerce.Module.Core.Services;
using SimplCommerce.Module.Core.ViewModels;

namespace SimplCommerce.Module.Catalog.Components
{
    public class ProductWidgetViewComponent : ViewComponent
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMediaService _mediaService;
        private readonly IProductPricingService _productPricingService;

        public ProductWidgetViewComponent(IRepository<Product> productRepository, IMediaService mediaService, IProductPricingService productPricingService)
        {
            _productRepository = productRepository;
            _mediaService = mediaService;
            _productPricingService = productPricingService;
        }

        public IViewComponentResult Invoke(WidgetInstanceViewModel widgetInstance)
        {
            var model = new ProductWidgetComponentVm
            {
                Id = widgetInstance.Id,
                WidgetName = widgetInstance.Name,
                Setting = JsonConvert.DeserializeObject<ProductWidgetSetting>(widgetInstance.Data)
            };

            var query = _productRepository.Query()
              .Where(x => x.IsPublished && x.IsVisibleIndividually);

            if (model.Setting.FeaturedOnly)
            {
                query = query.Where(x => x.IsFeatured);
            }

            model.Products = query
              .Include(x => x.ThumbnailImage)
              .OrderByDescending(x => x.CreatedOn)
              .Take(model.Setting.NumberOfProducts)
              .Select(x => ProductThumbnail.FromProduct(x)).ToList();

            foreach (var product in model.Products)
            {
                product.ThumbnailUrl = _mediaService.GetThumbnailUrl(product.ThumbnailImage);
                product.CalculatedProductPrice = _productPricingService.CalculateProductPrice(product);
            }

            return View("/Modules/SimplCommerce.Module.Catalog/Views/Components/ProductWidget.cshtml", model);
        }
    }
}
