﻿using Sisgea.GerarHorario.Core.Dtos.Configuracoes;
using Sisgea.GerarHorario.Core.Dtos.Entidades;

namespace Sisgea.GerarHorario.Core;

public class Program
{

    public static void Main()
    {
        // ====================================================

        var turmas = new Turma[] {
            new(
                "1",//TURMA
                "1A INFORMATICA",//NOME DA TURMA
                [
                    new Diario (Id: "diario:1_1", TurmaId: "turma:1", ProfessorId:  "1", DisciplinaId: "disciplina:1", QuantidadeMaximaSemana: 2),//SOCIOLOGIA
                    new Diario (Id: "diario:1_2", TurmaId: "turma:1", ProfessorId: "2", DisciplinaId: "disciplina:2", QuantidadeMaximaSemana: 1),//QUIMICA
                    new Diario (Id: "diario:1_3", TurmaId: "turma:1", ProfessorId: "3", DisciplinaId: "disciplina:3", QuantidadeMaximaSemana: 2),//ESPANHOL
                    new Diario (Id: "diario:1_4", TurmaId: "turma:1", ProfessorId: "4", DisciplinaId: "disciplina:4", QuantidadeMaximaSemana: 3),//PORTUGUES
                    new Diario (Id: "diario:1_5", TurmaId: "turma:1", ProfessorId: "5", DisciplinaId: "disciplina:5", QuantidadeMaximaSemana: 2),//HISTÓRIA
                    new Diario (Id: "diario:1_6", TurmaId: "turma:1", ProfessorId: "6", DisciplinaId: "disciplina:6", QuantidadeMaximaSemana: 1),//FILOSOFIA
                    new Diario (Id: "diario:1_7", TurmaId: "turma:1", ProfessorId: "7", DisciplinaId: "disciplina:7", QuantidadeMaximaSemana: 4),//REDES
                    new Diario (Id: "diario:1_8", TurmaId: "turma:1", ProfessorId: "8", DisciplinaId: "disciplina:8", QuantidadeMaximaSemana: 4),//PW
                    new Diario (Id: "diario:1_9", TurmaId: "turma:1", ProfessorId: "9", DisciplinaId: "disciplina:9", QuantidadeMaximaSemana: 3),//MATEMATICA
                    new Diario (Id: "diario:1_10", TurmaId: "turma:1", ProfessorId: "10", DisciplinaId: "disciplina:10", QuantidadeMaximaSemana: 2),//BD
                    new Diario (Id: "diario:1_11", TurmaId: "turma:1", ProfessorId: "11", DisciplinaId: "disciplina:11", QuantidadeMaximaSemana: 2),//ED. FISICA 
                    new Diario (Id: "diario:1_12", TurmaId: "turma:1", ProfessorId: "12", DisciplinaId: "disciplina:12", QuantidadeMaximaSemana: 2),//EMPREENDEDORISMO
                    new Diario (Id: "diario:1_13", TurmaId: "turma:1", ProfessorId: "13", DisciplinaId: "disciplina:13", QuantidadeMaximaSemana: 1), //SST
                    new Diario (Id: "diario:1_14", TurmaId: "turma:1", ProfessorId: "14", DisciplinaId: "disciplina:14", QuantidadeMaximaSemana: 3), //PDS
                    new Diario (Id: "diario:1_15", TurmaId: "turma:1", ProfessorId: "15", DisciplinaId: "disciplina:15", QuantidadeMaximaSemana: 1),//FISICA

                ],
                [
                    //
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "11:59:59")),//O 1A INFORMATICA TERA AULA NA SEGUNDA DAS 07:30 AS 12:00
                    //
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "11:59:59")),//O 1A INFORMATICA TERA AULA NA TERÇA DAS 07:30 AS 12:00 E DAS 13:00 AS 17:30
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("13:00", "17:29:59")),
                    //
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),//O 1A INFORMATICA TERA AULA NA QUARTA DAS 07:30 AS 12:00
                    //
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "11:59:59")),//O 1A INFORMATICA TERA AULA NA QUINTA DAS 07:30 AS 12:00 E DAS 13:00 AS 17:30
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("13:00", "17:29:59")),
                    //
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("13:00", "17:29:59")),//O 1A INFORMATICA TERA AULA NA SEXTA DAS 13:00 AS 17:30
                ]
            ),

           /* new(
                "2",
                "1B INFORMATICA",
                [
                    new Diario (Id: "diario:2_1", TurmaId: "turma:2", ProfessorId: "2", DisciplinaId: "disciplina:4", QuantidadeMaximaSemana: 1),
                    new Diario (Id: "diario:2_3", TurmaId: "turma:2", ProfessorId: "1", DisciplinaId: "disciplina:1", QuantidadeMaximaSemana: 3),
                    new Diario (Id: "diario:2_2", TurmaId: "turma:2", ProfessorId: "2", DisciplinaId: "disciplina:2", QuantidadeMaximaSemana: 2),
                ],
                [
                    //
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("13:00", "17:29:59")),//SEGUNDA DAS 13:00 AS 17:30
                    //
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "11:59:59")),//TERCA DAS 07:30 AS 12:00 E AS 13:00 AS 17:30
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("13:00", "17:29:59")),//TERCA
                    //
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),//QUARTA DAS 07:30 AS 12:00
                    //
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "11:59:59")),//QUINTA DAS 07:30 AS 12:00 E 13:00 AS 17:30
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("13:00", "17:29:59")),//QUINTA
                    //
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "11:59:59")),//SEXTA DAS 07:30 AS 12:00
                ]
            ),

             new(
                "3",
                "1 PERIODO ADS",
                [
                    new Diario (Id: "diario:3_1", TurmaId: "turma:3", ProfessorId: "2", DisciplinaId: "disciplina:4", QuantidadeMaximaSemana: 1),
                    new Diario (Id: "diario:3_3", TurmaId: "turma:3", ProfessorId: "1", DisciplinaId: "disciplina:1", QuantidadeMaximaSemana: 3),
                    new Diario (Id: "diario:3_2", TurmaId: "turma:3", ProfessorId: "2", DisciplinaId: "disciplina:2", QuantidadeMaximaSemana: 2),
                ],
                [
                    //
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("19:00", "23:29:59")),//SEGUNDA DAS 13:00 AS 17:30
                    //
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("19:00", "23:29:59")),//TERCA DAS 07:30 AS 12:00 E AS 13:00 AS 17:30
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("19:00", "23:29:59")),//TERCA
                    //
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("19:00", "23:29:59")),//QUARTA DAS 07:30 AS 12:00
                    //
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("19:00", "23:29:59")),//QUINTA DAS 07:30 AS 12:00 E 13:00 AS 17:30
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("19:00", "23:29:59")),//QUINTA
                    //
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("19:00", "23:29:59")),//SEXTA DAS 07:30 AS 12:00
                ]
            ),*/

            /* new(
                "4",
                "2 PERIODO ADS",
                [
                    new Diario (Id: "diario:4_1", TurmaId: "turma:4", ProfessorId:  "1", DisciplinaId: "disciplina:1", QuantidadeMaximaSemana: 2),//SOCIOLOGIA
                    new Diario (Id: "diario:4_2", TurmaId: "turma:4", ProfessorId: "2", DisciplinaId: "disciplina:2", QuantidadeMaximaSemana: 1),//QUIMICA
                    new Diario (Id: "diario:4_3", TurmaId: "turma:4", ProfessorId: "3", DisciplinaId: "disciplina:3", QuantidadeMaximaSemana: 2),//ESPANHOL
                    new Diario (Id: "diario:4_4", TurmaId: "turma:4", ProfessorId: "4", DisciplinaId: "disciplina:4", QuantidadeMaximaSemana: 3),//PORTUGUES
                    new Diario (Id: "diario:4_5", TurmaId: "turma:4", ProfessorId: "5", DisciplinaId: "disciplina:5", QuantidadeMaximaSemana: 2),//HISTÓRIA
                    new Diario (Id: "diario:4_6", TurmaId: "turma:4", ProfessorId: "6", DisciplinaId: "disciplina:6", QuantidadeMaximaSemana: 1),//FILOSOFIA
                    new Diario (Id: "diario:4_7", TurmaId: "turma:4", ProfessorId: "7", DisciplinaId: "disciplina:7", QuantidadeMaximaSemana: 4),//REDES
                    new Diario (Id: "diario:4_8", TurmaId: "turma:4", ProfessorId: "8", DisciplinaId: "disciplina:8", QuantidadeMaximaSemana: 4),//PW
                    new Diario (Id: "diario:4_9", TurmaId: "turma:4", ProfessorId: "9", DisciplinaId: "disciplina:9", QuantidadeMaximaSemana: 3),//MATEMATICA
                    new Diario (Id: "diario:4_10", TurmaId: "turma:4", ProfessorId: "10", DisciplinaId: "disciplina:10", QuantidadeMaximaSemana: 2),//BD
                    new Diario (Id: "diario:4_11", TurmaId: "turma:4", ProfessorId: "11", DisciplinaId: "disciplina:11", QuantidadeMaximaSemana: 2),//ED. FISICA 
                    new Diario (Id: "diario:4_12", TurmaId: "turma:4", ProfessorId: "12", DisciplinaId: "disciplina:12", QuantidadeMaximaSemana: 2),//EMPREENDEDORISMO
                    new Diario (Id: "diario:4_13", TurmaId: "turma:4", ProfessorId: "13", DisciplinaId: "disciplina:13", QuantidadeMaximaSemana: 1), //SST
                    new Diario (Id: "diario:4_14", TurmaId: "turma:4", ProfessorId: "14", DisciplinaId: "disciplina:14", QuantidadeMaximaSemana: 3), //PDS
                    new Diario (Id: "diario:4_15", TurmaId: "turma:4", ProfessorId: "15", DisciplinaId: "disciplina:15", QuantidadeMaximaSemana: 1),//FISICA
                ],
                [
                    //
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("19:00", "23:29:59")),//SEGUNDA DAS 13:00 AS 17:30
                    //
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("19:00", "23:29:59")),//TERCA DAS 07:30 AS 12:00 E AS 13:00 AS 17:30
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("19:00", "23:29:59")),//TERCA
                    //
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("19:00", "23:29:59")),//QUARTA DAS 07:30 AS 12:00
                    //
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("19:00", "23:29:59")),//QUINTA DAS 07:30 AS 12:00 E 13:00 AS 17:30
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("19:00", "23:29:59")),//QUINTA
                    //
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("19:00", "23:29:59")),//SEXTA DAS 07:30 AS 12:00
                ]
            ),*/
        };

        var professores = new Professor[] {
            new(
                "1",
                "Danilo",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("13:00", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "11:59:59")),

                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("19:00", "23:29:59")),
                ]
            ),
            new(
                "2",
                "Elias",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("19:00", "23:29:59")),
                ]
            ),
            new(
                "3",
                "João",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("19:00", "23:29:59")),
                ]
            ),
            new(
                "4",
                "Jackson",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("19:00", "23:29:59")),
                ]
            ),
            new(
                "5",
                "Adalberto",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("19:00", "23:29:59")),
                ]
            ),
            new(
                "6",
                "Beatriz",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("19:00", "23:29:59")),
                ]
            ),
            new(
                "7",
                "Carlos",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("19:00", "23:29:59")),
                ]
            ),
            new(
                "8",
                "Daniela",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("19:00", "23:29:59")),
                ]
            ),
            new(
                "9",
                "Eduardo",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("19:00", "23:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("19:00", "23:29:59")),
                ]
            ),
             new(
                "10",
                "Enzo",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                 
                ]
            ),
             new(
                "11",
                "Fernanda",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                 
                ]
            ),
            new(
                "12",
                "Gustavo",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                  
                ]
            ),
            new(
                "13",
                "Mariana",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                 
                ]
            ),
            new(
                "14",
                "Rafael",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                   
                ]
            ),
            new(
                "15",
                "Deizi",
                [
                    new DisponibilidadeDia(DiaSemanaIso.SEGUNDA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.TERCA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUARTA, new Intervalo("07:30", "11:59:59")),
                    new DisponibilidadeDia(DiaSemanaIso.QUINTA, new Intervalo("07:30", "17:29:59")),
                    new DisponibilidadeDia(DiaSemanaIso.SEXTA, new Intervalo("07:30", "17:29:59")),

                   
                ]
            ),


        };

        var horariosDeAula = new Intervalo[] {
            // =====================
            new("07:30", "08:19:59"),//0
            new("08:20", "09:09:59"),//1
            new("09:10", "09:59:59"),//2
            //
            new("10:20", "11:09:59"),//3
            new("11:10", "11:59:59"),//4
            // =====================
          //  new("12:00", "12:59"),//RECREIO 
            // =====================
            new("13:00", "13:49:59"),//5
            new("13:50", "14:39:59"),//6
            new("14:40", "15:29:59"),//7
            //
            new("15:50", "16:39:59"),//8
            new("16:40", "17:29:59"),//9
            // =====================
            new("19:00", "19:49:59"),//10
            new("19:50", "20:39:59"),//11
            new("20:40", "21:29:59"),//12
            //
            new("21:50", "22:39:59"),//13
            new("22:40", "23:29:59"),//14
        };

        var gerarHorarioOptions = new GerarHorarioOptions(
            diaSemanaInicio: DiaSemanaIso.SEGUNDA,
            diaSemanaFim: DiaSemanaIso.SEXTA,
            turmas: turmas,
            professores: professores,
            horariosDeAula: horariosDeAula
        );

        // ====================================================
        var horarioGeradoEnumerator = Gerador.GerarHorario(gerarHorarioOptions);
        // ====================================================

        var limiteGeracao = 1;
        var indiceGeracao = 0;

        foreach (var horarioGerado in horarioGeradoEnumerator)
        {

            if (indiceGeracao < limiteGeracao)
            {
                string? diaAnterior = null;

                foreach (var turma in gerarHorarioOptions.Turmas)
                {
                    Console.WriteLine($"Turma (Id={turma.Id}, Nome={turma.Nome ?? "Sem nome"})");

                    var turmaAulas = from aula in horarioGerado.Aulas
                                     where aula.TurmaId == turma.Id
                                     select aula;


                    foreach (var aula in turmaAulas)
                    {
                        string dia = Convert.ToString(aula.DiaDaSemanaIso);

                        switch (aula.DiaDaSemanaIso)
                        {
                            case 0:
                                {
                                    dia = "DOM";
                                    break;
                                }
                            case 1:
                                {
                                    dia = "SEG";
                                    break;
                                }
                            case 2:
                                {
                                    dia = "TER";
                                    break;
                                }
                            case 3:
                                {
                                    dia = "QUA";
                                    break;
                                }
                            case 4:
                                {
                                    dia = "QUI";
                                    break;
                                }
                            case 5:
                                {
                                    dia = "SEX";
                                    break;
                                }
                            case 6:
                                {
                                    dia = "SAB";
                                    break;
                                }
                        }

                        var diario = turma.DiariosDaTurma.Where(diario => diario.Id == aula.DiarioId).First();

                        if (dia != diaAnterior)
                        {
                            Console.WriteLine("");
                        }

                        Console.WriteLine($"- Dia: {dia} | Intervalo: {horariosDeAula[aula.IntervaloDeTempo]} | Professor: {diario.ProfessorId} | Diario: {diario.Id}");




                        diaAnterior = dia;
                    }
                    Console.WriteLine();

                }

                indiceGeracao++;
            }
            else
            {
                break;
            }

        }
    }
}


