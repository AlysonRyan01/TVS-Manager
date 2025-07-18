using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TVS_App.Domain.Entities;
using TVS_App.Infrastructure.Extensions;

namespace TVS_App.Infrastructure.Services.QuestPDFs;

public class GenerateCheckInDocument : QuestPdf
{
    private readonly ServiceOrder _serviceOrder;

    public GenerateCheckInDocument(ServiceOrder serviceOrder) : base(serviceOrder)
    {
        _serviceOrder = serviceOrder;
    }

    public override void Compose(IDocumentContainer document)
    {
        try
        {
            var qrCodeBytes = GenerateQrCodeService.GenerateImage($"https://www.tvseletronica.com/consultar?id={_serviceOrder.Id}&code={_serviceOrder.SecurityCode}");

            document.Page(page =>
            {
                page.Margin(8);

                page.Content().Column(column =>
                {   
                    column.Item().Element(container =>
                    {
                        container.Row(row =>
                        {
                            row.Spacing(0);
                            row.ConstantItem(400).Text("TVS ELETRÔNICA")
                                .FontSize(40)
                                .Underline()
                                .ExtraBold()
                                .FontFamily("Arial")
                                .FontColor(Colors.Red.Darken2);

                            row.RelativeItem(); 

                            row.ConstantItem(55).Column(col =>
                            {
                                col.Item().Image(qrCodeBytes);
                                
                                col.Item().Text("Consulte pelo QR Code")
                                    .FontSize(6)
                                    .Italic()
                                    .ExtraBold()
                                    .FontColor(Colors.Red.Darken2)
                                    .AlignCenter();
                            });
                        });
                    });

                    column.Item().Element(container =>
                    {
                        container.Row(row =>
                        {
                            row.Spacing(5);
                            row.ConstantItem(140).Text("WWW.TVSELETRONICA.COM")
                                .FontSize(8)
                                .ExtraBold()
                                .Underline();

                            row.ConstantItem(120).Text("FONE: (41) 3292-3047")
                                .FontSize(8)
                                .ExtraBold();

                            row.ConstantItem(200).Text("WHATSAPP: (41) 99274-4920")
                                .FontSize(8)
                                .ExtraBold();
                        });
                    });

                    column.Item().PaddingVertical(15).LineHorizontal(1).LineColor(Colors.Red.Lighten2);
                    
                    column.Item().Element(container =>
                    {
                        container.Row(row =>
                        {
                            row.ConstantItem(350).Column(col =>
                            {
                                col.Spacing(4);

                                col.Item().Text($"ORDEM DE SERVIÇO N:  {_serviceOrder.Id:00'.'000}")
                                    .FontSize(12)
                                    .ExtraBold()
                                    .FontFamily("Arial")
                                    .FontColor(Colors.Red.Darken2);

                                col.Item().Text($"EMPRESA:  {_serviceOrder.Enterprise.GetDisplayName()}")
                                    .FontSize(12)
                                    .ExtraBold()
                                    .FontFamily("Arial")
                                    .FontColor(Colors.Red.Darken2);

                                // Separador
                                col.Item().PaddingTop(10).Text("DADOS DO CLIENTE")
                                    .FontSize(10)
                                    .ExtraBold()
                                    .FontColor(Colors.Red.Darken2);

                                col.Item().Text($"Cliente:  {_serviceOrder.Customer?.Name?.CustomerName.ToUpper() ?? "-"}")
                                    .FontSize(10);

                                col.Item().Text($"Endereço:  {_serviceOrder.Customer?.Address?.Street?.ToUpper() ?? "-"}, " +
                                                $"{_serviceOrder.Customer?.Address?.Number ?? ""}, " +
                                                $"{_serviceOrder.Customer?.Address?.Neighborhood?.ToUpper() ?? "-"}")
                                    .FontSize(10);

                                col.Item().Text($"Cidade:  {_serviceOrder.Customer?.Address?.City?.ToUpper() ?? "-"}")
                                    .FontSize(10);

                                col.Item().Text($"Fone:  {_serviceOrder.Customer?.Phone.CustomerPhone?.ToUpper() ?? "-"}")
                                    .FontSize(10);

                                col.Item().Text($"Fone:  {_serviceOrder.Customer?.Phone2?.CustomerPhone.ToUpper() ?? "-"}")
                                    .FontSize(10);
                            });

                            row.ConstantItem(200).Column(col =>
                            {
                                col.Spacing(4);

                                col.Item().Text($"DATA:  {_serviceOrder.EntryDate:dd/MM/yyyy}")
                                    .FontSize(12)
                                    .ExtraBold()
                                    .FontFamily("Arial")
                                    .FontColor(Colors.Red.Darken2);

                                // Separador
                                col.Item().PaddingTop(10).Text("DADOS DO PRODUTO")
                                    .FontSize(10)
                                    .ExtraBold()
                                    .FontColor(Colors.Red.Darken2);

                                col.Item().Text($"Aparelho:  {_serviceOrder.Product?.Type.ToString().ToUpper() ?? "-"}")
                                    .FontSize(10);

                                col.Item().Text($"Marca:  {_serviceOrder.Product?.Brand?.ToUpper() ?? "-"}")
                                    .FontSize(10);

                                col.Item().Text($"Modelo:  {_serviceOrder.Product?.Model?.ToUpper() ?? "-"}")
                                    .FontSize(10);

                                col.Item().Text($"Série:  {_serviceOrder.Product?.SerialNumber?.ToUpper() ?? "-"}")
                                    .FontSize(10);

                                col.Item().Text($"Defeito:  {_serviceOrder?.Product?.Defect?.ToUpper() ?? "-"}")
                                    .FontSize(10);

                                col.Item().Text($"Acessórios:  {_serviceOrder?.Product?.Accessories?.ToUpper() ?? "-"}")
                                    .FontSize(10);
                            });
                        });
                    });

                column.Item().PaddingTop(20).LineHorizontal(1).LineColor(Colors.Red.Lighten2);

                            column.Item().Element(container =>
                            {
                                container.Row(row =>
                                {
                                    row.Spacing(0);
                                    row.ConstantItem(400).Text($"{_serviceOrder.Id.ToString("00'.'000")}")
                                        .FontSize(120)
                                        .ExtraBold()
                                        .FontColor(Colors.Red.Darken3).Justify();
                                });
                            });

                            column.Item().PaddingVertical(20).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                        });
                    });
        }
        catch (Exception ex)
        {
            throw new Exception($"Ocorreu um erro ao gerar o pdf de entrada: {ex.Message}");
        }
    }
}