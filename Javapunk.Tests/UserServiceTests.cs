using Javapunk.Data;
using Javapunk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
namespace Javapunk.Tests;

// Unit tests for UserService using an isolated in-memory database.
public class UserServiceTests{

 private static ApplicationDbContext CreateContext(){
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    // Positive: creates a user with a valid username.
    [Fact]
    public async Task CreateUser_ValidUsername_CreatesNewUser()
    {
        await using var context = CreateContext();
        var logger = NullLogger<UserService>.Instance;
        var service = new UserService(context, logger);

        var result = await service.CreateUser("TestUser");

        Assert.NotNull(result);
        Assert.Equal("TestUser", result.User_name);

        var savedUser = await context.Users.FirstOrDefaultAsync(u => u.User_name == "TestUser");
        Assert.NotNull(savedUser);
    }

    // Positive: initializes a new user's score to zero
    [Fact]
    public async Task CreateUser_NewUserStartsWithZeroScore()
    {
        await using var context = CreateContext();
        var logger = NullLogger<UserService>.Instance;
        var service = new UserService(context, logger);

        var result = await service.CreateUser("TestUser");

        Assert.NotNull(result);
        Assert.Equal(0, result.User_score);
    }
    
    // Positive: adds points to an existing user's score
    [Fact]
    public async Task AddScore_ExistingUser_AddsPoints(){

        await using var context = CreateContext();
        var logger = NullLogger<UserService>.Instance;
        var service = new UserService(context, logger);

        var user = new Users{User_name = "TestUser", User_score = 0};
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var result = await service.AddScoreToUserScore(user.Id,10);
        
        Assert.True(result);

        context.ChangeTracker.Clear();
        var savedUser = await context.Users.FirstAsync(u => u.Id == user.Id);
    
        Assert.Equal(10, savedUser.User_score);
    }

    // Positive: adds new points while preserving the existing score
    [Fact]
    public async Task AddScore_ExistingScore_PreservesPreviousPoints()
    {
        await using var context = CreateContext();
        var logger = NullLogger<UserService>.Instance;
        var service = new UserService(context, logger);

        var checkScore = new Users {User_name = "TestUser", User_score = 20};
        context.Users.Add(checkScore);
        await context.SaveChangesAsync();

        var addToScore = await service.AddScoreToUserScore(checkScore.Id,10);

        Assert.True(addToScore);

        context.ChangeTracker.Clear();
        var savedUser = await context.Users.FirstAsync(u => u.Id == checkScore.Id);

        Assert.Equal(30, savedUser.User_score);
    }

    // Negative: rejects a username if it already exists
    [Fact]
    public async Task CreateUser_DuplicateUsername_RejectsUser()
    {
        await using var context = CreateContext();
        var logger = NullLogger<UserService>.Instance;
        var service = new UserService(context, logger);

        var user = new Users{User_name = "TestUser"};
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var newUser = await service.CreateUser("TestUser");

        Assert.Null(newUser);
        Assert.Single(context.Users);
    }
    
    // Negative: rejects a username if it is shorter than three characters
    [Fact]
    public async Task CreateUser_UsernameTooShort_RejectsUser()
    {
        await using var context = CreateContext();
        var logger = NullLogger<UserService>.Instance;
        var service = new UserService(context, logger);

        var newUser = await service.CreateUser("hi");

        Assert.Null(newUser);
        Assert.Empty(context.Users);
    }

    // Negative: returns false when the user does not exist
    [Fact]
    public async Task AddScore_NonexistentUser_ReturnsFalse()
    {
        await using var context = CreateContext();
        var logger = NullLogger<UserService>.Instance;
        var service = new UserService(context, logger);

        var addScore = await service.AddScoreToUserScore(999,10);

        Assert.False(addScore);
        Assert.Empty(context.Users);
    }
}