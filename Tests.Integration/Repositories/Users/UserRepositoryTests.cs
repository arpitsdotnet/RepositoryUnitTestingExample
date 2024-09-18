using Core.Models;
using Data.Context;
using Data.Repositories.Users;
using Tests.Integration.TestContext;

namespace Tests.Integration.Repositories.Users;
public class UserRepositoryTests
{
    private readonly ApplicationDbContext _context;

    public UserRepositoryTests()
    {
        _context = InMemoryContextGenerator.Generate();
    }

    [Fact]
    public async Task Create_ShouldReturnUser_WhenCreatedSuccessfully()
    {
        // Arrange
        User expectedUser = CreateSingleUser();

        var repository = new UserRepository(_context);

        // Act
        var actualUser = await repository.Create(expectedUser, CancellationToken.None);
        var actualEntry = await repository.SaveChanges(CancellationToken.None);

        // Assert
        Assert.Single(_context.Users);
        Assert.Equal(expectedUser.Id, actualUser?.Id);
        Assert.Equal(expectedUser.Name, actualUser?.Name);
        Assert.Equal(1, actualEntry);
    }

    [Fact]
    public async Task Delete_ShouldDeleteUser_WhenIdIsGiven()
    {
        // Arrange
        User expectedUser = CreateSingleUser();

        var repository = new UserRepository(_context);
        await repository.Create(expectedUser, CancellationToken.None);
        await repository.SaveChanges(CancellationToken.None);

        // Act
        var actualUser = await repository.Delete(expectedUser.Id, CancellationToken.None);
        var actualEntry = await repository.SaveChanges(CancellationToken.None);

        // Assert
        Assert.Equal(0, _context.Users.Count());
        Assert.Equal(1, actualEntry);
    }

    [Fact]
    public async Task Update_ShouldUpdateUser_WhenIdIsGiven()
    {
        // Arrange
        string updatedUserName = "My Test Name";
        User expectedUser = CreateSingleUser();

        var repository = new UserRepository(_context);
        await repository.Create(expectedUser, CancellationToken.None);
        await repository.SaveChanges(CancellationToken.None);

        // Act
        var actualUser = await repository.GetById(expectedUser.Id, CancellationToken.None);

        actualUser.Name = updatedUserName;

        var actualEntry = await repository.SaveChanges(CancellationToken.None);

        // Assert
        Assert.Single(_context.Users);
        Assert.Equal(expectedUser.Id, actualUser?.Id);
        Assert.Equal(updatedUserName, actualUser?.Name);
        Assert.Equal(1, actualEntry);
    }


    [Fact]
    public async Task GetAll_ShouldReturnUsers_WhenFilterIsGiven()
    {
        // Arrange
        User[] expectedUsers = CreateMultipleUsers();

        var repository = new UserRepository(_context);
        await repository.Create(expectedUsers[0], CancellationToken.None);
        await repository.Create(expectedUsers[1], CancellationToken.None);
        await repository.Create(expectedUsers[2], CancellationToken.None);
        await repository.SaveChanges(CancellationToken.None);

        // Act
        var actualUsers = await repository.GetAll(x => x.Name.Contains("Test"), CancellationToken.None);

        // Assert
        Assert.Equal(3, actualUsers?.Count);
        Assert.Equal(expectedUsers[0].Name, actualUsers?[0].Name);
        Assert.Equal(expectedUsers[1].Name, actualUsers?[1].Name);
        Assert.Equal(expectedUsers[2].Name, actualUsers?[2].Name);
    }

    [Fact]
    public async Task GetById_ShouldReturnUser_WhenIdIsGiven()
    {
        // Arrange
        User expectedUser = CreateSingleUser();

        var repository = new UserRepository(_context);
        await repository.Create(expectedUser, CancellationToken.None);
        await repository.SaveChanges(CancellationToken.None);

        // Act
        var actualUser = await repository.GetById(expectedUser.Id, CancellationToken.None);

        // Assert
        Assert.Single(_context.Users);
        Assert.Equal(expectedUser.Id, actualUser?.Id);
        Assert.Equal(expectedUser.Name, actualUser?.Name);
        Assert.Equal(expectedUser.Email, actualUser?.Email);
        Assert.Equal(expectedUser.Mobile, actualUser?.Mobile);
    }

    [Fact]
    public async Task Get_ShouldReturnUsers_WhenFilterIsGiven()
    {
        // Arrange
        User[] expectedUsers = CreateMultipleUsers();
        
        var repository = new UserRepository(_context);
        await repository.Create(expectedUsers[0], CancellationToken.None);
        await repository.Create(expectedUsers[1], CancellationToken.None);
        await repository.Create(expectedUsers[2], CancellationToken.None);
        await repository.SaveChanges(CancellationToken.None);

        // Act
        var actualUser = await repository.Get(x => x.Name.Contains("3"), CancellationToken.None);

        // Assert
        Assert.Equal(3, _context.Users.Count());
        Assert.Equal(expectedUsers[2].Name, actualUser?.Name);
    }
    private static User CreateSingleUser()
    {
        return User.Create(Guid.NewGuid(), "Test User", "testuser@example.com", "1234567890");
    }

    private static User[] CreateMultipleUsers()
    {
        return new User[]
        {
            User.Create(Guid.NewGuid(), "Test User1", "testuser1@example.com", "1234567891"),
            User.Create(Guid.NewGuid(), "Test User2", "testuser2@example.com", "1234567892"),
            User.Create(Guid.NewGuid(), "Test User3", "testuser3@example.com", "1234567893")
        };
    }

}
