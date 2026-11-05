defmodule InboxZero.MessagingRoute do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          purpose: atom(),
          target_type: atom(),
          target_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          messaging_channel: InboxZero.MessagingChannel.t() | nil
        }

  schema "MessagingRoute" do
    field(:id, :string, primary_key: true)
    field(:purpose, Ecto.Enum, values: [:RULE_NOTIFICATIONS, :SCHEDULED_CHECK_INS, :MEETING_BRIEFS, :DOCUMENT_FILINGS, :DIGESTS, :FOLLOW_UPS])
    field(:target_type, Ecto.Enum, values: [:CHANNEL, :DIRECT_MESSAGE], source: :targetType)
    field(:target_id, :string, source: :targetId)
    field(:messaging_channel_id, :string, source: :messagingChannelId)
    belongs_to(:messaging_channel, InboxZero.MessagingChannel, foreign_key: :messaging_channel_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end