using CarRental.Domain.Billing.Enums;

namespace CarRental.Domain.Billing.Entities;

public partial class InvoiceItem
{
    public long InvoiceItemId { get; set; }

    public long InvoiceId { get; set; }

    public InvoiceItemType ItemType { get; set; }

    public string Description { get; set; } = null!;

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal LineTotal { get; set; }

    public short SortOrder { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
