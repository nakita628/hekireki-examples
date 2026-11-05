defmodule InboxZero.Member do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          role: String.t(),
          allow_org_admin_analytics: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: InboxZero.Organization.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil,
          conversations: [InboxZero.Conversation.t()],
          conversation_participants: [InboxZero.ConversationParticipant.t()],
          conversation_comments: [InboxZero.ConversationComment.t()]
        }

  schema "Member" do
    field(:id, :string, primary_key: true)
    field(:role, :string, default: "member")
    field(:allow_org_admin_analytics, :boolean, default: false, source: :allowOrgAdminAnalytics)
    field(:organization_id, :string, source: :organizationId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:organization, InboxZero.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:conversations, InboxZero.Conversation, foreign_key: :publisher_id, references: :id)
    has_many(:conversation_participants, InboxZero.ConversationParticipant, foreign_key: :member_id, references: :id)
    has_many(:conversation_comments, InboxZero.ConversationComment, foreign_key: :author_member_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end