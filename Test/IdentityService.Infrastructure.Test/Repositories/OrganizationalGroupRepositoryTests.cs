using System;
using System.Linq;
using System.Threading.Tasks;

using Inplanticular.IdentityService.Core.V1.Entities;
using Inplanticular.IdentityService.Core.V1.Services;
using Inplanticular.IdentityService.Infrastructure.Test.TestUtils;
using Inplanticular.IdentityService.Infrastructure.V1.Database;
using Inplanticular.IdentityService.Infrastructure.V1.Database.Models;
using Inplanticular.IdentityService.Infrastructure.V1.Repositories;
using Inplanticular.IdentityService.Infrastructure.V1.Services;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Inplanticular.IdentityService.Infrastructure.Test.Repositories;

public class OrganizationalGroupRepositoryTests : IAsyncDisposable {
	private readonly ApplicationDbContext _dbContext;
	private readonly IMappingService _mappingService;
	private readonly OrganizationalGroupRepository _sut;

	public OrganizationalGroupRepositoryTests() {
		this._dbContext = InMemoryDbContextFactory.CreateNewAsync<ApplicationDbContext>().GetAwaiter().GetResult();
		this._mappingService = new MappingService();
		this._sut = new OrganizationalGroupRepository(this._dbContext, this._mappingService);
	}

	public async ValueTask DisposeAsync() {
		await this._dbContext.Database.CloseConnectionAsync();
		await this._dbContext.DisposeAsync();
	}

	#region Methode: AddGroupAsync

	[Fact]
	public async Task AddGroupAsync_ShouldAddGroupSuccessfully_WhenGroupDoesNotExist() {
		// Arrange
		this._dbContext.OrganizationalGroups.Add(new OrganizationalGroupModel {Name = "OtherGroup"});
		await this._dbContext.SaveChangesAsync();
		
		// Act
		await this._sut.AddGroupAsync(new OrganizationalGroup {Name = "TestGroup"});
		
		// Assert
		var groupNames = await this._dbContext.OrganizationalGroups.Select(group => group.Name).ToListAsync();
		Assert.Equal(2, groupNames.Count);
		Assert.Equal(groupNames.Count, groupNames.Intersect(new[] {"TestGroup", "OtherGroup"}).Count());
	}
	
	[Fact]
	public async Task AddGroupAsync_ShouldThrowUpdateException_WhenGroupAlreadyExists() {
		// Arrange
		this._dbContext.OrganizationalGroups.Add(new OrganizationalGroupModel {Name = "TestGroup"});
		await this._dbContext.SaveChangesAsync();
		
		// Act + Assert
		await Assert.ThrowsAsync<DbUpdateException>(() => this._sut.AddGroupAsync(new OrganizationalGroup {Name = "TestGroup"}));
		Assert.Single(await this._dbContext.OrganizationalGroups.ToListAsync());
	}
	
	#endregion
	
	#region Methode: RemoveGroupAsync

	[Fact]
	public async Task RemoveGroupAsync_ShouldRemoveGroup_WhenGroupIdExists() {
		// Arrange
		var group = new OrganizationalGroupModel {Name = "TestGroup"};
		this._dbContext.OrganizationalGroups.Add(group);
		await this._dbContext.SaveChangesAsync();
		
		// Act
		await this._sut.RemoveGroupAsync(new OrganizationalGroup() {Id = group.Id, Name = "NameShouldNotMatter"});
		
		// Assert
		Assert.Empty(await this._dbContext.OrganizationalGroups.ToListAsync());
	}
	
	[Fact]
	public async Task RemoveGroupAsync_ShouldThrowArgumentNullException_WhenGroupIdDoesNotExist() {
		// The test should throw an ArgumentNullException because the repository tries to get the tracked entity but will not find it.
		// So it tries to remove null.
		
		// Arrange
		var group = new OrganizationalGroupModel {Name = "TestGroup"};
		this._dbContext.OrganizationalGroups.Add(group);
		await this._dbContext.SaveChangesAsync();
		
		// Act + Assert
		await Assert.ThrowsAsync<ArgumentNullException>(() => this._sut.RemoveGroupAsync(new OrganizationalGroup() {Id = "InvalidId", Name = "NameShouldNotMatter"}));
		Assert.Single(await this._dbContext.OrganizationalGroups.ToListAsync());
	}
	
	#endregion
	
	#region Methode: UpdateGroupAsync

	[Fact]
	public async Task UpdateGroupAsync_ShouldUpdateGroupSuccessfully_WhenGroupExistsAndNewNameIsNotInUse() {
		// Arrange
		var group = new OrganizationalGroupModel {Name = "TestGroup"};
		this._dbContext.OrganizationalGroups.Add(group);
		await this._dbContext.SaveChangesAsync();
		
		// Act
		var result = await this._sut.UpdateGroupAsync(new OrganizationalGroup {Id = group.Id, Name = "NewGroupName"});
		
		// Assert
		Assert.True(result);

		var newGroup = await this._dbContext.OrganizationalGroups.SingleAsync(g => g.Id.Equals(group.Id));
		Assert.Equal("NewGroupName", newGroup.Name);
	}
	
	[Fact]
	public async Task UpdateGroupAsync_ShouldReturnFalse_WhenGroupDoesNotExist() {
		// Arrange
		var group = new OrganizationalGroupModel {Name = "TestGroup"};
		this._dbContext.OrganizationalGroups.Add(group);
		await this._dbContext.SaveChangesAsync();
		
		// Act
		var result = await this._sut.UpdateGroupAsync(new OrganizationalGroup {Id = "InvalidId", Name = "NewGroupName"});
		
		// Assert
		Assert.False(result);
	}
	
