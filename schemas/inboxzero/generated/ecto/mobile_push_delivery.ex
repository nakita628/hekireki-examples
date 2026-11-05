defmodule InboxZero.MobilePushDelivery do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          deduplication_key: String.t(),
          inserted_at: DateTime.t(),
          mobile_push_token: InboxZero.MobilePushToken.t() | nil
        }

  schema "MobilePushDelivery" do
    field(:id, :string, primary_key: true)
    field(:deduplication_key, :string, source: :deduplicationKey)
    field(:mobile_push_token_id, :string, source: :mobilePushTokenId)
    belongs_to(:mobile_push_token, InboxZero.MobilePushToken, foreign_key: :mobile_push_token_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end