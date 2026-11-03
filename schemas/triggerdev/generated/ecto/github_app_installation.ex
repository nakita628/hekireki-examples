defmodule Trigger.GithubAppInstallation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          app_installation_id: integer(),
          target_id: integer(),
          target_type: String.t(),
          account_handle: String.t(),
          permissions: map() | nil,
          repository_selection: atom(),
          installed_by: String.t() | nil,
          installed_by_user_id: String.t() | nil,
          deleted_at: DateTime.t() | nil,
          suspended_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Trigger.Organization.t() | nil,
          repositories: [Trigger.GithubRepository.t()]
        }

  schema "GithubAppInstallation" do
    field(:id, :string, primary_key: true)
    field(:app_installation_id, :integer, source: :appInstallationId)
    field(:target_id, :integer, source: :targetId)
    field(:target_type, :string, source: :targetType)
    field(:account_handle, :string, source: :accountHandle)
    field(:permissions, :map)
    field(:repository_selection, Ecto.Enum, values: [:ALL, :SELECTED], source: :repositorySelection)
    field(:installed_by, :string, source: :installedBy)
    field(:installed_by_user_id, :string, source: :installedByUserId)
    field(:deleted_at, Trigger.PrismaDateTime, source: :deletedAt)
    field(:suspended_at, Trigger.PrismaDateTime, source: :suspendedAt)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, Trigger.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:repositories, Trigger.GithubRepository, foreign_key: :installation_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end