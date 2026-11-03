defmodule Calcom.UserPassword do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          user_id: term(),
          hash: String.t(),
          user: Calcom.User.t() | nil
        }

  schema "UserPassword" do
    field(:hash, :string)
    field(:user_id, :id, primary_key: true, source: :userId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
  end
end