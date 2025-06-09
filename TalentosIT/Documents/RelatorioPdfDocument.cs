using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TalentosIT.ViewModels;

namespace TalentosIT.Documents
{
    public class RelatorioPdfDocument : IDocument
    {
        private readonly RelatorioViewModel _model;

        public RelatorioPdfDocument(RelatorioViewModel model)
        {
            _model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Header().Text("Relatório TalentosIT").FontSize(20).Bold().AlignCenter();
                page.Content().PaddingVertical(10).Column(column =>
                {
                    column.Spacing(20);

                    // Tabela por Categoria e País
                    column.Item().Text("Relatório por Categoria e País").Bold().FontSize(14);
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn();
                            c.RelativeColumn();
                            c.ConstantColumn(100);
                        });

                        table.Header(h =>
                        {
                            h.Cell().Text("Categoria").Bold();
                            h.Cell().Text("País").Bold();
                            h.Cell().Text("Preço Médio (€)").Bold();
                        });

                        foreach (var item in _model.PorCategoriaPais)
                        {
                            table.Cell().Text(item.Categoria);
                            table.Cell().Text(item.Pais);
                            table.Cell().Text(item.PrecoMedio.ToString("F2"));
                        }
                    });

                    // Tabela por Skill
                    column.Item().Text("Relatório por Skill").Bold().FontSize(14);
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn();     // Skill
                            c.ConstantColumn(100);  // Preço Médio
                            c.ConstantColumn(120);  // Preço Mensal
                        });

                        table.Header(h =>
                        {
                            h.Cell().Text("Skill").Bold();
                            h.Cell().Text("Preço Médio (€)").Bold();
                            h.Cell().Text("Preço Mensal Estimado (€)").Bold();
                        });

                        foreach (var item in _model.PorSkill)
                        {
                            table.Cell().Text(item.Skill);
                            table.Cell().Text(item.PrecoMedio.ToString("F2"));
                            table.Cell().Text(item.PrecoMensalEstimado.ToString("F2"));
                        }
                    });
                });
                page.Footer().AlignCenter().Text(txt =>
                {
                    txt.Span("Página ");
                    txt.CurrentPageNumber();
                    txt.Span(" de ");
                    txt.TotalPages();
                });
            });
        }
    }
}
