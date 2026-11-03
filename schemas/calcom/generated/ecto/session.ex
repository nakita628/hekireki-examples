defmodule Calcom.Session do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          session_token: String.t(),
          expires: DateTime.t(),
          user: Calcom.User.t() | nil
        }

  schema "Session" do
    field(:id, :string, primary_key: true)
    field(:session_token, :string, source: :sessionToken)
    field(:expires, Calcom.PrismaDateTime)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
  end
end