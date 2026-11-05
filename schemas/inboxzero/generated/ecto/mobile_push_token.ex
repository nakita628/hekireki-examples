defmodule InboxZero.MobilePushToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          token: String.t(),
          platform: atom(),
          token_type: atom(),
          app_version: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: InboxZero.User.t() | nil,
          mobile_push_deliveries: [InboxZero.MobilePushDelivery.t()]
        }

  schema "MobilePushToken" do
    field(:id, :string, primary_key: true)
    field(:token, :string)
    field(:platform, Ecto.Enum, values: [ANDROID: "android", IOS: "ios"])
    field(:token_type, Ecto.Enum, values: [EXPO: "expo", APNS: "apns", FCM: "fcm"], default: :EXPO, source: :tokenType)
    field(:app_version, :string, source: :appVersion)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, InboxZero.User, foreign_key: :user_id, define_field: false, type: :string)
    has_many(:mobile_push_deliveries, InboxZero.MobilePushDelivery, foreign_key: :mobile_push_token_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end