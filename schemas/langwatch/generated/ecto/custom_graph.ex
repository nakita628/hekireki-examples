defmodule LangWatch.CustomGraph do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          graph: map(),
          filters: map() | nil,
          kind: String.t(),
          grid_column: integer(),
          grid_row: integer(),
          col_span: integer(),
          row_span: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          dashboard: LangWatch.Dashboard.t() | nil,
          trigger: LangWatch.Trigger.t() | nil,
          trigger_sent: [LangWatch.TriggerSent.t()]
        }

  schema "CustomGraph" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:graph, :map)
    field(:filters, :map)
    field(:kind, :string, default: "builder")
    field(:grid_column, :integer, default: 0, source: :gridColumn)
    field(:grid_row, :integer, default: 0, source: :gridRow)
    field(:col_span, :integer, default: 1, source: :colSpan)
    field(:row_span, :integer, default: 1, source: :rowSpan)
    field(:project_id, :string, source: :projectId)
    field(:dashboard_id, :string, source: :dashboardId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:dashboard, LangWatch.Dashboard, foreign_key: :dashboard_id, define_field: false, type: :string)
    has_one(:trigger, LangWatch.Trigger, foreign_key: :custom_graph_id, references: :id)
    has_many(:trigger_sent, LangWatch.TriggerSent, foreign_key: :custom_graph_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end