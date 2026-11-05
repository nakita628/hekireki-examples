defmodule InboxZero.ThreadTracker do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          sent_at: DateTime.t(),
          thread_id: String.t(),
          message_id: String.t(),
          resolved: boolean(),
          type: atom(),
          follow_up_applied_at: DateTime.t() | nil,
          follow_up_draft_id: String.t() | nil,
          follow_up_draft_content: String.t() | nil,
          follow_up_draft_created_at: DateTime.t() | nil,
          follow_up_notifications: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "ThreadTracker" do
    field(:id, :string, primary_key: true)
    field(:sent_at, InboxZero.PrismaDateTime, source: :sentAt)
    field(:thread_id, :string, source: :threadId)
    field(:message_id, :string, source: :messageId)
    field(:resolved, :boolean, default: false)
    field(:type, Ecto.Enum, values: [:AWAITING, :NEEDS_REPLY, :NEEDS_ACTION])
    field(:follow_up_applied_at, InboxZero.PrismaDateTime, source: :followUpAppliedAt)
    field(:follow_up_draft_id, :string, source: :followUpDraftId)
    field(:follow_up_draft_content, :string, source: :followUpDraftContent)
    field(:follow_up_draft_created_at, InboxZero.PrismaDateTime, source: :followUpDraftCreatedAt)
    field(:follow_up_notifications, :map, source: :followUpNotifications)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end