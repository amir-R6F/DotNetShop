using System.Collections.Generic;
using Shop.Domain;

namespace Sm.Domain.OrderAgg
{
    public class Order : EntityBase
    {


        public long AccountId { get; private set; }
        public double TotalAmount { get; private set; }
        public double DiscountAmount { get; private set; }
        public double PayAmount { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        public string IssueTrackingNum { get; private set; }
        public long RefId { get; private set; }
        public List<OrderItem> Items { get; private set; }
        
        public Order(long accountId, double totalAmount, double discountAmount, double payAmount)
        {
            AccountId = accountId;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            IsPaid = false;
            IsCanceled = false;
            RefId = 0;
            Items = new List<OrderItem>();
        }

        public void PaymentSucceeded(long refId)
        {
            IsPaid = true;
            if (refId != 0)
                RefId = refId;
        }
        
        public void SetIssueTrackingNum(string number)
        {
            IssueTrackingNum = number;
        }
        
        public void Cancel(string number)
        {
            IsCanceled = true;
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }
        
    }
}