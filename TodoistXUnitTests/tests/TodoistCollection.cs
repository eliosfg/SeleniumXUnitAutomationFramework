using TodoistTests.tests;
using Xunit;

namespace TodoistXUnitTests.tests
{
    [CollectionDefinition("Todoist collection", DisableParallelization = true)]
    public class TodoistCollection : ICollectionFixture<BaseTestFixture>
    {
    }
}
