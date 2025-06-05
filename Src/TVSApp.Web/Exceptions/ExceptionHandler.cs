using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using TVS_App.Domain.Shared;

namespace TVSApp.Web.Exceptions;

public static class ExceptionHandler
{
    public static BaseResponse<T> Handle<T>(Exception ex)
    {
        return ex switch
        {
            DbUpdateException dbEx => new BaseResponse<T>(default, 400, $"Erro ao salvar no banco: {dbEx.InnerException?.Message ?? dbEx.Message}"),
            InvalidOperationException invEx => new BaseResponse<T>(default, 400, $"Operação inválida: {invEx.Message}"),
            ArgumentNullException argEx => new BaseResponse<T>(default, 400, $"Argumento inválido: {argEx.Message}"),
            TimeoutException timeoutEx => new BaseResponse<T>(default, 408, $"Tempo de resposta do banco expirou: {timeoutEx.Message}"),
            DBConcurrencyException dbConcurrencyEx => new BaseResponse<T>(default, 409, $"Erro de concorrência: {dbConcurrencyEx.Message}"),
            DbException dbEx => new BaseResponse<T>(default, 500, $"Erro de banco de dados: {dbEx.Message}"),
            _ => new BaseResponse<T>(default, 500, $"Erro inesperado: {ex.Message}")
        };
    }
}