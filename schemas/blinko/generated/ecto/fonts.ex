defmodule Blinko.Fonts do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          display_name: String.t(),
          url: String.t() | nil,
          file_data: binary() | nil,
          is_local: boolean(),
          is_system: boolean(),
          weights: map(),
          category: String.t(),
          sort_order: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "fonts" do
    field(:name, :string)
    field(:display_name, :string, source: :displayName)
    field(:url, :string)
    field(:file_data, :binary, source: :fileData)
    field(:is_local, :boolean, default: false, source: :isLocal)
    field(:is_system, :boolean, default: false, source: :isSystem)
    field(:weights, :map)
    field(:category, :string, default: "sans-serif")
    field(:sort_order, :integer, default: 0, source: :sortOrder)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end