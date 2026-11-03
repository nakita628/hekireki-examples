defmodule Trigger.TaskIdentifier do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          slug: String.t(),
          current_trigger_source: atom(),
          first_seen_at: DateTime.t(),
          last_seen_at: DateTime.t(),
          is_in_latest_deployment: boolean(),
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil,
          project: Trigger.Project.t() | nil,
          current_worker: Trigger.BackgroundWorker.t() | nil
        }

  schema "TaskIdentifier" do
    field(:id, :string, primary_key: true)
    field(:slug, :string)
    field(:current_trigger_source, Ecto.Enum, values: [:STANDARD, :SCHEDULED, :AGENT, :WEBHOOK], default: :STANDARD, source: :currentTriggerSource)
    field(:first_seen_at, Trigger.PrismaDateTime, autogenerate: true, source: :firstSeenAt)
    field(:last_seen_at, Trigger.PrismaDateTime, autogenerate: true, source: :lastSeenAt)
    field(:is_in_latest_deployment, :boolean, default: true, source: :isInLatestDeployment)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    field(:project_id, :string, source: :projectId)
    field(:current_worker_id, :string, source: :currentWorkerId)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:current_worker, Trigger.BackgroundWorker, foreign_key: :current_worker_id, define_field: false, type: :string)
  end
end