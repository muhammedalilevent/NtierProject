using NTier.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTierProje.UI.Models
{
    public class Cart
    {
        private Dictionary<Guid, Product> _cart = null;
        public Cart()
        {
            _cart = new Dictionary<Guid, Product>();
        }

        public List<Product> CartProductList
        {
            get {
                return _cart.Values.ToList();
            }
        }

        public void AddCart(Product item)
        {
            if (!_cart.ContainsKey(item.Id))
            {
                _cart.Add(item.Id, item);
            }
            else
            {
                _cart[item.Id].Quantity = (int.Parse(_cart[item.Id].Quantity) + 1).ToString();
            }
        }
        public void RemoveCart(Guid id)
        {
            _cart.Remove(id);
        }

        public void DecreaseCart(Guid id)
        {
            _cart[id].Quantity = (int.Parse(_cart[id].Quantity) - 1).ToString();
            if (int.Parse(_cart[id].Quantity) <= 0)
            {
                _cart.Remove(id);
            }
        }
        public void IncreaseCart(Guid id)
        {
            _cart[id].Quantity = (int.Parse(_cart[id].Quantity) + 1).ToString();
        }
    }
}