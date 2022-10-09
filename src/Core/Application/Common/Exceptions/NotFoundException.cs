using System.Net;

namespace Application.Common.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message = default!)
            : base(message, null, HttpStatusCode.NotFound)
        {
        }
    }
}
