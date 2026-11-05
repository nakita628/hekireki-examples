defmodule OpenPanel.User do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          first_name: String.t() | nil,
          last_name: String.t() | nil,
          name: String.t() | nil,
          email_verified: boolean(),
          image: String.t() | nil,
          two_factor_enabled: boolean(),
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          two_factor: OpenPanel.TwoFactor.t() | nil,
          totp: OpenPanel.UserTotp.t() | nil,
          created_organizations: [OpenPanel.Organization.t()],
          subscriptions: [OpenPanel.Organization.t()],
          membership: [OpenPanel.Member.t()],
          sent_invites: [OpenPanel.Member.t()],
          sessions: [OpenPanel.Session.t()],
          accounts: [OpenPanel.Account.t()],
          invites: [OpenPanel.Invite.t()],
          conversations: [OpenPanel.Conversation.t()],
          two_factor_challenges: [OpenPanel.TwoFactorChallenge.t()]
        }

  schema "users" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:email, :string)
    field(:first_name, :string, source: :firstName)
    field(:last_name, :string, source: :lastName)
    field(:name, :string)
    field(:email_verified, :boolean, default: false, source: :emailVerified)
    field(:image, :string)
    field(:two_factor_enabled, :boolean, default: false, source: :twoFactorEnabled)
    field(:deleted_at, OpenPanel.PrismaDateTime, source: :deletedAt)
    has_one(:two_factor, OpenPanel.TwoFactor, foreign_key: :user_id, references: :id)
    has_one(:totp, OpenPanel.UserTotp, foreign_key: :user_id, references: :id)
    has_many(:created_organizations, OpenPanel.Organization, foreign_key: :created_by_user_id, references: :id)
    has_many(:subscriptions, OpenPanel.Organization, foreign_key: :subscription_created_by_user_id, references: :id)
    has_many(:membership, OpenPanel.Member, foreign_key: :user_id, references: :id)
    has_many(:sent_invites, OpenPanel.Member, foreign_key: :invited_by_id, references: :id)
    has_many(:sessions, OpenPanel.Session, foreign_key: :user_id, references: :id)
    has_many(:accounts, OpenPanel.Account, foreign_key: :user_id, references: :id)
    has_many(:invites, OpenPanel.Invite, foreign_key: :created_by_id, references: :id)
    has_many(:conversations, OpenPanel.Conversation, foreign_key: :user_id, references: :id)
    has_many(:two_factor_challenges, OpenPanel.TwoFactorChallenge, foreign_key: :user_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end