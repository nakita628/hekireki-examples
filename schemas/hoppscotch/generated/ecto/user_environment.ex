defmodule Hoppscotch.UserEnvironment do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          variables: map(),
          is_global: boolean(),
          user: Hoppscotch.User.t() | nil
        }

  schema "UserEnvironment" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:variables, :map)
    field(:is_global, :boolean, source: :isGlobal)
    field(:user_uid, :string, source: :userUid)
    belongs_to(:user, Hoppscotch.User, foreign_key: :user_uid, define_field: false, type: :string, references: :uid)
  end
end