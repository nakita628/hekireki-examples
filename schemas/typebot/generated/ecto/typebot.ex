defmodule Typebot.Typebot do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          version: String.t() | nil,
          icon: String.t() | nil,
          name: String.t(),
          groups: map(),
          events: map() | nil,
          variables: map(),
          edges: map(),
          theme: map(),
          selected_theme_template_id: String.t() | nil,
          settings: map(),
          public_id: String.t() | nil,
          custom_domain: String.t() | nil,
          results_table_preferences: map() | nil,
          is_archived: boolean(),
          is_closed: boolean(),
          whats_app_credentials_id: String.t() | nil,
          risk_level: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          folder: Typebot.DashboardFolder.t() | nil,
          workspace: Typebot.Workspace.t() | nil,
          space: Typebot.Space.t() | nil,
          published_typebot: Typebot.PublicTypebot.t() | nil,
          collaborators: [Typebot.CollaboratorsOnTypebots.t()],
          invitations: [Typebot.Invitation.t()],
          results: [Typebot.Result.t()],
          webhooks: [Typebot.Webhook.t()],
          banned_ips: [Typebot.BannedIp.t()]
        }

  schema "Typebot" do
    field(:id, :string, primary_key: true)
    field(:version, :string)
    field(:icon, :string)
    field(:name, :string)
    field(:groups, :map)
    field(:events, :map)
    field(:variables, :map)
    field(:edges, :map)
    field(:theme, :map)
    field(:selected_theme_template_id, :string, source: :selectedThemeTemplateId)
    field(:settings, :map)
    field(:public_id, :string, source: :publicId)
    field(:custom_domain, :string, source: :customDomain)
    field(:results_table_preferences, :map, source: :resultsTablePreferences)
    field(:is_archived, :boolean, default: false, source: :isArchived)
    field(:is_closed, :boolean, default: false, source: :isClosed)
    field(:whats_app_credentials_id, :string, source: :whatsAppCredentialsId)
    field(:risk_level, :integer, source: :riskLevel)
    field(:folder_id, :string, source: :folderId)
    field(:workspace_id, :string, source: :workspaceId)
    field(:space_id, :string, source: :spaceId)
    belongs_to(:folder, Typebot.DashboardFolder, foreign_key: :folder_id, define_field: false, type: :string)
    belongs_to(:workspace, Typebot.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    belongs_to(:space, Typebot.Space, foreign_key: :space_id, define_field: false, type: :string)
    has_one(:published_typebot, Typebot.PublicTypebot, foreign_key: :typebot_id)
    has_many(:collaborators, Typebot.CollaboratorsOnTypebots, foreign_key: :typebot_id)
    has_many(:invitations, Typebot.Invitation, foreign_key: :typebot_id)
    has_many(:results, Typebot.Result, foreign_key: :typebot_id)
    has_many(:webhooks, Typebot.Webhook, foreign_key: :typebot_id)
    has_many(:banned_ips, Typebot.BannedIp, foreign_key: :responsible_typebot_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end