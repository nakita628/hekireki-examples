defmodule LangWatch.DashboardWidget do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          dashboard_id: String.t() | nil,
          name: String.t(),
          definition: map(),
          grid_column: integer(),
          grid_row: integer(),
          col_span: integer(),
          row_span: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "DashboardWidget" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:dashboard_id, :string, source: :dashboardId)
    field(:name, :string)
    field(:definition, :map)
    field(:grid_column, :integer, default: 0, source: :gridColumn)
    field(:grid_row, :integer, default: 0, source: :gridRow)
    field(:col_span, :integer, default: 1, source: :colSpan)
    field(:row_span, :integer, default: 1, source: :rowSpan)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end