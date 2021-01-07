using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EcoSpider.Grpc.Models
{
    public class Base
    {
        protected IDictionary<string, string> _errors;

        public IDictionary<string, string> Errors => _errors;

        public string ErrorsList
        {
            get
            {
                return string.Join(',', _errors);
            }
        }

        public bool HasErrors()
        {
            return _errors.Count > 0;
        }

        protected Base()
        {
            _errors = new Dictionary<string, string>();
        }
    }
}