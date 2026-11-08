defmodule WorkoutCool.PlanProviderMapping do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: WorkoutCool.PrismaDateTime, autogenerate: {WorkoutCool.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          provider: atom(),
          external_id: String.t(),
          region: String.t() | nil,
          metadata: map() | nil,
          is_active: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          plan: WorkoutCool.SubscriptionPlan.t() | nil
        }

  schema "plan_provider_mappings" do
    field(:id, :string, primary_key: true)
    field(:provider, Ecto.Enum, values: [:STRIPE, :PAYPAL, :LEMONSQUEEZY, :PADDLE, :APPLE_PAY, :GOOGLE_PAY, :REVENUECAT, :NONE, :OTHER])
    field(:external_id, :string, source: :externalId)
    field(:region, :string)
    field(:metadata, :map)
    field(:is_active, :boolean, default: true, source: :isActive)
    field(:plan_id, :string, source: :planId)
    belongs_to(:plan, WorkoutCool.SubscriptionPlan, foreign_key: :plan_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end