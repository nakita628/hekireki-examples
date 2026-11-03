defmodule Formbricks.Organization do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          whitelabel: map(),
          is_aismart_tools_enabled: boolean(),
          display_time_zone: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          billing: Formbricks.OrganizationBilling.t() | nil,
          memberships: [Formbricks.Membership.t()],
          workspaces: [Formbricks.Workspace.t()],
          invites: [Formbricks.Invite.t()],
          teams: [Formbricks.Team.t()],
          api_keys: [Formbricks.ApiKey.t()],
          feedback_directories: [Formbricks.FeedbackDirectory.t()],
          retention_policies: [Formbricks.RetentionPolicy.t()],
          retention_exemptions: [Formbricks.RetentionExemption.t()],
          retention_runs: [Formbricks.RetentionRun.t()],
          retention_notices: [Formbricks.RetentionNotice.t()]
        }

  schema "Organization" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:whitelabel, :map, default: %{})
    field(:is_aismart_tools_enabled, :boolean, default: false, source: :isAISmartToolsEnabled)
    field(:display_time_zone, :string, source: :displayTimeZone)
    has_one(:billing, Formbricks.OrganizationBilling, foreign_key: :organization_id)
    has_many(:memberships, Formbricks.Membership, foreign_key: :organization_id)
    has_many(:workspaces, Formbricks.Workspace, foreign_key: :organization_id)
    has_many(:invites, Formbricks.Invite, foreign_key: :organization_id)
    has_many(:teams, Formbricks.Team, foreign_key: :organization_id)
    has_many(:api_keys, Formbricks.ApiKey, foreign_key: :organization_id)
    has_many(:feedback_directories, Formbricks.FeedbackDirectory, foreign_key: :organization_id)
    has_many(:retention_policies, Formbricks.RetentionPolicy, foreign_key: :organization_id)
    has_many(:retention_exemptions, Formbricks.RetentionExemption, foreign_key: :organization_id)
    has_many(:retention_runs, Formbricks.RetentionRun, foreign_key: :organization_id)
    has_many(:retention_notices, Formbricks.RetentionNotice, foreign_key: :organization_id)
    timestamps(inserted_at_source: :created_at)
  end
end