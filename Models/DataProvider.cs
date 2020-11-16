namespace Trippy_Land.Models
{
    public static class DataProvider
    {
        /// <summary>
        /// Khai báo 1 thuộc tính để truy xuất và làm việc với db qua EF
        /// </summary>
        private static Trippy_Land_Context _Entities = null;

        public static Trippy_Land_Context Entities
        {
            get
            {
                if (_Entities == null)
                {
                    _Entities = new Trippy_Land_Context();
                }
                return _Entities;
            }
            set
            {
                _Entities = value;
            }
        }
    }
}