defmodule TypeHero.ReportIssue do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          type: atom(),
          report: TypeHero.Report.t() | nil
        }

  schema "ReportIssue" do
    field(:type, Ecto.Enum, values: [:DEROGATORY, :OTHER, :UNCLEAR, :BULLYING, :SPAM, :HATESPEECH, :THREAT])
    field(:report_id, :id, source: :reportId)
    belongs_to(:report, TypeHero.Report, foreign_key: :report_id, define_field: false)
  end
end