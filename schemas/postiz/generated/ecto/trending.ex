defmodule Postiz.Trending do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          trending_list: String.t(),
          language: String.t() | nil,
          hash: String.t(),
          date: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "Trending" do
    field(:trending_list, :string, source: :trendingList)
    field(:language, :string)
    field(:hash, :string)
    field(:date, Postiz.PrismaDateTime)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end