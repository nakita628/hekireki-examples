defmodule Langfuse.DashboardWidget do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t(),
          view: atom(),
          dimensions: map(),
          metrics: map(),
          filters: map(),
          chart_type: atom(),
          chart_config: map(),
          min_version: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_by_user: Langfuse.User.t() | nil,
          updated_by_user: Langfuse.User.t() | nil,
          project: Langfuse.Project.t() | nil
        }

  schema "dashboard_widgets" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:view, Ecto.Enum, values: [:TRACES, :OBSERVATIONS, :SCORES_NUMERIC, :SCORES_CATEGORICAL, :SCORES_BOOLEAN])
    field(:dimensions, :map)
    field(:metrics, :map)
    field(:filters, :map)
    field(:chart_type, Ecto.Enum, values: [:LINE_TIME_SERIES, :AREA_TIME_SERIES, :BAR_TIME_SERIES, :HORIZONTAL_BAR, :VERTICAL_BAR, :PIE, :NUMBER, :HISTOGRAM, :PIVOT_TABLE])
    field(:chart_config, :map)
    field(:min_version, :integer, default: 1)
    belongs_to(:created_by_user, Langfuse.User, foreign_key: :created_by, type: :string)
    belongs_to(:updated_by_user, Langfuse.User, foreign_key: :updated_by, type: :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end