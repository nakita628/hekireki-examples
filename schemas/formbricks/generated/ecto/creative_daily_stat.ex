defmodule Formbricks.CreativeDailyStat do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          creative_id: String.t(),
          date: Date.t(),
          device_type: atom(),
          impressions: integer(),
          clicks: integer(),
          creative: Formbricks.Creative.t() | nil
        }

  schema "CreativeDailyStat" do
    field(:date, Formbricks.PrismaDate, primary_key: true)
    field(:device_type, Ecto.Enum, values: [:phone, :tablet, :desktop], source: :deviceType)
    field(:impressions, :integer, default: 0)
    field(:clicks, :integer, default: 0)
    field(:creative_id, :string, primary_key: true, source: :creativeId)
    belongs_to(:creative, Formbricks.Creative, foreign_key: :creative_id, define_field: false, type: :string)
  end
end