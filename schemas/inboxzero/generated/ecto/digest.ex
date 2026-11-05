defmodule InboxZero.Digest do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          sent_at: DateTime.t() | nil,
          status: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          items: [InboxZero.DigestItem.t()]
        }

  schema "Digest" do
    field(:id, :string, primary_key: true)
    field(:sent_at, InboxZero.PrismaDateTime, source: :sentAt)
    field(:status, Ecto.Enum, values: [:PENDING, :PROCESSING, :SENT, :FAILED], default: :PENDING)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:items, InboxZero.DigestItem, foreign_key: :digest_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end