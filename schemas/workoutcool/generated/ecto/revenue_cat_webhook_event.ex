defmodule WorkoutCool.RevenueCatWebhookEvent do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: WorkoutCool.PrismaDateTime, autogenerate: {WorkoutCool.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          event_type: String.t(),
          event_timestamp: DateTime.t(),
          app_user_id: String.t(),
          environment: String.t(),
          product_id: String.t() | nil,
          transaction_id: String.t() | nil,
          original_transaction_id: String.t() | nil,
          entitlement_ids: String.t() | nil,
          processed: boolean(),
          processing_error: String.t() | nil,
          retry_count: integer(),
          raw_event_data: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "revenuecat_webhook_events" do
    field(:id, :string, primary_key: true)
    field(:event_type, :string, source: :eventType)
    field(:event_timestamp, WorkoutCool.PrismaDateTime, source: :eventTimestamp)
    field(:app_user_id, :string, source: :appUserId)
    field(:environment, :string)
    field(:product_id, :string, source: :productId)
    field(:transaction_id, :string, source: :transactionId)
    field(:original_transaction_id, :string, source: :originalTransactionId)
    field(:entitlement_ids, :string, source: :entitlementIds)
    field(:processed, :boolean, default: false)
    field(:processing_error, :string, source: :processingError)
    field(:retry_count, :integer, default: 0, source: :retryCount)
    field(:raw_event_data, :map, source: :rawEventData)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end