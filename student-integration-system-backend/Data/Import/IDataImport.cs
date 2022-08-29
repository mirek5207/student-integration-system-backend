using Microsoft.EntityFrameworkCore;

namespace student_integration_system_backend.Data.Import;

public interface IDataImport
{
    void Seed(ModelBuilder builder);
}