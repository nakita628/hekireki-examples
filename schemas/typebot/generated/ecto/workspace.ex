defmodule Typebot.Workspace do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          icon: String.t() | nil,
          plan: atom(),
          stripe_id: String.t() | nil,
          additional_chats_index: integer(),
          additional_storage_index: integer(),
          chats_limit_first_email_sent_at: DateTime.t() | nil,
          storage_limit_first_email_sent_at: DateTime.t() | nil,
          chats_limit_second_email_sent_at: DateTime.t() | nil,
          storage_limit_second_email_sent_at: DateTime.t() | nil,
          custom_chats_limit: integer() | nil,
          custom_storage_limit: integer() | nil,
          custom_seats_limit: integer() | nil,
          is_quarantined: boolean(),
          is_suspended: boolean(),
          is_past_due: boolean(),
          is_verified: boolean() | nil,
          chats_hard_limit: integer() | nil,
          settings: map() | nil,
          last_activity_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          claimable_custom_plan: Typebot.ClaimableCustomPlan.t() | nil,
          credentials: [Typebot.Credentials.t()],
          custom_domains: [Typebot.CustomDomain.t()],
          folders: [Typebot.DashboardFolder.t()],
          members: [Typebot.MemberInWorkspace.t()],
          typebots: [Typebot.Typebot.t()],
          invitations: [Typebot.WorkspaceInvitation.t()],
          theme_templates: [Typebot.ThemeTemplate.t()],
          spaces: [Typebot.Space.t()]
        }

  schema "Workspace" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:icon, :string)
    field(:plan, Ecto.Enum, values: [:FREE, :STARTER, :PRO, :LIFETIME, :OFFERED, :CUSTOM, :UNLIMITED, :ENTERPRISE], default: :FREE)
    field(:stripe_id, :string, source: :stripeId)
    field(:additional_chats_index, :integer, default: 0, source: :additionalChatsIndex)
    field(:additional_storage_index, :integer, default: 0, source: :additionalStorageIndex)
    field(:chats_limit_first_email_sent_at, Typebot.PrismaDateTime, source: :chatsLimitFirstEmailSentAt)
    field(:storage_limit_first_email_sent_at, Typebot.PrismaDateTime, source: :storageLimitFirstEmailSentAt)
    field(:chats_limit_second_email_sent_at, Typebot.PrismaDateTime, source: :chatsLimitSecondEmailSentAt)
    field(:storage_limit_second_email_sent_at, Typebot.PrismaDateTime, source: :storageLimitSecondEmailSentAt)
    field(:custom_chats_limit, :integer, source: :customChatsLimit)
    field(:custom_storage_limit, :integer, source: :customStorageLimit)
    field(:custom_seats_limit, :integer, source: :customSeatsLimit)
    field(:is_quarantined, :boolean, default: false, source: :isQuarantined)
    field(:is_suspended, :boolean, default: false, source: :isSuspended)
    field(:is_past_due, :boolean, default: false, source: :isPastDue)
    field(:is_verified, :boolean, source: :isVerified)
    field(:chats_hard_limit, :integer, source: :chatsHardLimit)
    field(:settings, :map)
    field(:last_activity_at, Typebot.PrismaDateTime, source: :lastActivityAt)
    has_one(:claimable_custom_plan, Typebot.ClaimableCustomPlan, foreign_key: :workspace_id, references: :id)
    has_many(:credentials, Typebot.Credentials, foreign_key: :workspace_id, references: :id)
    has_many(:custom_domains, Typebot.CustomDomain, foreign_key: :workspace_id, references: :id)
    has_many(:folders, Typebot.DashboardFolder, foreign_key: :workspace_id, references: :id)
    has_many(:members, Typebot.MemberInWorkspace, foreign_key: :workspace_id, references: :id)
    has_many(:typebots, Typebot.Typebot, foreign_key: :workspace_id, references: :id)
    has_many(:invitations, Typebot.WorkspaceInvitation, foreign_key: :workspace_id, references: :id)
    has_many(:theme_templates, Typebot.ThemeTemplate, foreign_key: :workspace_id, references: :id)
    has_many(:spaces, Typebot.Space, foreign_key: :workspace_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end