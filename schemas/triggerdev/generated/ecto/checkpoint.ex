defmodule Trigger.Checkpoint do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          type: atom(),
          location: String.t(),
          image_ref: String.t(),
          reason: String.t() | nil,
          metadata: String.t() | nil,
          attempt_number: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          run: Trigger.TaskRun.t() | nil,
          attempt: Trigger.TaskRunAttempt.t() | nil,
          project: Trigger.Project.t() | nil,
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil,
          events: [Trigger.CheckpointRestoreEvent.t()]
        }

  schema "Checkpoint" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:type, Ecto.Enum, values: [:DOCKER, :KUBERNETES])
    field(:location, :string)
    field(:image_ref, :string, source: :imageRef)
    field(:reason, :string)
    field(:metadata, :string)
    field(:attempt_number, :integer, source: :attemptNumber)
    field(:run_id, :string, source: :runId)
    field(:attempt_id, :string, source: :attemptId)
    field(:project_id, :string, source: :projectId)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    belongs_to(:run, Trigger.TaskRun, foreign_key: :run_id, define_field: false, type: :string)
    belongs_to(:attempt, Trigger.TaskRunAttempt, foreign_key: :attempt_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, define_field: false, type: :string)
    has_many(:events, Trigger.CheckpointRestoreEvent, foreign_key: :checkpoint_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end