defmodule Formbricks.CreativeDailyPageStat do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          creative_id: String.t(),
          date: Date.t(),
          page: String.t(),
          impressions: integer(),
          clicks: integer(),
          creative: Formbricks.Creative.t() | nil
        }

  schema "CreativeDailyPageStat" do
    field(:date, Formbricks.PrismaDate, primary_key: true)
    field(:page, :string, primary_key: true)
    field(:impressions, :integer, default: 0)
    field(:clicks, :integer, default: 0)
    field(:creative_id, :string, primary_key: true, source: :creativeId)
    belongs_to(:creative, Formbricks.Creative, foreign_key: :creative_id, define_field: false, type: :string)
  end
end