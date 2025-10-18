using System.Numerics;
using Domain.Models.Common;

namespace Domain.Models.Entities.BedModel
{
    public class Bed : BaseModel
    {
        private Vector3 _position;

        public Bed(BedDto dto)
        {
            _position = dto.Position;
        }
        public BedDto GetDto()
        {
            return new BedDto
            {
                Position = _position
            };
        }
    }
}