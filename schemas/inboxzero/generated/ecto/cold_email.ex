defmodule InboxZero.ColdEmail do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          from_email: String.t(),
          message_id: String.t() | nil,
          thread_id: String.t() | nil,
          status: atom() | nil,
          reason: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          digest_items: [InboxZero.DigestItem.t()]
        }

  schema "ColdEmail" do
    field(:id, :string, primary_key: true)
    field(:from_email, :string, source: :fromEmail)
    field(:message_id, :string, source: :messageId)
    field(:thread_id, :string, source: :threadId)
    field(:status, Ecto.Enum, values: [:AI_LABELED_COLD, :USER_REJECTED_COLD])
    field(:reason, :string)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:digest_items, InboxZero.DigestItem, foreign_key: :cold_email_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end