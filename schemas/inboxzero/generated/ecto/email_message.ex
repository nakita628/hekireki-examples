defmodule InboxZero.EmailMessage do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          thread_id: String.t(),
          message_id: String.t(),
          date: DateTime.t(),
          from: String.t(),
          from_name: String.t() | nil,
          from_domain: String.t(),
          to: String.t(),
          unsubscribe_link: String.t() | nil,
          read: boolean(),
          sent: boolean(),
          draft: boolean(),
          inbox: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "EmailMessage" do
    field(:id, :string, primary_key: true)
    field(:thread_id, :string, source: :threadId)
    field(:message_id, :string, source: :messageId)
    field(:date, InboxZero.PrismaDateTime)
    field(:from, :string)
    field(:from_name, :string, source: :fromName)
    field(:from_domain, :string, source: :fromDomain)
    field(:to, :string)
    field(:unsubscribe_link, :string, source: :unsubscribeLink)
    field(:read, :boolean)
    field(:sent, :boolean)
    field(:draft, :boolean)
    field(:inbox, :boolean)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end