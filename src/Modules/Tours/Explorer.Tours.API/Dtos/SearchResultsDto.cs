using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class SearchResultsDto
    {
        public List<ObjectDto> Objects;
        public List<PublicKeypointDto> PublicKeyPoints;
        public List<TourDto> Tours;
    }
}
