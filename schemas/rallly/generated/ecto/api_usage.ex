defmodule Rallly.ApiUsage do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          space_id: Ecto.UUID.t(),
          resource: atom(),
          period: Date.t(),
          count: integer(),
          near_limit_warned_at: DateTime.t() | nil,
          at_limit_warned_at: DateTime.t() | nil,
          updated_at: DateTime.t(),
          space: Rallly.Space.t() | nil
        }

  schema "api_usage" do
    field(:resource, Ecto.Enum, values: [:poll])
    field(:period, Rallly.PrismaDate, primary_key: true)
    field(:count, :integer, default: 0)
    field(:near_limit_warned_at, Rallly.PrismaDateTime)
    field(:at_limit_warned_at, Rallly.PrismaDateTime)
    field(:space_id, :binary_id, primary_key: true)
    belongs_to(:space, Rallly.Space, foreign_key: :space_id, define_field: false, type: :binary_id)
    timestamps(inserted_at: false)
  end
end