defmodule InboxZero.OrganizationRuleAction do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          label: String.t() | nil,
          subject: String.t() | nil,
          content: String.t() | nil,
          to: String.t() | nil,
          cc: String.t() | nil,
          bcc: String.t() | nil,
          url: String.t() | nil,
          folder_name: String.t() | nil,
          delay_in_minutes: integer() | nil,
          static_attachments: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization_rule: InboxZero.OrganizationRule.t() | nil
        }

  schema "OrganizationRuleAction" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:ARCHIVE, :LABEL, :REPLY, :SEND_EMAIL, :FORWARD, :DRAFT_EMAIL, :DRAFT_MESSAGING_CHANNEL, :NOTIFY_MESSAGING_CHANNEL, :MARK_SPAM, :CALL_WEBHOOK, :MARK_READ, :STAR, :DELETE, :DIGEST, :MOVE_FOLDER, :NOTIFY_SENDER, :INTEGRATION])
    field(:label, :string)
    field(:subject, :string)
    field(:content, :string)
    field(:to, :string)
    field(:cc, :string)
    field(:bcc, :string)
    field(:url, :string)
    field(:folder_name, :string, source: :folderName)
    field(:delay_in_minutes, :integer, source: :delayInMinutes)
    field(:static_attachments, :map, source: :staticAttachments)
    field(:organization_rule_id, :string, source: :organizationRuleId)
    belongs_to(:organization_rule, InboxZero.OrganizationRule, foreign_key: :organization_rule_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end