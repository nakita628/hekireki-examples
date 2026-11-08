defmodule TypeHero.BetaTokens do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          token: String.t(),
          user: TypeHero.User.t() | nil
        }

  schema "BetaTokens" do
    field(:token, :string)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:user, TypeHero.User, foreign_key: :user_id, define_field: false, type: :binary_id)
  end
end