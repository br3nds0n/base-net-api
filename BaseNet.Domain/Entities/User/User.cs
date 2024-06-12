using BaseNet.Libs.Data.SDK.Base;

namespace BaseNet.Domain.Entities.User
{
    public class User : EntityBase
    {
        public Guid UserId { get; set; }
    }
}