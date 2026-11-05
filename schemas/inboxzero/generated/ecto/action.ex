defmodule InboxZero.Action do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          rule_id: String.t(),
          messaging_channel_id: String.t() | nil,
          messaging_channel_email_account_id: String.t() | nil,
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
          delay_in_minutes: integer() | nil,
          static_attachments: map() | nil,
          integration_name: String.t() | nil,
          integration_tool_name: String.t() | nil,
          integration_args: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "Action" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:ARCHIVE, :LABEL, :REPLY, :SEND_EMAIL, :FORWARD, :DRAFT_EMAIL, :DRAFT_MESSAGING_CHANNEL, :NOTIFY_MESSAGING_CHANNEL, :MARK_SPAM, :CALL_WEBHOOK, :MARK_READ, :STAR, :DELETE, :DIGEST, :MOVE_FOLDER, :NOTIFY_SENDER, :INTEGRATION])
    field(:rule_id, :string, source: :ruleId)
    field(:messaging_channel_id, :string, source: :messagingChannelId)
    field(:messaging_channel_email_account_id, :string, source: :messagingChannelEmailAccountId)
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
    field(:delay_in_minutes, :integer, source: :delayInMinutes)
    field(:static_attachments, :map, source: :staticAttachments)
    field(:integration_name, :string, source: :integrationName)
    field(:integration_tool_name, :string, source: :integrationToolName)
    field(:integration_args, :map, source: :integrationArgs)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end