defmodule LangWatch.Team do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          slug: String.t(),
          archived_at: DateTime.t() | nil,
          is_personal: boolean(),
          department_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil,
          owner_user: LangWatch.User.t() | nil,
          members: [LangWatch.TeamUser.t()],
          projects: [LangWatch.Project.t()],
          ingestion_sources: [LangWatch.IngestionSource.t()],
          ai_tool_entry_teams: [LangWatch.AiToolEntryTeam.t()]
        }

  schema "Team" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:slug, :string)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:is_personal, :boolean, default: false, source: :isPersonal)
    field(:department_id, :string, source: :departmentId)
    field(:organization_id, :string, source: :organizationId)
    field(:owner_user_id, :string, source: :ownerUserId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:owner_user, LangWatch.User, foreign_key: :owner_user_id, define_field: false, type: :string)
    has_many(:members, LangWatch.TeamUser, foreign_key: :team_id, references: :id)
    has_many(:projects, LangWatch.Project, foreign_key: :team_id, references: :id)
    has_many(:ingestion_sources, LangWatch.IngestionSource, foreign_key: :team_id, references: :id)
    has_many(:ai_tool_entry_teams, LangWatch.AiToolEntryTeam, foreign_key: :team_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end