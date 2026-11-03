defmodule Calcom.OrganizationBilling do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          subscription_id: String.t(),
          subscription_item_id: String.t(),
          customer_id: String.t(),
          status: String.t(),
          plan_name: String.t(),
          subscription_start: DateTime.t() | nil,
          subscription_trial_end: DateTime.t() | nil,
          subscription_end: DateTime.t() | nil,
          billing_period: atom() | nil,
          billing_mode: atom(),
          price_per_seat: integer() | nil,
          paid_seats: integer() | nil,
          high_water_mark: integer() | nil,
          high_water_mark_period_start: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Calcom.Team.t() | nil,
          monthly_prorations: [Calcom.MonthlyProration.t()],
          seat_change_logs: [Calcom.SeatChangeLog.t()]
        }

  schema "OrganizationBilling" do
    field(:subscription_id, :string, source: :subscriptionId)
    field(:subscription_item_id, :string, source: :subscriptionItemId)
    field(:customer_id, :string, source: :customerId)
    field(:status, :string)
    field(:plan_name, :string, source: :planName)
    field(:subscription_start, Calcom.PrismaDateTime, source: :subscriptionStart)
    field(:subscription_trial_end, Calcom.PrismaDateTime, source: :subscriptionTrialEnd)
    field(:subscription_end, Calcom.PrismaDateTime, source: :subscriptionEnd)
    field(:billing_period, Ecto.Enum, values: [:MONTHLY, :ANNUALLY], source: :billingPeriod)
    field(:billing_mode, Ecto.Enum, values: [:SEATS, :ACTIVE_USERS], default: :SEATS, source: :billingMode)
    field(:price_per_seat, :integer, source: :pricePerSeat)
    field(:paid_seats, :integer, source: :paidSeats)
    field(:high_water_mark, :integer, source: :highWaterMark)
    field(:high_water_mark_period_start, Calcom.PrismaDateTime, source: :highWaterMarkPeriodStart)
    field(:team_id, :id, source: :teamId)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false, type: :id)
    has_many(:monthly_prorations, Calcom.MonthlyProration, foreign_key: :organization_billing_id)
    has_many(:seat_change_logs, Calcom.SeatChangeLog, foreign_key: :organization_billing_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end