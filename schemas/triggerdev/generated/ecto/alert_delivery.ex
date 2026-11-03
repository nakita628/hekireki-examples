defmodule Trigger.AlertDelivery do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          alert_event_id: String.t(),
          event_at: DateTime.t(),
          notifier_id: String.t(),
          notifier: map(),
          status: String.t(),
          claim_id: String.t() | nil,
          claimed_at: DateTime.t() | nil,
          delivered_at: DateTime.t() | nil,
          failure_reason: String.t() | nil,
          provider_metadata: map() | nil,
          attempts: integer(),
          first_attempt_at: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "AlertDelivery" do
    field(:id, :string, primary_key: true)
    field(:alert_event_id, :string, source: :alertEventId)
    field(:event_at, Trigger.PrismaDateTime, primary_key: true, source: :eventAt)
    field(:notifier_id, :string, source: :notifierId)
    field(:notifier, :map)
    field(:status, :string, default: "pending")
    field(:claim_id, :string, source: :claimId)
    field(:claimed_at, Trigger.PrismaDateTime, source: :claimedAt)
    field(:delivered_at, Trigger.PrismaDateTime, source: :deliveredAt)
    field(:failure_reason, :string, source: :failureReason)
    field(:provider_metadata, :map, source: :providerMetadata)
    field(:attempts, :integer, default: 0)
    field(:first_attempt_at, Trigger.PrismaDateTime, source: :firstAttemptAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end