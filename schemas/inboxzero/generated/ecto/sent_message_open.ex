defmodule InboxZero.SentMessageOpen do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          token: String.t(),
          thread_id: String.t() | nil,
          message_id: String.t() | nil,
          first_opened_at: DateTime.t() | nil,
          last_opened_at: DateTime.t() | nil,
          open_count: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "SentMessageOpen" do
    field(:id, :string, primary_key: true)
    field(:token, :string)
    field(:thread_id, :string, source: :threadId)
    field(:message_id, :string, source: :messageId)
    field(:first_opened_at, InboxZero.PrismaDateTime, source: :firstOpenedAt)
    field(:last_opened_at, InboxZero.PrismaDateTime, source: :lastOpenedAt)
    field(:open_count, :integer, default: 0, source: :openCount)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end