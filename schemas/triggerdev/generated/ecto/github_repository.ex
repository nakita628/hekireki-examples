defmodule Trigger.GithubRepository do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          github_id: integer(),
          name: String.t(),
          full_name: String.t(),
          html_url: String.t(),
          private: boolean(),
          default_branch: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          installation: Trigger.GithubAppInstallation.t() | nil,
          connected_github_repository: [Trigger.ConnectedGithubRepository.t()]
        }

  schema "GithubRepository" do
    field(:id, :string, primary_key: true)
    field(:github_id, :integer, source: :githubId)
    field(:name, :string)
    field(:full_name, :string, source: :fullName)
    field(:html_url, :string, source: :htmlUrl)
    field(:private, :boolean)
    field(:default_branch, :string, source: :defaultBranch)
    field(:installation_id, :string, source: :installationId)
    belongs_to(:installation, Trigger.GithubAppInstallation, foreign_key: :installation_id, define_field: false, type: :string)
    has_many(:connected_github_repository, Trigger.ConnectedGithubRepository, foreign_key: :repository_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end