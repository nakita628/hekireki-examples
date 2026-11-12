defmodule Blinko.Cache do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          key: String.t(),
          value: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "cache" do
    field(:key, :string)
    field(:value, :map)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end