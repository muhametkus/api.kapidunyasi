using Api.KapiDunyasi.Domain.Common;

namespace Api.KapiDunyasi.Domain.Categories;

public class Category : AggregateRoot
{
    public string NameTR { get; private set; }
    public string Slug { get; private set; }
    public string? DescriptionTR { get; private set; }

    public Guid? ParentId { get; private set; }
    public Category? Parent { get; private set; }

    public ICollection<Category> Children { get; private set; } = new List<Category>();

    protected Category() { }

    public Category(
        string nameTR,
        string slug,
        string? descriptionTR = null,
        Guid? parentId = null)
    {
        NameTR = nameTR;
        Slug = slug;
        DescriptionTR = descriptionTR;
        ParentId = parentId;
    }
}