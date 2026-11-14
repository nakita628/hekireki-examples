defmodule LangWatch.WebPushSubscription do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          user_id: String.t(),
          endpoint: String.t(),
          p256dh: String.t(),
          auth: String.t(),
          user_agent: String.t() | nil,
          last_success_at: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "WebPushSubscription" do
    field(:id, :string, primary_key: true)
    field(:user_id, :string, source: :userId)
    field(:endpoint, :string)
    field(:p256dh, :string)
    field(:auth, :string)
    field(:user_agent, :string, source: :userAgent)
    field(:last_success_at, LangWatch.PrismaDateTime, source: :lastSuccessAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end