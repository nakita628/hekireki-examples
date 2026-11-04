defmodule Langfuse.TopicClusteringRun do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          facet_id: String.t(),
          facet_version: integer(),
          status: String.t(),
          config: map(),
          error: String.t() | nil,
          started_at: DateTime.t() | nil,
          finished_at: DateTime.t() | nil,
          topic_version_ids: [String.t()],
          rule_id: String.t() | nil,
          batch_action_id: String.t() | nil,
          inserted_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "topic_clustering_runs" do
    field(:id, :string, primary_key: true)
    field(:facet_id, :string)
    field(:facet_version, :integer)
    field(:status, :string, default: "pending")
    field(:config, :map, default: %{})
    field(:error, :string)
    field(:started_at, Langfuse.PrismaDateTime)
    field(:finished_at, Langfuse.PrismaDateTime)
    field(:topic_version_ids, {:array, :string})
    field(:rule_id, :string)
    field(:batch_action_id, :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end