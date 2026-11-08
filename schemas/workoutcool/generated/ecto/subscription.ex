defmodule WorkoutCool.Subscription do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: WorkoutCool.PrismaDateTime, autogenerate: {WorkoutCool.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          revenue_cat_user_id: String.t() | nil,
          status: atom(),
          started_at: DateTime.t(),
          current_period_end: DateTime.t() | nil,
          cancelled_at: DateTime.t() | nil,
          platform: atom() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: WorkoutCool.User.t() | nil,
          plan: WorkoutCool.SubscriptionPlan.t() | nil
        }

  schema "subscriptions" do
    field(:id, :string, primary_key: true)
    field(:revenue_cat_user_id, :string, source: :revenueCatUserId)
    field(:status, Ecto.Enum, values: [:ACTIVE, :TRIAL, :CANCELLED, :EXPIRED, :PAUSED])
    field(:started_at, WorkoutCool.PrismaDateTime, source: :startedAt)
    field(:current_period_end, WorkoutCool.PrismaDateTime, source: :currentPeriodEnd)
    field(:cancelled_at, WorkoutCool.PrismaDateTime, source: :cancelledAt)
    field(:platform, Ecto.Enum, values: [:WEB, :IOS, :ANDROID])
    field(:user_id, :string, source: :userId)
    field(:plan_id, :string, source: :planId)
    belongs_to(:user, WorkoutCool.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:plan, WorkoutCool.SubscriptionPlan, foreign_key: :plan_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end