	[Fact]
	public async Task UpdateGroupAsync_ShouldReturnFalse_WhenNewGroupNameAlreadyExists() {
		// Arrange
		var group = new OrganizationalGroupModel {Name = "TestGroup"};
		this._dbContext.OrganizationalGroups.AddRange(group, new OrganizationalGroupModel() { Name = "ExistingGroupName" });
		await this._dbContext.SaveChangesAsync();
		
		// Act + Assert
		await Assert.ThrowsAsync<DbUpdateException>(() => this._sut.UpdateGroupAsync(new OrganizationalGroup {Id = group.Id, Name = "ExistingGroupName"}));

		// Group should be unchanged
		var unchangedGroup = await this._dbContext.OrganizationalGroups.AsNoTracking().FirstOrDefaultAsync(g => g.Id.Equals(group.Id));
		Assert.Equal("TestGroup", unchangedGroup!.Name);
	}

	#endregion
	
	#region Methode: UpdateGroupAsync - Callback
	
	[Fact]
	public async Task UpdateGroupAsync_UpdaterOverload_ShouldUpdateGroupSuccessfully_WhenGroupExistsAndNewNameIsNotInUse() {
		// Arrange
		var group = new OrganizationalGroupModel {Name = "TestGroup"};
		this._dbContext.OrganizationalGroups.Add(group);
		await this._dbContext.SaveChangesAsync();
		
		// Act
		var result = await this._sut.UpdateGroupAsync(group.Id, g => g.Name = "NewGroupName");
		
		// Assert
		Assert.True(result);

		var newGroup = await this._dbContext.OrganizationalGroups.SingleAsync(g => g.Id.Equals(group.Id));
		Assert.Equal("NewGroupName", newGroup.Name);
	}
	
	[Fact]
	public async Task UpdateGroupAsync_UpdaterOverload_ShouldReturnFalse_WhenGroupDoesNotExist() {
		// Arrange
		var group = new OrganizationalGroupModel {Name = "TestGroup"};
		this._dbContext.OrganizationalGroups.Add(group);
		await this._dbContext.SaveChangesAsync();
		
		// Act
		var result = await this._sut.UpdateGroupAsync("InvalidId", g => g.Name = "NewGroupName");
		
		// Assert
		Assert.False(result);
	}
	
	[Fact]
	public async Task UpdateGroupAsync_UpdaterOverload_ShouldReturnFalse_WhenNewGroupNameAlreadyExists() {
		// Arrange
		var group = new OrganizationalGroupModel {Name = "TestGroup"};
		this._dbContext.OrganizationalGroups.AddRange(group, new OrganizationalGroupModel() { Name = "ExistingGroupName" });
		await this._dbContext.SaveChangesAsync();
		
		// Act + Assert
		await Assert.ThrowsAsync<DbUpdateException>(() => this._sut.UpdateGroupAsync(group.Id, g => g.Name = "ExistingGroupName"));

		// Group should be unchanged
		var unchangedGroup = await this._dbContext.OrganizationalGroups.AsNoTracking().FirstOrDefaultAsync(g => g.Id.Equals(group.Id));
		Assert.Equal("TestGroup", unchangedGroup!.Name);
	}
	
	#endregion
	
	#region Methode: FindGroupByIdAsync

	[Fact]
	public async Task FindGroupByIdAsync_ShouldFindGroup_WhenGroupIdExists() {
		// Arrange
		var group = new OrganizationalGroupModel {Name = "TestGroup"};
		this._dbContext.OrganizationalGroups.Add(group);
		await this._dbContext.SaveChangesAsync();

		// Act
		var result = await this._sut.FindGroupByIdAsync(group.Id);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(group.Id, result!.Id);
		Assert.Equal(group.Name, result!.Name);
	}
	
	[Fact]
	public async Task FindGroupByIdAsync_ShouldReturnNull_WhenGroupIdDoesNotExist() {
		// Arrange
		var group = new OrganizationalGroupModel {Name = "TestGroup"};
		this._dbContext.OrganizationalGroups.Add(group);
		await this._dbContext.SaveChangesAsync();

		// Act
		var result = await this._sut.FindGroupByIdAsync("InvalidId");

		// Assert
		Assert.Null(result);
	}
	
	#endregion
	
	#region Methode: FindGroupByNameAsync
	
	[Fact]
	public async Task FindGroupByNameAsync_ShouldFindGroup_WhenGroupNameExists() {
		// Arrange
		var group = new OrganizationalGroupModel {Name = "TestGroup"};
		this._dbContext.OrganizationalGroups.Add(group);
		await this._dbContext.SaveChangesAsync();

		// Act
		var result = await this._sut.FindGroupByNameAsync(group.Name);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(group.Id, result!.Id);
		Assert.Equal(group.Name, result!.Name);
	}
	
	[Fact]
	public async Task FindGroupByNameAsync_ShouldReturnNull_WhenGroupNameDoesNotExist() {
		// Arrange
		var group = new OrganizationalGroupModel {Name = "TestGroup"};
		this._dbContext.OrganizationalGroups.Add(group);
		await this._dbContext.SaveChangesAsync();

		// Act
		var result = await this._sut.FindGroupByNameAsync("OtherGroup");

		// Assert
		Assert.Null(result);
	}
	
	#endregion
}