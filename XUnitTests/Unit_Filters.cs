using Xunit;
using StoicDreams.FileProxy.Filter;

namespace XUnitTests
{
	public class Unit_Filters
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
		[Theory]
		[InlineData("", false)]
		[InlineData(@"http://some.web/path", true)]
		[InlineData(@"http://some.web/folder/file", true)]
		[InlineData(@"http://some.web/folder/file.json", true)]
		[InlineData(@"/folder/file.json", false)]
		public void TestFilterIsProtocolFormat(string urlPath, bool result)
		{
			Assert.Equal(result, Matches.IsProtocolFormat(urlPath));
		}
	}
}
