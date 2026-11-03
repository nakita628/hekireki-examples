defmodule Postiz.ItemUser do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          key: String.t(),
          user: Postiz.User.t() | nil
        }

  schema "ItemUser" do
    field(:key, :string)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:user, Postiz.User, foreign_key: :user_id, define_field: false)
  end
end