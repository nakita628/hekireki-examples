defmodule Calcom.PlatformBilling do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          customer_id: String.t(),
          subscription_id: String.t() | nil,
          price_id: String.t() | nil,
          plan: String.t(),
          billing_cycle_start: integer() | nil,
          billing_cycle_end: integer() | nil,
          overdue: boolean() | nil,
          manager_billing: Calcom.PlatformBilling.t() | nil,
          team: Calcom.Team.t() | nil,
          managed_billings: [Calcom.PlatformBilling.t()]
        }

  schema "PlatformBilling" do
    field(:customer_id, :string, source: :customerId)
    field(:subscription_id, :string, source: :subscriptionId)
    field(:price_id, :string, source: :priceId)
    field(:plan, :string, default: "none")
    field(:billing_cycle_start, :integer, source: :billingCycleStart)
    field(:billing_cycle_end, :integer, source: :billingCycleEnd)
    field(:overdue, :boolean, default: false)
    field(:manager_billing_id, :id, source: :managerBillingId)
    belongs_to(:manager_billing, Calcom.PlatformBilling, foreign_key: :manager_billing_id, define_field: false)
    belongs_to(:team, Calcom.Team, foreign_key: :id)
    has_many(:managed_billings, Calcom.PlatformBilling, foreign_key: :manager_billing_id)
  end
end