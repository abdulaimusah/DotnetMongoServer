
using DotnetMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotnetMongo.Services;

public class CompanyService 
{
    private readonly IMongoCollection<Company> _companyCollection;

    public CompanyService(
        IOptions<CompanyDBSettings> companyDBSettings
    ) {
        var mongoClient = new MongoClient(
            companyDBSettings.Value.ConnectionString
        );

        var mongoDatabase = mongoClient.GetDatabase(
            companyDBSettings.Value.DatabaseName
        );

        _companyCollection = mongoDatabase.GetCollection<Company>(
            companyDBSettings.Value.CollectionName
        );
    }

    public async Task<List<Company>> GetAsync() => 
        await _companyCollection.Find(_ => true).ToListAsync();
    
    public async Task<Company?> GetAsync(string id) => 
        await _companyCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Company newCompnay) => 
        await _companyCollection.InsertOneAsync(newCompnay);

    public async Task UpdateAsync(string id, 
       Company updatedCompnay) => 
       await _companyCollection.ReplaceOneAsync(x => x.Id == id,
       updatedCompnay);

    public async Task RemoveAsync(string id) => 
       await _companyCollection.DeleteOneAsync(x => x.Id == id);
}