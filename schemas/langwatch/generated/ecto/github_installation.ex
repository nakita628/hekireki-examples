defmodule LangWatch.GithubInstallation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          installation_id: String.t(),
          account_login: String.t(),
          account_type: String.t(),
          account_id: String.t(),
          repository_selection: String.t(),
          repositories: map() | nil,
          suspended_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil
        }

  schema "GithubInstallation" do
    field(:id, :string, primary_key: true)
    field(:installation_id, :string, source: :installationId)
    field(:account_login, :string, source: :accountLogin)
    field(:account_type, :string, source: :accountType)
    field(:account_id, :string, source: :accountId)
    field(:repository_selection, :string, source: :repositorySelection)
    field(:repositories, :map)
    field(:suspended_at, LangWatch.PrismaDateTime, source: :suspendedAt)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end