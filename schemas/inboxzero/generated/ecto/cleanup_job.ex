defmodule InboxZero.CleanupJob do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          action: atom(),
          days_old: integer(),
          instructions: String.t() | nil,
          skip_reply: boolean() | nil,
          skip_starred: boolean() | nil,
          skip_calendar: boolean() | nil,
          skip_receipt: boolean() | nil,
          skip_attachment: boolean() | nil,
          skip_conversation: boolean() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          threads: [InboxZero.CleanupThread.t()]
        }

  schema "CleanupJob" do
    field(:id, :string, primary_key: true)
    field(:action, Ecto.Enum, values: [:ARCHIVE, :MARK_READ], default: :ARCHIVE)
    field(:days_old, :integer, default: 7, source: :daysOld)
    field(:instructions, :string)
    field(:skip_reply, :boolean, source: :skipReply)
    field(:skip_starred, :boolean, source: :skipStarred)
    field(:skip_calendar, :boolean, source: :skipCalendar)
    field(:skip_receipt, :boolean, source: :skipReceipt)
    field(:skip_attachment, :boolean, source: :skipAttachment)
    field(:skip_conversation, :boolean, source: :skipConversation)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:threads, InboxZero.CleanupThread, foreign_key: :job_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end