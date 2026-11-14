defmodule LangWatch.GithubPullRequest do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          repository_host: String.t(),
          repository_full_name: String.t(),
          head_branch: String.t(),
          pr_number: integer(),
          html_url: String.t(),
          title: String.t(),
          state: String.t(),
          is_draft: boolean(),
          author_login: String.t() | nil,
          pr_created_at: DateTime.t(),
          pr_closed_at: DateTime.t() | nil,
          pr_merged_at: DateTime.t() | nil,
          pr_updated_at: DateTime.t() | nil,
          mapped_at: DateTime.t(),
          last_checked_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil
        }

  schema "GithubPullRequest" do
    field(:id, :string, primary_key: true)
    field(:repository_host, :string, source: :repositoryHost)
    field(:repository_full_name, :string, source: :repositoryFullName)
    field(:head_branch, :string, source: :headBranch)
    field(:pr_number, :integer, source: :prNumber)
    field(:html_url, :string, source: :htmlUrl)
    field(:title, :string)
    field(:state, :string)
    field(:is_draft, :boolean, default: false, source: :isDraft)
    field(:author_login, :string, source: :authorLogin)
    field(:pr_created_at, LangWatch.PrismaDateTime, source: :prCreatedAt)
    field(:pr_closed_at, LangWatch.PrismaDateTime, source: :prClosedAt)
    field(:pr_merged_at, LangWatch.PrismaDateTime, source: :prMergedAt)
    field(:pr_updated_at, LangWatch.PrismaDateTime, source: :prUpdatedAt)
    field(:mapped_at, LangWatch.PrismaDateTime, autogenerate: true, source: :mappedAt)
    field(:last_checked_at, LangWatch.PrismaDateTime, autogenerate: true, source: :lastCheckedAt)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end