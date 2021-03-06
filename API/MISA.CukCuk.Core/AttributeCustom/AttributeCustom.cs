using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.CukCuk.Core.AttributeCustom
{
    /// <summary>
    /// Trường để kiểm tra xem property có bị bỏ trống không
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredField:Attribute
    {
        public string MsgError = string.Empty;
        public RequiredField(string msgError = "")
        {
            MsgError = msgError;
        }
    }
    /// <summary>
    /// Trường kiểm tra email có hợp lệ không
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailField : Attribute
    {
    }
    /// <summary>
    /// Trường kiểm tra số điện thoại có hợp lệ không
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneNumberField : Attribute
    {
    }
    /// <summary>
    /// Trường kiểm tra xem property có quá số lượng ký tự cho phép không
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLengthField : Attribute
    {
        public int MaxLength = 20;
        public string MsgError = string.Empty;
        //public MaxLengthField(int _maxLength = 0, string msg = "")
        //{
        //    MaxLength = _maxLength;
        //    MsgError = msg;
        //}
    }

}
