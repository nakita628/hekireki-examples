defmodule Trigger.WorkerInstanceGroup do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          name: String.t(),
          master_queue: String.t(),
          description: String.t() | nil,
          hidden: boolean(),
          cloud_provider: String.t() | nil,
          location: String.t() | nil,
          static_ips: String.t() | nil,
          workload_type: atom(),
          region: String.t() | nil,
          enable_fast_path: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          token: Trigger.WorkerGroupToken.t() | nil,
          organization: Trigger.Organization.t() | nil,
          project: Trigger.Project.t() | nil,
          workers: [Trigger.WorkerInstance.t()],
          background_workers: [Trigger.BackgroundWorker.t()],
          default_for_projects: [Trigger.Project.t()]
        }

  schema "WorkerInstanceGroup" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:MANAGED, :UNMANAGED])
    field(:name, :string)
    field(:master_queue, :string, source: :masterQueue)
    field(:description, :string)
    field(:hidden, :boolean, default: false)
    field(:cloud_provider, :string, source: :cloudProvider)
    field(:location, :string)
    field(:static_ips, :string, source: :staticIPs)
    field(:workload_type, Ecto.Enum, values: [:CONTAINER, :MICROVM], default: :CONTAINER, source: :workloadType)
    field(:region, :string)
    field(:enable_fast_path, :boolean, default: false, source: :enableFastPath)
    field(:token_id, :string, source: :tokenId)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:token, Trigger.WorkerGroupToken, foreign_key: :token_id, define_field: false, type: :string)
    belongs_to(:organization, Trigger.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:workers, Trigger.WorkerInstance, foreign_key: :worker_group_id)
    has_many(:background_workers, Trigger.BackgroundWorker, foreign_key: :worker_group_id)
    has_many(:default_for_projects, Trigger.Project, foreign_key: :default_worker_group_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end