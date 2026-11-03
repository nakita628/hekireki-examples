defmodule Calcom.MonthlyProration do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          month_key: String.t(),
          period_start: DateTime.t(),
          period_end: DateTime.t(),
          seats_at_start: integer(),
          seats_added: integer(),
          seats_removed: integer(),
          net_seat_increase: integer(),
          seats_at_end: integer(),
          subscription_id: String.t(),
          subscription_item_id: String.t(),
          customer_id: String.t(),
          subscription_start: DateTime.t(),
          subscription_end: DateTime.t(),
          remaining_days: integer(),
          price_per_seat: integer(),
          prorated_amount: integer(),
          invoice_item_id: String.t() | nil,
          invoice_id: String.t() | nil,
          invoice_url: String.t() | nil,
          status: atom(),
          charged_at: DateTime.t() | nil,
          failed_at: DateTime.t() | nil,
          failure_reason: String.t() | nil,
          retry_count: integer(),
          metadata: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Calcom.Team.t() | nil,
          team_billing: Calcom.TeamBilling.t() | nil,
          organization_billing: Calcom.OrganizationBilling.t() | nil,
          seat_changes: [Calcom.SeatChangeLog.t()]
        }

  schema "MonthlyProration" do
    field(:month_key, :string, source: :monthKey)
    field(:period_start, Calcom.PrismaDateTime, source: :periodStart)
    field(:period_end, Calcom.PrismaDateTime, source: :periodEnd)
    field(:seats_at_start, :integer, source: :seatsAtStart)
    field(:seats_added, :integer, source: :seatsAdded)
    field(:seats_removed, :integer, source: :seatsRemoved)
    field(:net_seat_increase, :integer, source: :netSeatIncrease)
    field(:seats_at_end, :integer, source: :seatsAtEnd)
    field(:subscription_id, :string, source: :subscriptionId)
    field(:subscription_item_id, :string, source: :subscriptionItemId)
    field(:customer_id, :string, source: :customerId)
    field(:subscription_start, Calcom.PrismaDateTime, source: :subscriptionStart)
    field(:subscription_end, Calcom.PrismaDateTime, source: :subscriptionEnd)
    field(:remaining_days, :integer, source: :remainingDays)
    field(:price_per_seat, :integer, source: :pricePerSeat)
    field(:prorated_amount, :integer, source: :proratedAmount)
    field(:invoice_item_id, :string, source: :invoiceItemId)
    field(:invoice_id, :string, source: :invoiceId)
    field(:invoice_url, :string, source: :invoiceUrl)
    field(:status, Ecto.Enum, values: [:PENDING, :INVOICE_CREATED, :CHARGED, :FAILED, :CANCELLED], default: :PENDING)
    field(:charged_at, Calcom.PrismaDateTime, source: :chargedAt)
    field(:failed_at, Calcom.PrismaDateTime, source: :failedAt)
    field(:failure_reason, :string, source: :failureReason)
    field(:retry_count, :integer, default: 0, source: :retryCount)
    field(:metadata, :map)
    field(:team_id, :id, source: :teamId)
    field(:team_billing_id, :binary_id, source: :teamBillingId)
    field(:organization_billing_id, :binary_id, source: :organizationBillingId)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false, type: :id)
    belongs_to(:team_billing, Calcom.TeamBilling, foreign_key: :team_billing_id, define_field: false)
    belongs_to(:organization_billing, Calcom.OrganizationBilling, foreign_key: :organization_billing_id, define_field: false)
    has_many(:seat_changes, Calcom.SeatChangeLog, foreign_key: :processed_in_proration_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end