defmodule OpenPanel.Report do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          interval: atom(),
          range: String.t(),
          chart_type: atom(),
          line_type: String.t(),
          breakdowns: map(),
          events: map(),
          global_filters: map(),
          formula: String.t() | nil,
          unit: String.t() | nil,
          metric: atom(),
          previous: boolean(),
          criteria: String.t() | nil,
          funnel_group: String.t() | nil,
          funnel_window: float() | nil,
          options: map() | nil,
          display: map() | nil,
          visible_series: [String.t()],
          start_date: String.t() | nil,
          end_date: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: OpenPanel.Project.t() | nil,
          dashboard: OpenPanel.Dashboard.t() | nil,
          layout: OpenPanel.ReportLayout.t() | nil,
          share: OpenPanel.ShareReport.t() | nil
        }

  schema "reports" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:name, :string)
    field(:interval, Ecto.Enum, values: [:hour, :day, :month, :minute, :week])
    field(:range, :string, default: "30d")
    field(:chart_type, Ecto.Enum, values: [:linear, :bar, :histogram, :pie, :metric, :area, :map, :funnel, :retention, :conversion, :sankey, :distribution], source: :chartType)
    field(:line_type, :string, default: "monotone", source: :lineType)
    field(:breakdowns, :map)
    field(:events, :map)
    field(:global_filters, :map, source: :globalFilters)
    field(:formula, :string)
    field(:unit, :string)
    field(:metric, Ecto.Enum, values: [:sum, :average, :min, :max, :count], default: :sum)
    field(:previous, :boolean, default: false)
    field(:criteria, :string)
    field(:funnel_group, :string, source: :funnelGroup)
    field(:funnel_window, :float, source: :funnelWindow)
    field(:options, :map)
    field(:display, :map)
    field(:visible_series, {:array, :string}, source: :visibleSeries)
    field(:start_date, :string, source: :startDate)
    field(:end_date, :string, source: :endDate)
    field(:project_id, :string, source: :projectId)
    field(:dashboard_id, :string, source: :dashboardId)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:dashboard, OpenPanel.Dashboard, foreign_key: :dashboard_id, define_field: false, type: :string)
    has_one(:layout, OpenPanel.ReportLayout, foreign_key: :report_id, references: :id)
    has_one(:share, OpenPanel.ShareReport, foreign_key: :report_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end