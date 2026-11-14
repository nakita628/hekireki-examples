defmodule LangWatch.TriggerSent do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          trace_id: String.t() | nil,
          resolved_at: DateTime.t() | nil,
          open_incident_key: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          custom_graph: LangWatch.CustomGraph.t() | nil,
          project: LangWatch.Project.t() | nil,
          trigger: LangWatch.Trigger.t() | nil
        }

  schema "TriggerSent" do
    field(:id, :string, primary_key: true)
    field(:trace_id, :string, source: :traceId)
    field(:resolved_at, LangWatch.PrismaDateTime, source: :resolvedAt)
    field(:open_incident_key, :string, source: :openIncidentKey)
    field(:custom_graph_id, :string, source: :customGraphId)
    field(:project_id, :string, source: :projectId)
    field(:trigger_id, :string, source: :triggerId)
    belongs_to(:custom_graph, LangWatch.CustomGraph, foreign_key: :custom_graph_id, define_field: false, type: :string)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:trigger, LangWatch.Trigger, foreign_key: :trigger_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end