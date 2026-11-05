defmodule InboxZero.Organization do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          slug: String.t(),
          logo: String.t() | nil,
          metadata: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          sso_provider: InboxZero.SsoProvider.t() | nil,
          members: [InboxZero.Member.t()],
          sessions: [InboxZero.Session.t()],
          invitations: [InboxZero.Invitation.t()],
          rules: [InboxZero.OrganizationRule.t()],
          conversations: [InboxZero.Conversation.t()]
        }

  schema "Organization" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:slug, :string)
    field(:logo, :string)
    field(:metadata, :map)
    has_one(:sso_provider, InboxZero.SsoProvider, foreign_key: :organization_id, references: :id)
    has_many(:members, InboxZero.Member, foreign_key: :organization_id, references: :id)
    has_many(:sessions, InboxZero.Session, foreign_key: :active_organization_id, references: :id)
    has_many(:invitations, InboxZero.Invitation, foreign_key: :organization_id, references: :id)
    has_many(:rules, InboxZero.OrganizationRule, foreign_key: :organization_id, references: :id)
    has_many(:conversations, InboxZero.Conversation, foreign_key: :organization_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end