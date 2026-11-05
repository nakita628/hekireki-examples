defmodule Chibisafe.Albums do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Chibisafe.PrismaDateTime, autogenerate: {Chibisafe.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          uuid: String.t(),
          user_id: integer(),
          name: String.t(),
          description: String.t() | nil,
          zipped_at: DateTime.t() | nil,
          edited_at: DateTime.t() | nil,
          nsfw: boolean(),
          sort_order: String.t() | nil,
          inserted_at: DateTime.t(),
          files: [Chibisafe.Files.t()]
        }

  schema "albums" do
    field(:uuid, :string)
    field(:user_id, :integer, source: :userId)
    field(:name, :string)
    field(:description, :string)
    field(:zipped_at, Chibisafe.PrismaDateTime, source: :zippedAt)
    field(:edited_at, Chibisafe.PrismaDateTime, source: :editedAt)
    field(:nsfw, :boolean, default: false)
    field(:sort_order, :string, source: :sortOrder)
    many_to_many(:files, Chibisafe.Files, join_through: "_albumsTofiles", join_keys: [A: :id, B: :id])
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end