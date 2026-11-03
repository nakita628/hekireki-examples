defmodule Trigger.ProjectAlert do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          status: atom(),
          type: atom(),
          task_run_attempt_id: String.t() | nil,
          task_run_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Trigger.Project.t() | nil,
          environment: Trigger.RuntimeEnvironment.t() | nil,
          channel: Trigger.ProjectAlertChannel.t() | nil,
          worker_deployment: Trigger.WorkerDeployment.t() | nil
        }

  schema "ProjectAlert" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:status, Ecto.Enum, values: [:PENDING, :SENT, :FAILED], default: :PENDING)
    field(:type, Ecto.Enum, values: [:TASK_RUN, :TASK_RUN_ATTEMPT, :DEPLOYMENT_FAILURE, :DEPLOYMENT_SUCCESS, :ERROR_GROUP, :DASHBOARD_AGENT_WATCH])
    field(:task_run_attempt_id, :string, source: :taskRunAttemptId)
    field(:task_run_id, :string, source: :taskRunId)
    field(:project_id, :string, source: :projectId)
    field(:environment_id, :string, source: :environmentId)
    field(:channel_id, :string, source: :channelId)
    field(:worker_deployment_id, :string, source: :workerDeploymentId)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
    belongs_to(:channel, Trigger.ProjectAlertChannel, foreign_key: :channel_id, define_field: false, type: :string)
    belongs_to(:worker_deployment, Trigger.WorkerDeployment, foreign_key: :worker_deployment_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end