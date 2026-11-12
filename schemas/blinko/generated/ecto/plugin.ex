defmodule Blinko.Plugin do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          metadata: map(),
          path: String.t(),
          is_use: boolean(),
          is_dev: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "plugin" do
    field(:metadata, :map)
    field(:path, :string)
    field(:is_use, :boolean, default: true, source: :isUse)
    field(:is_dev, :boolean, default: false, source: :isDev)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end