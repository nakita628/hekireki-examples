defmodule Postiz.MastraFavorites do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          user_id: String.t(),
          entity_type: String.t(),
          entity_id: String.t(),
          created_at: DateTime.t(),
          created_at_z: DateTime.t() | nil
        }

  schema "mastra_favorites" do
    field(:user_id, :string, primary_key: true, source: :userId)
    field(:entity_type, :string, primary_key: true, source: :entityType)
    field(:entity_id, :string, primary_key: true, source: :entityId)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
  end
end