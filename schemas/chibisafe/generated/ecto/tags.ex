defmodule Chibisafe.Tags do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Chibisafe.PrismaDateTime, autogenerate: {Chibisafe.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          uuid: String.t(),
          user_id: integer(),
          name: String.t(),
          edited_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          files: [Chibisafe.Files.t()]
        }

  schema "tags" do
    field(:uuid, :string)
    field(:user_id, :integer, source: :userId)
    field(:name, :string)
    field(:edited_at, Chibisafe.PrismaDateTime, source: :editedAt)
    many_to_many(:files, Chibisafe.Files, join_through: "_filesTotags", join_keys: [B: :id, A: :id])
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end