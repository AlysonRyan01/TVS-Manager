using TVS_App.Domain.Entities;
using TVS_App.Domain.Shared;

namespace TVS_App.Application.Interfaces;

public interface IGenerateServiceOrderPdf
{
    Task<BaseResponse<byte[]>> GenerateCheckInDocumentAsync(ServiceOrder serviceOrder);
    Task<BaseResponse<byte[]>> GenerateCheckOutDocumentAsync(ServiceOrder serviceOrder);
    Task<BaseResponse<byte[]>> GenerateSaleDocumentAsync(ServiceOrder serviceOrder);
    Task<BaseResponse<byte[]>> RegeneratePdfAsync(ServiceOrder serviceOrder);
}