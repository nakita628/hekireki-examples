defmodule OpenSaas.PageViewSource do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          name: String.t(),
          date: DateTime.t(),
          visitors: integer(),
          daily_stats: OpenSaas.DailyStats.t() | nil
        }

  schema "PageViewSource" do
    field(:name, :string, primary_key: true)
    field(:date, OpenSaas.PrismaDateTime, primary_key: true, autogenerate: true)
    field(:visitors, :integer)
    field(:daily_stats_id, :id, source: :dailyStatsId)
    belongs_to(:daily_stats, OpenSaas.DailyStats, foreign_key: :daily_stats_id, define_field: false)
  end
end