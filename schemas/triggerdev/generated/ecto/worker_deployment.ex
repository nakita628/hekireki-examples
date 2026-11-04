defmodule Trigger.WorkerDeployment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          content_hash: String.t(),
          friendly_id: String.t(),
          short_code: String.t(),
          version: String.t(),
          runtime: String.t() | nil,
          runtime_version: String.t() | nil,
          cli_version: String.t() | nil,
          image_reference: String.t() | nil,
          image_platform: String.t(),
          template_key: String.t() | nil,
          index_object_key: String.t() | nil,
          index_sha256: String.t() | nil,
          index_format_version: integer() | nil,
          external_build_data: map() | nil,
          build_server_metadata: map() | nil,
          build_env_vars: map() | nil,
          status: atom(),
          type: atom(),
          triggered_via: String.t() | nil,
          commit_sha: String.t() | nil,
          external_id: String.t() | nil,
          started_at: DateTime.t() | nil,
          installed_at: DateTime.t() | nil,
          built_at: DateTime.t() | nil,
          deployed_at: DateTime.t() | nil,
          failed_at: DateTime.t() | nil,
          error_data: map() | nil,
          canceled_at: DateTime.t() | nil,
          canceled_reason: String.t() | nil,
          git: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Trigger.Project.t() | nil,
          environment: Trigger.RuntimeEnvironment.t() | nil,
          worker: Trigger.BackgroundWorker.t() | nil,
          triggered_by: Trigger.User.t() | nil,
          promotions: [Trigger.WorkerDeploymentPromotion.t()],
          alerts: [Trigger.ProjectAlert.t()],
          worker_instance: [Trigger.WorkerInstance.t()],
          integration_deployments: [Trigger.IntegrationDeployment.t()]
        }

  schema "WorkerDeployment" do
    field(:id, :string, primary_key: true)
    field(:content_hash, :string, source: :contentHash)
    field(:friendly_id, :string, source: :friendlyId)
    field(:short_code, :string, source: :shortCode)
    field(:version, :string)
    field(:runtime, :string)
    field(:runtime_version, :string, source: :runtimeVersion)
    field(:cli_version, :string, source: :cliVersion)
    field(:image_reference, :string, source: :imageReference)
    field(:image_platform, :string, default: "linux/amd64", source: :imagePlatform)
    field(:template_key, :string, source: :templateKey)
    field(:index_object_key, :string, source: :indexObjectKey)
    field(:index_sha256, :string, source: :indexSha256)
    field(:index_format_version, :integer, source: :indexFormatVersion)
    field(:external_build_data, :map, source: :externalBuildData)
    field(:build_server_metadata, :map, source: :buildServerMetadata)
    field(:build_env_vars, :map, source: :buildEnvVars)
    field(:status, Ecto.Enum, values: [:PENDING, :INSTALLING, :BUILDING, :DEPLOYING, :DEPLOYED, :FAILED, :CANCELED, :TIMED_OUT], default: :PENDING)
    field(:type, Ecto.Enum, values: [:MANAGED, :UNMANAGED, :V1], default: :V1)
    field(:triggered_via, :string, source: :triggeredVia)
    field(:commit_sha, :string, source: :commitSHA)
    field(:external_id, :string, source: :externalId)
    field(:started_at, Trigger.PrismaDateTime, source: :startedAt)
    field(:installed_at, Trigger.PrismaDateTime, source: :installedAt)
    field(:built_at, Trigger.PrismaDateTime, source: :builtAt)
    field(:deployed_at, Trigger.PrismaDateTime, source: :deployedAt)
    field(:failed_at, Trigger.PrismaDateTime, source: :failedAt)
    field(:error_data, :map, source: :errorData)
    field(:canceled_at, Trigger.PrismaDateTime, source: :canceledAt)
    field(:canceled_reason, :string, source: :canceledReason)
    field(:git, :map)
    field(:project_id, :string, source: :projectId)
    field(:environment_id, :string, source: :environmentId)
    field(:worker_id, :string, source: :workerId)
    field(:triggered_by_id, :string, source: :triggeredById)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
    belongs_to(:worker, Trigger.BackgroundWorker, foreign_key: :worker_id, define_field: false, type: :string)
    belongs_to(:triggered_by, Trigger.User, foreign_key: :triggered_by_id, define_field: false, type: :string)
    has_many(:promotions, Trigger.WorkerDeploymentPromotion, foreign_key: :deployment_id, references: :id)
    has_many(:alerts, Trigger.ProjectAlert, foreign_key: :worker_deployment_id, references: :id)
    has_many(:worker_instance, Trigger.WorkerInstance, foreign_key: :deployment_id, references: :id)
    has_many(:integration_deployments, Trigger.IntegrationDeployment, foreign_key: :deployment_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end