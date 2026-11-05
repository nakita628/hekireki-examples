defmodule InboxZero.ExecutedAction do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          execution_status: atom() | nil,
          execution_started_at: DateTime.t() | nil,
          executed_at: DateTime.t() | nil,
          execution_error: map() | nil,
          sent_message_ids: [String.t()],
          messaging_message_id: String.t() | nil,
          messaging_message_sent_at: DateTime.t() | nil,
          messaging_message_status: atom() | nil,
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
          static_attachments: map() | nil,
          selected_attachments: map() | nil,
          integration_name: String.t() | nil,
          integration_tool_name: String.t() | nil,
          integration_args: map() | nil,
          draft_id: String.t() | nil,
          draft_status: atom() | nil,
          was_draft_sent: boolean() | nil,
          draft_model_provider: String.t() | nil,
          draft_model_name: String.t() | nil,
          draft_pipeline_version: integer() | nil,
          draft_context_metadata: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          executed_rule: InboxZero.ExecutedRule.t() | nil,
          messaging_channel: InboxZero.MessagingChannel.t() | nil,
          draft_send_log: InboxZero.DraftSendLog.t() | nil,
          scheduled_action: InboxZero.ScheduledAction.t() | nil,
          digest_items: [InboxZero.DigestItem.t()]
        }

  schema "ExecutedAction" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:ARCHIVE, :LABEL, :REPLY, :SEND_EMAIL, :FORWARD, :DRAFT_EMAIL, :DRAFT_MESSAGING_CHANNEL, :NOTIFY_MESSAGING_CHANNEL, :MARK_SPAM, :CALL_WEBHOOK, :MARK_READ, :STAR, :DELETE, :DIGEST, :MOVE_FOLDER, :NOTIFY_SENDER, :INTEGRATION])
    field(:execution_status, Ecto.Enum, values: [:SUCCEEDED, :FAILED, :SKIPPED], source: :executionStatus)
    field(:execution_started_at, InboxZero.PrismaDateTime, source: :executionStartedAt)
    field(:executed_at, InboxZero.PrismaDateTime, source: :executedAt)
    field(:execution_error, :map, source: :executionError)
    field(:sent_message_ids, {:array, :string}, source: :sentMessageIds)
    field(:messaging_message_id, :string, source: :messagingMessageId)
    field(:messaging_message_sent_at, InboxZero.PrismaDateTime, source: :messagingMessageSentAt)
    field(:messaging_message_status, Ecto.Enum, values: [:SENT, :DRAFT_SENT, :DRAFT_EDITED, :DISMISSED, :EXPIRED, :FAILED], source: :messagingMessageStatus)
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
    field(:static_attachments, :map, source: :staticAttachments)
    field(:selected_attachments, :map, source: :selectedAttachments)
    field(:integration_name, :string, source: :integrationName)
    field(:integration_tool_name, :string, source: :integrationToolName)
    field(:integration_args, :map, source: :integrationArgs)
    field(:draft_id, :string, source: :draftId)
    field(:draft_status, Ecto.Enum, values: [:PENDING, :LIKELY_SENT, :REPLIED_WITHOUT_DRAFT, :CLEANED_UP_UNUSED, :MISSING_FROM_PROVIDER, :USER_EDITED, :CLEANUP_SKIPPED], source: :draftStatus)
    field(:was_draft_sent, :boolean, source: :wasDraftSent)
    field(:draft_model_provider, :string, source: :draftModelProvider)
    field(:draft_model_name, :string, source: :draftModelName)
    field(:draft_pipeline_version, :integer, source: :draftPipelineVersion)
    field(:draft_context_metadata, :map, source: :draftContextMetadata)
    field(:executed_rule_id, :string, source: :executedRuleId)
    field(:messaging_channel_id, :string, source: :messagingChannelId)
    belongs_to(:executed_rule, InboxZero.ExecutedRule, foreign_key: :executed_rule_id, define_field: false, type: :string)
    belongs_to(:messaging_channel, InboxZero.MessagingChannel, foreign_key: :messaging_channel_id, define_field: false, type: :string)
    has_one(:draft_send_log, InboxZero.DraftSendLog, foreign_key: :executed_action_id, references: :id)
    has_one(:scheduled_action, InboxZero.ScheduledAction, foreign_key: :executed_action_id, references: :id)
    has_many(:digest_items, InboxZero.DigestItem, foreign_key: :action_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end