using System;
using BackendAPICrud.Data;
using BackendAPICrud.Estudantes;
using BackendAPICrud.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

// Context metodos.
// AddAsync: Adiciona uma nova entidade ao contexto. 
// FindAsync() : Busca uma entidade no banco de dados com base na chave primária.
// Remove() : Marca uma entidade para ser excluída do banco de dados.

public static class EstudantesRouter
{
    public static void AddRouterEstudantes(this WebApplication app)
    {
        var rotasEstudantes = app.MapGroup("estudantes");

        // Rota para salva os estudantes no Mysql.
        rotasEstudantes.MapPost("post", async (RecordEstudantes.AddEstudantesRequest request , AppDBContext context) => 
        { 
            try{

                var VerificarEstudantes = await context.Estudante.AnyAsync( estudante => estudante.Nome == request.Nome);

                if (VerificarEstudantes){
                    return Results.Conflict("Ja existe esse nome salvo");
                }
                var estudante = new Estudante(request.Nome);

                await context.Estudante.AddAsync(estudante);
                await context.SaveChangesAsync();

                var estudanteReturn = new EstudanteDTO(estudante.id, estudante.Nome);

                return Results.Ok(estudanteReturn);

            }catch (Exception ex){
                return Results.BadRequest(ex.Message);
            }

        });

        // Buscar todos os estudantes ativo.
        rotasEstudantes.MapGet("buscar", async (AppDBContext context) => {
            var SelectEstudante = await context.Estudante
                .Where(estudante => estudante.Ativo)
                .ToListAsync();
            return Results.Ok(SelectEstudante);
        });
        // Buscar estudante por id
        rotasEstudantes.MapGet("buscar/{id:guid}", async (Guid id, AppDBContext context) => {
            var SelectEstudanteID = await context.Estudante.FindAsync(id);
            if (SelectEstudanteID == null){
                    return Results.NotFound();
            }
            var ReturnSelectEstudanteID = new EstudanteDTO(SelectEstudanteID.id, SelectEstudanteID.Nome);
            return Results.Ok(ReturnSelectEstudanteID);
        });
        // rota para atualizar
        rotasEstudantes.MapPut("atualizar/{id:guid}", async (Guid id, RecordEstudantes.UpdateEstudanteRequest request ,AppDBContext context) => {
            try{
                var estudante = await context.Estudante.SingleOrDefaultAsync(estudante => estudante.id == id);

                if (estudante == null){
                    return Results.NotFound();
                }
                estudante.AtualizarNome(request.Nome);
                await context.SaveChangesAsync(); // Salva todas as alterações feitas no context no banco de dados.

                var estudanteReturn = new EstudanteDTO(estudante.id, estudante.Nome);

                return Results.Ok(estudanteReturn);
            }catch (Exception ex) {
                return Results.BadRequest(ex.Message);
            }
        });
        // rota para deletar
        rotasEstudantes.MapDelete("delete/{id:guid}", async (Guid id, AppDBContext context) => {
            try{
                var estudante = await context.Estudante.FindAsync(id);
                if (!(estudante == null)){
                    context.Estudante.Remove(estudante);
                    await context.SaveChangesAsync();
                    return Results.Ok("Aluno excluido");
                }
                return Results.NotFound();
            }catch (Exception ex){
                return Results.BadRequest(ex.Message);
            }
        });
    }
}
