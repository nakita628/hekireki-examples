defmodule Trigger.TrustedDeployRepository do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          issuer: String.t(),
          repository_owner: String.t(),
          repository_name: String.t(),
          repository_id: String.t() | nil,
          repository_owner_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Trigger.Project.t() | nil,
          environments: [Trigger.TrustedDeployEnvironment.t()]
        }

  schema "TrustedDeployRepository" do
    field(:id, :string, primary_key: true)
    field(:issuer, :string, default: "https://token.actions.githubusercontent.com")
    field(:repository_owner, :string, source: :repositoryOwner)
    field(:repository_name, :string, source: :repositoryName)
    field(:repository_id, :string, source: :repositoryId)
    field(:repository_owner_id, :string, source: :repositoryOwnerId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:environments, Trigger.TrustedDeployEnvironment, foreign_key: :trusted_deploy_repository_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end