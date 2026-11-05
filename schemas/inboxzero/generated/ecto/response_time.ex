defmodule InboxZero.ResponseTime do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          thread_id: String.t(),
          sent_message_id: String.t(),
          received_message_id: String.t(),
          response_time_mins: integer(),
          received_at: DateTime.t(),
          sent_at: DateTime.t(),
          inserted_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "ResponseTime" do
    field(:id, :string, primary_key: true)
    field(:thread_id, :string, source: :threadId)
    field(:sent_message_id, :string, source: :sentMessageId)
    field(:received_message_id, :string, source: :receivedMessageId)
    field(:response_time_mins, :integer, source: :responseTimeMins)
    field(:received_at, InboxZero.PrismaDateTime, source: :receivedAt)
    field(:sent_at, InboxZero.PrismaDateTime, source: :sentAt)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end