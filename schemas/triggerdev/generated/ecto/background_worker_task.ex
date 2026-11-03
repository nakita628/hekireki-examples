defmodule Trigger.BackgroundWorkerTask do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          slug: String.t(),
          description: String.t() | nil,
          friendly_id: String.t(),
          file_path: String.t(),
          export_name: String.t() | nil,
          queue_config: map() | nil,
          retry_config: map() | nil,
          machine_config: map() | nil,
          gates: map() | nil,
          max_duration_in_seconds: integer() | nil,
          ttl: String.t() | nil,
          trigger_source: atom(),
          config: map() | nil,
          payload_schema: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          worker: Trigger.BackgroundWorker.t() | nil,
          project: Trigger.Project.t() | nil,
          file: Trigger.BackgroundWorkerFile.t() | nil,
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil,
          queue: Trigger.TaskQueue.t() | nil,
          attempts: [Trigger.TaskRunAttempt.t()],
          runs: [Trigger.TaskRun.t()]
        }

  schema "BackgroundWorkerTask" do
    field(:id, :string, primary_key: true)
    field(:slug, :string)
    field(:description, :string)
    field(:friendly_id, :string, source: :friendlyId)
    field(:file_path, :string, source: :filePath)
    field(:export_name, :string, source: :exportName)
    field(:queue_config, :map, source: :queueConfig)
    field(:retry_config, :map, source: :retryConfig)
    field(:machine_config, :map, source: :machineConfig)
    field(:gates, :map)
    field(:max_duration_in_seconds, :integer, source: :maxDurationInSeconds)
    field(:ttl, :string)
    field(:trigger_source, Ecto.Enum, values: [:STANDARD, :SCHEDULED, :AGENT, :WEBHOOK], default: :STANDARD, source: :triggerSource)
    field(:config, :map)
    field(:payload_schema, :map, source: :payloadSchema)
    field(:worker_id, :string, source: :workerId)
    field(:project_id, :string, source: :projectId)
    field(:file_id, :string, source: :fileId)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    field(:queue_id, :string, source: :queueId)
    belongs_to(:worker, Trigger.BackgroundWorker, foreign_key: :worker_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:file, Trigger.BackgroundWorkerFile, foreign_key: :file_id, define_field: false, type: :string)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, define_field: false, type: :string)
    belongs_to(:queue, Trigger.TaskQueue, foreign_key: :queue_id, define_field: false, type: :string)
    has_many(:attempts, Trigger.TaskRunAttempt, foreign_key: :background_worker_task_id)
    has_many(:runs, Trigger.TaskRun, foreign_key: :locked_by_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end