defmodule InboxZero.ChatMemory do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          content: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          chat: InboxZero.Chat.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "ChatMemory" do
    field(:id, :string, primary_key: true)
    field(:content, :string)
    field(:chat_id, :string, source: :chatId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:chat, InboxZero.Chat, foreign_key: :chat_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end