defmodule LangWatch.BillingMeterCheckpoint do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          billing_month: String.t(),
          meter: String.t(),
          last_reported_total: integer(),
          pending_reported_total: integer() | nil,
          consecutive_failures: integer(),
          last_counted_event_id: String.t() | nil,
          updated_at: DateTime.t()
        }

  schema "BillingMeterCheckpoint" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:billing_month, :string, source: :billingMonth)
    field(:meter, :string, default: "langwatch_billable_events")
    field(:last_reported_total, :integer, default: 0, source: :lastReportedTotal)
    field(:pending_reported_total, :integer, source: :pendingReportedTotal)
    field(:consecutive_failures, :integer, default: 0, source: :consecutiveFailures)
    field(:last_counted_event_id, :string, source: :lastCountedEventId)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end