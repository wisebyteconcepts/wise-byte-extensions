using WiseByteExtensions.Core.Validators;

using Xunit;

namespace WiseByteExtensions.Tests.Validators;

public class DirectoryValidatorTests
{
    // Utility: create a temporary test directory
    private string CreateTempDirectory()
    {
        var dir = Path.Combine(Path.GetTempPath(), "WBETest_" + Guid.NewGuid());
        Directory.CreateDirectory(dir);
        return dir;
    }

    // Utility: create a restricted simulated path
    private string FakeSystemPath(string sub)
        => Path.Combine("C:\\Windows", sub);


    // -----------------------------
    // IsValidDirectoryString
    // -----------------------------
    [Fact]
    public void IsValidDirectoryString_ShouldReturnFalse_OnNullOrEmpty()
    {
        Assert.False(DirectoryValidator.IsValidDirectoryString(null));
        Assert.False(DirectoryValidator.IsValidDirectoryString(""));
    }

    [Fact]
    public void IsValidDirectoryString_ShouldReturnFalse_OnInvalidCharacters()
    {
        Assert.False(DirectoryValidator.IsValidDirectoryString("C:\\Test<Folder>"));
        Assert.False(DirectoryValidator.IsValidDirectoryString("C:\\Te|st"));
    }

    [Fact]
    public void IsValidDirectoryString_ShouldReturnTrue_OnValidPath()
    {
        Assert.True(DirectoryValidator.IsValidDirectoryString("C:\\ValidFolder"));
    }



    // -----------------------------
    // DirectoryExists
    // -----------------------------
    [Fact]
    public void DirectoryExists_ShouldReturnTrue_WhenDirectoryExists()
    {
        var dir = CreateTempDirectory();
        Assert.True(DirectoryValidator.DirectoryExists(dir));
    }

    [Fact]
    public void DirectoryExists_ShouldReturnFalse_WhenDirectoryDoesNotExist()
    {
        var dir = Path.Combine(Path.GetTempPath(), "NoSuchDir_" + Guid.NewGuid());
        Assert.False(DirectoryValidator.DirectoryExists(dir));
    }



    // -----------------------------
    // IsDirectoryWritable
    // -----------------------------
    [Fact]
    public void IsDirectoryWritable_ShouldReturnTrue_OnWritableDirectory()
    {
        var dir = CreateTempDirectory();
        Assert.True(DirectoryValidator.IsDirectoryWritable(dir));
    }

    [Fact]
    public void IsDirectoryWritable_ShouldReturnFalse_OnNonExistingDirectory()
    {
        var dir = Path.Combine(Path.GetTempPath(), "DoesNotExist_" + Guid.NewGuid());
        Assert.False(DirectoryValidator.IsDirectoryWritable(dir));
    }



    // -----------------------------
    // CanCreateDirectory
    // -----------------------------
    [Fact]
    public void CanCreateDirectory_ShouldReturnTrue_WhenCanCreateNewDirectory()
    {
        var parent = CreateTempDirectory();
        var newDir = Path.Combine(parent, "CreateHere");
        Assert.True(DirectoryValidator.CanCreateDirectory(newDir));
    }

    [Fact]
    public void CanCreateDirectory_ShouldReturnFalse_WhenParentDoesNotExist()
    {
        var invalidDir = Path.Combine("Z:\\NoSuchDrive", "Folder");
        Assert.False(DirectoryValidator.CanCreateDirectory(invalidDir));
    }



    // -----------------------------
    // IsUsableDirectory
    // -----------------------------
    [Fact]
    public void IsUsableDirectory_ShouldReturnTrue_OnValidWritableDirectory()
    {
        var dir = CreateTempDirectory();
        Assert.True(DirectoryValidator.IsUsableDirectory(dir));
    }

    [Fact]
    public void IsUsableDirectory_ShouldReturnFalse_OnInvalidString()
    {
        Assert.False(DirectoryValidator.IsUsableDirectory("C:\\Bad|Path"));
    }

    [Fact]
    public void IsUsableDirectory_ShouldReturnFalse_OnRestrictedSystemDirectory()
    {
        var sysDir = FakeSystemPath("System32");
        Assert.False(DirectoryValidator.IsUsableDirectory(sysDir));
    }



    // -----------------------------
    // IsRestrictedSystemPath
    // -----------------------------
    [Theory]
    [InlineData("C:\\Windows\\System32")]
    [InlineData("C:\\Program Files\\Test")]
    [InlineData("C:\\ProgramData\\Something")]
    public void IsRestrictedSystemPath_ShouldIdentifyRestrictedPaths(string path)
    {
        Assert.True(DirectoryValidator.IsRestrictedSystemPath(path));
    }

    [Fact]
    public void IsRestrictedSystemPath_ShouldReturnFalse_OnNormalPath()
    {
        var dir = CreateTempDirectory();
        Assert.False(DirectoryValidator.IsRestrictedSystemPath(dir));
    }



    // -----------------------------
    // IsValidDatabaseFilePath
    // -----------------------------
    [Fact]
    public void IsValidDatabaseFilePath_ShouldReturnTrue_OnValidDbPath()
    {
        var dir = CreateTempDirectory();
        var db = Path.Combine(dir, "test.db");
        Assert.True(DirectoryValidator.IsValidDatabaseFilePath(db));
    }

    [Fact]
    public void IsValidDatabaseFilePath_ShouldReturnFalse_OnWrongExtension()
    {
        var dir = CreateTempDirectory();
        var f = Path.Combine(dir, "test.txt");
        Assert.False(DirectoryValidator.IsValidDatabaseFilePath(f));
    }

    [Fact]
    public void IsValidDatabaseFilePath_ShouldReturnFalse_WhenParentDirectoryInvalid()
    {
        var invalidParent = "C:\\Invalid|Folder";
        var db = Path.Combine(invalidParent, "x.db");
        Assert.False(DirectoryValidator.IsValidDatabaseFilePath(db));
    }
}
