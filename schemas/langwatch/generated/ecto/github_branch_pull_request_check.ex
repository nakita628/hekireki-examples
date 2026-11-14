defmodule LangWatch.GithubBranchPullRequestCheck do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          repository_host: String.t(),
          repository_full_name: String.t(),
          head_branch: String.t(),
          last_checked_at: DateTime.t(),
          pr_count: integer(),
          not_found_at: DateTime.t() | nil,
          recheck_after: DateTime.t() | nil,
          attempts: integer(),
          last_requested_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil
        }

  schema "GithubBranchPullRequestCheck" do
    field(:id, :string, primary_key: true)
    field(:repository_host, :string, source: :repositoryHost)
    field(:repository_full_name, :string, source: :repositoryFullName)
    field(:head_branch, :string, source: :headBranch)
    field(:last_checked_at, LangWatch.PrismaDateTime, source: :lastCheckedAt)
    field(:pr_count, :integer, default: 0, source: :prCount)
    field(:not_found_at, LangWatch.PrismaDateTime, source: :notFoundAt)
    field(:recheck_after, LangWatch.PrismaDateTime, source: :recheckAfter)
    field(:attempts, :integer, default: 0)
    field(:last_requested_at, LangWatch.PrismaDateTime, source: :lastRequestedAt)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end