﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetLicensingClient.RestController;
using NetLicensingClient.Entities;
using System.Data;

namespace NetLicensingClient
{
    /// <summary>
    /// C# representation of the Product Service. See NetLicensingAPI JavaDoc for details:
    /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/ProductService.html
    /// </summary>
    public class ProductService
    {
        /// <summary>
        /// Creates new product object with given properties. See NetLicensingAPI JavaDoc for details:
        /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/ProductService.html
        /// </summary>
        public static Product create(Context context, Product newProduct)
        {
            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Product.ENDPOINT_PATH, newProduct.ToDictionary());
            return new Product(output.items[0]);
        }

        /// <summary>
        /// Gets product by its number. See NetLicensingAPI JavaDoc for details:
        /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/ProductService.html
        /// </summary>
        public static Product get(Context context, String number)
        {
            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Product.ENDPOINT_PATH + "/" + number, null);
            return new Product(output.items[0]);
        }

        /// <summary>
        /// Returns all products of a vendor. See NetLicensingAPI JavaDoc for details:
        /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/ProductService.html
        /// </summary>
        public static List<Product> list(Context context, String filter)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (filter != null && filter.Length > 0)
            {
                parameters.Add("filter", filter);
            } 

            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Product.ENDPOINT_PATH, null);

            List<Product> products = new List<Product>();
            foreach (item i in output.items)
            {
               products.Add(new Product(i));
            }
            return products;
        }

        /// <summary>
        /// Updates product properties. See NetLicensingAPI JavaDoc for details:
        /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/ProductService.html
        /// </summary>
        public static Product update(Context context, String number, Product updateProduct)
        {
            updateProduct.number = number;
            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Product.ENDPOINT_PATH + "/" + number, updateProduct.ToDictionary());
            return new Product(output.items[0]);
        }

        /// <summary>
        /// Deletes product. See NetLicensingAPI JavaDoc for details:
        /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/ProductService.html
        /// </summary>
        public static void delete(Context context, String number, bool forceCascade)
        {
            String strCascade = Convert.ToString(forceCascade).ToLower();
            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.DELETE, Constants.Product.ENDPOINT_PATH + "/" + number, Utilities.forceCascadeToDict(forceCascade));
        }

    }
}