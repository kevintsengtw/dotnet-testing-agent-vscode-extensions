using SampleCatalog = Practice.TUnit.Net8.Core.Services.BookCatalog;
using TUnit.Core;
using TUnit.Assertions;

public class BookCatalogCoverageTests
{
    private readonly SampleCatalog catalog = new();

    [Test]
    [Arguments("9780306406157", true)]
    [Arguments("9780306406158", false)]
    [Arguments("123", false)]
    public async Task IsbnValidation(string isbn, bool expected)
        => await Assert.That(catalog.IsValidIsbn13(isbn)).IsEqualTo(expected);

    [Test]
    [Arguments(1, 100)]
    [Arguments(2, 95)]
    [Arguments(5, 90)]
    [Arguments(10, 85)]
    public async Task MembershipDiscount(int years, int expected)
        => await Assert.That(catalog.CalculateDiscountPrice(100m, years)).IsEqualTo((decimal)expected);

    [Test]
    [Arguments(-1, 0)]
    [Arguments(0, 0)]
    [Arguments(3, 3)]
    [Arguments(100, 10)]
    public async Task OverdueFine(int days, int expected)
        => await Assert.That(catalog.CalculateOverdueFine(days, 20m)).IsEqualTo((decimal)expected);

    [Test]
    [Arguments(50, "小冊子")]
    [Arguments(150, "薄書")]
    [Arguments(300, "一般")]
    [Arguments(500, "厚書")]
    [Arguments(501, "巨著")]
    public async Task PageClassification(int pages, string expected)
        => await Assert.That(catalog.ClassifyByPageCount(pages)).IsEqualTo(expected);

    [Test]
    public async Task IndexCode()
        => await Assert.That(catalog.GenerateIndexCode("Technology", "Tseng", 2026)).IsEqualTo("TEC-T-2026");
}
