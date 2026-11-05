defmodule OpenPanel.ReportLayout do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          grid_columns: integer(),
          x: integer(),
          y: integer(),
          w: integer(),
          h: integer(),
          min_w: integer() | nil,
          min_h: integer() | nil,
          max_w: integer() | nil,
          max_h: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          report: OpenPanel.Report.t() | nil
        }

  schema "report_layouts" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:grid_columns, :integer, default: 12, source: :gridColumns)
    field(:x, :integer, default: 0)
    field(:y, :integer, default: 0)
    field(:w, :integer, default: 8)
    field(:h, :integer, default: 6)
    field(:min_w, :integer, default: 4, source: :minW)
    field(:min_h, :integer, default: 4, source: :minH)
    field(:max_w, :integer, source: :maxW)
    field(:max_h, :integer, source: :maxH)
    field(:report_id, :string, source: :reportId)
    belongs_to(:report, OpenPanel.Report, foreign_key: :report_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end