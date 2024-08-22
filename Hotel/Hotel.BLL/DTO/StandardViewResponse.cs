using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public enum ViewResponseStatus
    {
        OK,
        Error
    }

    public class StandardViewResponse<T>
    {
        public ViewResponseStatus Status
        {
            get
            {
                return string.IsNullOrEmpty(ErrorMessage) ? ViewResponseStatus.OK : ViewResponseStatus.Error;
            }
        }
        public string? ErrorMessage { get; set; }
        public T Value { get; set; }
        public StandardViewResponse(T value, string? errorMessage = null)
        {
            Value = value;
            ErrorMessage = errorMessage;
        }
    }
}
