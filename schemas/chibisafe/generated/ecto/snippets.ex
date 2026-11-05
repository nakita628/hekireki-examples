defmodule Chibisafe.Snippets do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Chibisafe.PrismaDateTime, autogenerate: {Chibisafe.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          uuid: String.t(),
          user_id: integer() | nil,
          identifier: String.t(),
          name: String.t(),
          description: String.t() | nil,
          content: String.t(),
          language: String.t(),
          parent_uuid: String.t() | nil,
          edited_at: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "snippets" do
    field(:uuid, :string)
    field(:user_id, :integer, source: :userId)
    field(:identifier, :string)
    field(:name, :string)
    field(:description, :string)
    field(:content, :string)
    field(:language, :string)
    field(:parent_uuid, :string, source: :parentUuid)
    field(:edited_at, Chibisafe.PrismaDateTime, source: :editedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end