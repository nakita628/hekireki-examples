defmodule WorkoutCool.SubscriptionPlan do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: WorkoutCool.PrismaDateTime, autogenerate: {WorkoutCool.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          price_monthly: Decimal.t() | nil,
          price_yearly: Decimal.t() | nil,
          currency: String.t(),
          interval: String.t(),
          is_active: boolean(),
          available_regions: [String.t()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          subscriptions: [WorkoutCool.Subscription.t()],
          provider_mappings: [WorkoutCool.PlanProviderMapping.t()]
        }

  schema "subscription_plans" do
    field(:id, :string, primary_key: true)
    field(:price_monthly, :decimal, source: :priceMonthly)
    field(:price_yearly, :decimal, source: :priceYearly)
    field(:currency, :string, default: "EUR")
    field(:interval, :string, default: "month")
    field(:is_active, :boolean, default: true, source: :isActive)
    field(:available_regions, {:array, :string}, source: :availableRegions)
    has_many(:subscriptions, WorkoutCool.Subscription, foreign_key: :plan_id, references: :id)
    has_many(:provider_mappings, WorkoutCool.PlanProviderMapping, foreign_key: :plan_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end