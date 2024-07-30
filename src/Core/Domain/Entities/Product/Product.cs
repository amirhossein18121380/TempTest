using Domain.Common;

namespace Domain.Entities.Product;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public DateTime ProduceDate { get; set; }
    public string ManufacturePhone { get; set; }
    public string ManufactureEmail { get; set; }
    public bool IsAvailable { get; set; }

    public Product(string name, DateTime produceDate, string manufacturePhone, string manufactureEmail, bool isAvailable)
    {
        Name = name;
        ProduceDate = produceDate;
        ManufacturePhone = manufacturePhone;
        ManufactureEmail = manufactureEmail;
        IsAvailable = isAvailable;
    }

    public Product() { }

    public bool IsDeleted { get; set; }

    #region Navigation Properties

    public User.User User { get; set; }
    public int CreatedByUserId { get; set; }

    #endregion
}