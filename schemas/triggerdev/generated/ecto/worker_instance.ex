defmodule Trigger.WorkerInstance do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          resource_identifier: String.t(),
          metadata: map() | nil,
          last_dequeue_at: DateTime.t() | nil,
          last_heartbeat_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          worker_group: Trigger.WorkerInstanceGroup.t() | nil,
          organization: Trigger.Organization.t() | nil,
          project: Trigger.Project.t() | nil,
          environment: Trigger.RuntimeEnvironment.t() | nil,
          deployment: Trigger.WorkerDeployment.t() | nil
        }

  schema "WorkerInstance" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:resource_identifier, :string, source: :resourceIdentifier)
    field(:metadata, :map)
    field(:last_dequeue_at, Trigger.PrismaDateTime, source: :lastDequeueAt)
    field(:last_heartbeat_at, Trigger.PrismaDateTime, source: :lastHeartbeatAt)
    field(:worker_group_id, :string, source: :workerGroupId)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    field(:environment_id, :string, source: :environmentId)
    field(:deployment_id, :string, source: :deploymentId)
    belongs_to(:worker_group, Trigger.WorkerInstanceGroup, foreign_key: :worker_group_id, define_field: false, type: :string)
    belongs_to(:organization, Trigger.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
    belongs_to(:deployment, Trigger.WorkerDeployment, foreign_key: :deployment_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end