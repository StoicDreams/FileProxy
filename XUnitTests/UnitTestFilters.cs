using Xunit;
using StoicDreams.FileProxy.Filter;

namespace XUnitTests
{
	public class UnitTestFilters
	{
		[Theory]
		[InlineData("https://www.test.com/folder/page.html?_=123&1=2", "/folder/page.html")]
		[InlineData("https://www.test.com/folder/page.html#truth", "/folder/page.html")]
		[InlineData("https://www.test.com/folder/page#truth", "/folder/page")]
		[InlineData("https://www.test.com/folder/page", "/folder/page")]
		[InlineData("https://www.test.com/page", "/page")]
		[InlineData("https://www.test.com/", "/")]
		[InlineData("/", "/")]
		[InlineData("/page", "/page")]
		[InlineData("/Folder/page", "/Folder/page")]
		public void TestFilterURLToRoutePath(string url, string expectedResult)
		{
			Assert.Equal(expectedResult, url.FilterURLToRoutePath());
		}
	}
}
