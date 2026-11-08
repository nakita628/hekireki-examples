defmodule TypeHero.Role do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          role: atom(),
          user: [TypeHero.User.t()]
        }

  schema "Role" do
    field(:role, Ecto.Enum, values: [:USER, :ADMIN, :MODERATOR, :CREATOR, :SUPPORTER, :CONTRIBUTOR])
    many_to_many(:user, TypeHero.User, join_through: "_RoleToUser", join_keys: [A: :id, B: :id])
  end
end