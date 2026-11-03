defmodule Rallly.SpaceWebhook do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          url: String.t(),
          secret: String.t(),
          events: [String.t()],
          enabled: boolean(),
          version: String.t(),
          cursor: DateTime.t(),
          last_delivered_at: DateTime.t() | nil,
          last_failed_at: DateTime.t() | nil,
          consecutive_failures: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          space: Rallly.Space.t() | nil,
          deliveries: [Rallly.WebhookDelivery.t()]
        }

  schema "space_webhooks" do
    field(:id, :string, primary_key: true)
    field(:url, :string)
    field(:secret, :string)
    field(:events, {:array, :string})
    field(:enabled, :boolean, default: true)
    field(:version, :string)
    field(:cursor, Rallly.PrismaDateTime, autogenerate: true)
    field(:last_delivered_at, Rallly.PrismaDateTime)
    field(:last_failed_at, Rallly.PrismaDateTime)
    field(:consecutive_failures, :integer, default: 0)
    belongs_to(:space, Rallly.Space, foreign_key: :space_id, type: :binary_id)
    has_many(:deliveries, Rallly.WebhookDelivery, foreign_key: :webhook_id)
    timestamps(inserted_at_source: :created_at)
  end
end