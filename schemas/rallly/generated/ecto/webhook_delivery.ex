defmodule Rallly.WebhookDelivery do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          activity_id: String.t(),
          event_type: String.t(),
          payload: map(),
          status: atom(),
          attempts: integer(),
          next_attempt_at: DateTime.t(),
          last_response_status: integer() | nil,
          last_error: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          webhook: Rallly.SpaceWebhook.t() | nil
        }

  schema "webhook_deliveries" do
    field(:id, :string, primary_key: true)
    field(:activity_id, :string)
    field(:event_type, :string)
    field(:payload, :map)
    field(:status, Ecto.Enum, values: [:pending, :in_flight, :succeeded, :failed, :exhausted], default: :pending)
    field(:attempts, :integer, default: 0)
    field(:next_attempt_at, Rallly.PrismaDateTime, autogenerate: true)
    field(:last_response_status, :integer)
    field(:last_error, :string)
    belongs_to(:webhook, Rallly.SpaceWebhook, foreign_key: :webhook_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end