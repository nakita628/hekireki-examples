defmodule InboxZero.Invitation do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          role: String.t() | nil,
          status: String.t(),
          expires_at: DateTime.t(),
          organization: InboxZero.Organization.t() | nil,
          inviter: InboxZero.EmailAccount.t() | nil
        }

  schema "invitation" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:role, :string)
    field(:status, :string)
    field(:expires_at, InboxZero.PrismaDateTime, source: :expiresAt)
    field(:organization_id, :string, source: :organizationId)
    field(:inviter_id, :string, source: :inviterId)
    belongs_to(:organization, InboxZero.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:inviter, InboxZero.EmailAccount, foreign_key: :inviter_id, define_field: false, type: :string)
  end
end