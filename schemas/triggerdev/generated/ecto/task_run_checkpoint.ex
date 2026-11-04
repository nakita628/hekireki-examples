defmodule Trigger.TaskRunCheckpoint do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          type: atom(),
          location: String.t(),
          image_ref: String.t() | nil,
          reason: String.t() | nil,
          metadata: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Trigger.Project.t() | nil,
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil,
          execution_snapshot: [Trigger.TaskRunExecutionSnapshot.t()]
        }

  schema "TaskRunCheckpoint" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:type, Ecto.Enum, values: [:DOCKER, :KUBERNETES, :COMPUTE])
    field(:location, :string)
    field(:image_ref, :string, source: :imageRef)
    field(:reason, :string)
    field(:metadata, :string)
    field(:project_id, :string, source: :projectId)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, define_field: false, type: :string)
    has_many(:execution_snapshot, Trigger.TaskRunExecutionSnapshot, foreign_key: :checkpoint_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end