defmodule OpenSaas.DailyStats do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          date: DateTime.t(),
          total_views: integer(),
          prev_day_views_change_percent: String.t(),
          user_count: integer(),
          paid_user_count: integer(),
          user_delta: integer(),
          paid_user_delta: integer(),
          total_revenue: float(),
          total_profit: float(),
          sources: [OpenSaas.PageViewSource.t()]
        }

  schema "DailyStats" do
    field(:date, OpenSaas.PrismaDateTime, autogenerate: true)
    field(:total_views, :integer, default: 0, source: :totalViews)
    field(:prev_day_views_change_percent, :string, default: "0", source: :prevDayViewsChangePercent)
    field(:user_count, :integer, default: 0, source: :userCount)
    field(:paid_user_count, :integer, default: 0, source: :paidUserCount)
    field(:user_delta, :integer, default: 0, source: :userDelta)
    field(:paid_user_delta, :integer, default: 0, source: :paidUserDelta)
    field(:total_revenue, :float, default: 0.0, source: :totalRevenue)
    field(:total_profit, :float, default: 0.0, source: :totalProfit)
    has_many(:sources, OpenSaas.PageViewSource, foreign_key: :daily_stats_id)
  end
end