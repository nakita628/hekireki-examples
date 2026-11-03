defmodule Calcom.WebhookScheduledTriggers do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          job_name: String.t() | nil,
          subscriber_url: String.t(),
          payload: String.t(),
          start_after: DateTime.t(),
          retry_count: integer(),
          app_id: String.t() | nil,
          inserted_at: DateTime.t() | nil,
          webhook: Calcom.Webhook.t() | nil,
          booking: Calcom.Booking.t() | nil
        }

  schema "WebhookScheduledTriggers" do
    field(:job_name, :string, source: :jobName)
    field(:subscriber_url, :string, source: :subscriberUrl)
    field(:payload, :string)
    field(:start_after, Calcom.PrismaDateTime, source: :startAfter)
    field(:retry_count, :integer, default: 0, source: :retryCount)
    field(:app_id, :string, source: :appId)
    field(:webhook_id, :string, source: :webhookId)
    field(:booking_id, :id, source: :bookingId)
    belongs_to(:webhook, Calcom.Webhook, foreign_key: :webhook_id, define_field: false, type: :string)
    belongs_to(:booking, Calcom.Booking, foreign_key: :booking_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end