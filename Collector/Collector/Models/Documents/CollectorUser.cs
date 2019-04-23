using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.Documents
{
    public class CollectorUser : MongoDbGenericRepository.Models.Document
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsOrganizationAdmin { get; set; }
        public List<string> OrganizationRoles { get; set; }
    }
}
