using System;
using System.Collections;

namespace EC.Common
{
    public enum FormatExportReport
    {
        PDF,
        Word,
        Excel,
        HTML32,
        HTML40,
        CrystalReport,
        RTF
    }

    public enum TypeString
    {
        Text,
        Numeric,
        CNPJ,
        CPF,
        Date,
        Int,
        CEP,
        Telephone,
        Currency,
        RA
    }

    public enum TypeImage
    {
        None = 0,
        DBImage,
        UrlImage,
        PhisicalImage,
        BarCode2of5,
        ReportPDF,
        Word,
        FilePDF,
        CrystalReport,
        Excel,
        HTML32,
        HTML40
    }

    public enum StatePage
    {
        List,
        Search,
        Update
    }

    public enum TypeError
    {
        Error,
        Success
    }


    public enum BasePageMode
    {
        None,
        Search,
        SearchResult,
        Insert,
        Update,
        Delete
    }

    public enum TipoInscricaoMatricula
    {
        Curricular = 1,
        ExtraCurricular = 2,
        Equivalente = 3,
        CoRequisito = 4
    }

    public enum SelectionType
    {
        None,
        Unique,
        Multiple
    }

    public enum PessoaType
    {
        Aluno,
        Funcionario,
        Professor,
        ProfessorMestrado,
        AlunoMestrado,
        UsuarioSolicitanteProtocolo,
        Egresso,
        TodosAlunos
    }

    public enum RotinaType
    {
        AtualizacaoHistorico = 1,
        CancelamentoFaturaAbandono = 2,
        FaturamentoOVsMensal = 6,
        LiquidacaoAX = 7
    }

    public enum SolicitacaoEstagioType
    {
        AssinaturaTermoCompromissoEstagio = 1,
        TermoAditivo = 3,
        EntregaRelatorioAtividades = 4,
        TermoRecisaoEstagio = 5
    }
    public enum TipoSituacaoAlocacaoAtividadeType
    {
        Solicitada = 1,
        SolicitacaoAnalise = 2,
        SolicitacaoAnalisada = 3,
        AlocacaoConfirmada = 4,
        AlocacaoPlanejada = 5,
        AlocacaoPreparadaRealizacaoAtividade = 6,
        AtividadeRealizada = 7,
        AtividadeNaoRealizada = 8,
        Cancelada = 9,
    }

    public enum TipoProcessoSeletivoType
    {
        Pel = 1,
        Nota10 = 2,
        Vestibular = 3,
        Graduado = 4,
        Transferido = 5,
        Mestrado = 6,
        Sequencial = 7,
        Especializacao = 8,
        Colegio = 9,
        AtualizacaoEAperfeicoamento = 10,
        Extensao = 11,
        Doutorado = 12,
        MudancaDeCurso = 13,
        AtividadeExtracurricular = 14,
        AlunoEspecial = 15,
        EspecializacaoADistancia = 16,
        EnemProuni = 17,
        TransferenciaExOfficio = 18,
        TecnicoProfissionalizante = 19,
    }
}
