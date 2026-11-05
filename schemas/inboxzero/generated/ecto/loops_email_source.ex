defmodule InboxZero.LoopsEmailSource do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email_message_id: String.t() | nil,
          loop_id: String.t() | nil,
          loop_name: String.t() | nil,
          campaign_id: String.t() | nil,
          campaign_name: String.t() | nil,
          source_type: String.t() | nil,
          event_time: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LoopsEmailSource" do
    field(:id, :string, primary_key: true)
    field(:email_message_id, :string, source: :emailMessageId)
    field(:loop_id, :string, source: :loopId)
    field(:loop_name, :string, source: :loopName)
    field(:campaign_id, :string, source: :campaignId)
    field(:campaign_name, :string, source: :campaignName)
    field(:source_type, :string, source: :sourceType)
    field(:event_time, InboxZero.PrismaDateTime, source: :eventTime)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end