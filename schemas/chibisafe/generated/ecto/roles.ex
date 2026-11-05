defmodule Chibisafe.Roles do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          users: [Chibisafe.Users.t()]
        }

  schema "roles" do
    field(:name, :string)
    many_to_many(:users, Chibisafe.Users, join_through: "_rolesTousers", join_keys: [A: :id, B: :id])
  end
end