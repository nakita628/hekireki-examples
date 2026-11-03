defmodule Trigger.ConnectedGithubRepository do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          branch_tracking: map(),
          preview_deployments_enabled: boolean(),
          build_filters: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Trigger.Project.t() | nil,
          repository: Trigger.GithubRepository.t() | nil
        }

  schema "ConnectedGithubRepository" do
    field(:id, :string, primary_key: true)
    field(:branch_tracking, :map, source: :branchTracking)
    field(:preview_deployments_enabled, :boolean, default: true, source: :previewDeploymentsEnabled)
    field(:build_filters, :map, source: :buildFilters)
    field(:project_id, :string, source: :projectId)
    field(:repository_id, :string, source: :repositoryId)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:repository, Trigger.GithubRepository, foreign_key: :repository_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end