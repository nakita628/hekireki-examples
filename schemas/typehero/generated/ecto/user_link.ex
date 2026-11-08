defmodule TypeHero.UserLink do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: TypeHero.PrismaDateTime, autogenerate: {TypeHero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          url: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: [TypeHero.User.t()]
        }

  schema "UserLink" do
    field(:url, :string)
    many_to_many(:user, TypeHero.User, join_through: "_UserToUserLink", join_keys: [B: :id, A: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end