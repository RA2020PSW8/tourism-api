using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IPersonRepository: ICrudRepository<Person>
    {
        Person GetFullProfile(long personId);
        bool Exists(long personId);
    }
}
