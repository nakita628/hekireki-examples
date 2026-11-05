defmodule InboxZero.ScheduledAction do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          action_type: atom(),
          message_id: String.t(),
          thread_id: String.t(),
          scheduled_for: DateTime.t(),
          status: atom(),
          scheduling_status: atom(),
          label: String.t() | nil,
          label_id: String.t() | nil,
          subject: String.t() | nil,
          content: String.t() | nil,
          to: String.t() | nil,
          cc: String.t() | nil,
          bcc: String.t() | nil,
          url: String.t() | nil,
          folder_name: String.t() | nil,
          folder_id: String.t() | nil,
          scheduled_id: String.t() | nil,
          static_attachments: map() | nil,
          selected_attachments: map() | nil,
          executed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          executed_rule: InboxZero.ExecutedRule.t() | nil,
          executed_action: InboxZero.ExecutedAction.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "ScheduledAction" do
    field(:id, :string, primary_key: true)
    field(:action_type, Ecto.Enum, values: [:ARCHIVE, :LABEL, :REPLY, :SEND_EMAIL, :FORWARD, :DRAFT_EMAIL, :DRAFT_MESSAGING_CHANNEL, :NOTIFY_MESSAGING_CHANNEL, :MARK_SPAM, :CALL_WEBHOOK, :MARK_READ, :STAR, :DELETE, :DIGEST, :MOVE_FOLDER, :NOTIFY_SENDER, :INTEGRATION], source: :actionType)
    field(:message_id, :string, source: :messageId)
    field(:thread_id, :string, source: :threadId)
    field(:scheduled_for, InboxZero.PrismaDateTime, source: :scheduledFor)
    field(:status, Ecto.Enum, values: [:PENDING, :EXECUTING, :COMPLETED, :FAILED, :CANCELLED], default: :PENDING)
    field(:scheduling_status, Ecto.Enum, values: [:PENDING, :SCHEDULED, :FAILED], default: :PENDING, source: :schedulingStatus)
    field(:label, :string)
    field(:label_id, :string, source: :labelId)
    field(:subject, :string)
    field(:content, :string)
    field(:to, :string)
    field(:cc, :string)
    field(:bcc, :string)
    field(:url, :string)
    field(:folder_name, :string, source: :folderName)
    field(:folder_id, :string, source: :folderId)
    field(:scheduled_id, :string, source: :scheduledId)
    field(:static_attachments, :map, source: :staticAttachments)
    field(:selected_attachments, :map, source: :selectedAttachments)
    field(:executed_at, InboxZero.PrismaDateTime, source: :executedAt)
    field(:executed_rule_id, :string, source: :executedRuleId)
    field(:executed_action_id, :string, source: :executedActionId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:executed_rule, InboxZero.ExecutedRule, foreign_key: :executed_rule_id, define_field: false, type: :string)
    belongs_to(:executed_action, InboxZero.ExecutedAction, foreign_key: :executed_action_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end