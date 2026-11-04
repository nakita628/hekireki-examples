defmodule Trigger.TaskQueue do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          name: String.t(),
          type: atom(),
          version: atom(),
          orderable_name: String.t() | nil,
          concurrency_version: atom(),
          role: atom(),
          concurrency_limit: integer() | nil,
          concurrency_limit_overridden_at: DateTime.t() | nil,
          concurrency_limit_overridden_by: String.t() | nil,
          concurrency_limit_base: integer() | nil,
          concurrency_limit_override_percent: Decimal.t() | nil,
          total_concurrency_limit: integer() | nil,
          total_concurrency_limit_overridden_at: DateTime.t() | nil,
          total_concurrency_limit_overridden_by: String.t() | nil,
          total_concurrency_limit_base: integer() | nil,
          rate_limit: map() | nil,
          paused: boolean(),
          archived_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Trigger.Project.t() | nil,
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil,
          attempts: [Trigger.TaskRunAttempt.t()],
          tasks: [Trigger.BackgroundWorkerTask.t()],
          workers: [Trigger.BackgroundWorker.t()]
        }

  schema "TaskQueue" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:name, :string)
    field(:type, Ecto.Enum, values: [:VIRTUAL, :NAMED], default: :VIRTUAL)
    field(:version, Ecto.Enum, values: [:V1, :V2], default: :V1)
    field(:orderable_name, :string, source: :orderableName)
    field(:concurrency_version, Ecto.Enum, values: [:V1, :V2], default: :V1, source: :concurrencyVersion)
    field(:role, Ecto.Enum, values: [:QUEUE, :LIMIT], default: :QUEUE)
    field(:concurrency_limit, :integer, source: :concurrencyLimit)
    field(:concurrency_limit_overridden_at, Trigger.PrismaDateTime, source: :concurrencyLimitOverriddenAt)
    field(:concurrency_limit_overridden_by, :string, source: :concurrencyLimitOverriddenBy)
    field(:concurrency_limit_base, :integer, source: :concurrencyLimitBase)
    field(:concurrency_limit_override_percent, :decimal, source: :concurrencyLimitOverridePercent)
    field(:total_concurrency_limit, :integer, source: :totalConcurrencyLimit)
    field(:total_concurrency_limit_overridden_at, Trigger.PrismaDateTime, source: :totalConcurrencyLimitOverriddenAt)
    field(:total_concurrency_limit_overridden_by, :string, source: :totalConcurrencyLimitOverriddenBy)
    field(:total_concurrency_limit_base, :integer, source: :totalConcurrencyLimitBase)
    field(:rate_limit, :map, source: :rateLimit)
    field(:paused, :boolean, default: false)
    field(:archived_at, Trigger.PrismaDateTime, source: :archivedAt)
    field(:project_id, :string, source: :projectId)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, define_field: false, type: :string)
    has_many(:attempts, Trigger.TaskRunAttempt, foreign_key: :queue_id, references: :id)
    has_many(:tasks, Trigger.BackgroundWorkerTask, foreign_key: :queue_id, references: :id)
    many_to_many(:workers, Trigger.BackgroundWorker, join_through: "_BackgroundWorkerToTaskQueue", join_keys: [B: :id, A: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end