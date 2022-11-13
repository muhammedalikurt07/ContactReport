using Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class GeneralResponse
    {
        public GeneralResponse()
        {

        }

        public GeneralResponse(ResultCode _code, string _message, object _data = null)
        {
            _Code = _code;
            _Message = _message;
            _Data = _data;
        }
        public ResultCode _Code { get; set; }
        public string _Message { get; set; }
        public object _Data { get; set; }

        public void Update(ResultCode code, string message, object data = null)
        {
            this._Code = code;
            this._Message = message;
            this._Data = data;
        }
    }
}
