defmodule InboxZero.EmailToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          token: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "EmailToken" do
    field(:id, :string, primary_key: true)
    field(:token, :string)
    field(:expires_at, InboxZero.PrismaDateTime, source: :expiresAt)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end