using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common;
public class Result<T>
{
    private Result(bool isSuccess, T? value)
    {
        IsSuccess = isSuccess;
        Value = value;
    }

    public bool IsSuccess { get; }
    public T? Value { get; }

    public static Result<T> Success(T value) => new(true, value);
    public static Result<T> Failure() => new(false, default);
}
