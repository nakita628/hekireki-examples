defmodule Postiz.Star do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          stars: integer(),
          total_stars: integer(),
          forks: integer(),
          total_forks: integer(),
          login: String.t(),
          date: Date.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "Star" do
    field(:stars, :integer)
    field(:total_stars, :integer, source: :totalStars)
    field(:forks, :integer)
    field(:total_forks, :integer, source: :totalForks)
    field(:login, :string)
    field(:date, Postiz.PrismaDate, autogenerate: true)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end