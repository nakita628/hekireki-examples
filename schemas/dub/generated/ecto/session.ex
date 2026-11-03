defmodule Dub.Session do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          session_token: String.t(),
          expires: DateTime.t(),
          user: Dub.User.t() | nil
        }

  schema "Session" do
    field(:id, :string, primary_key: true)
    field(:session_token, :string, source: :sessionToken)
    field(:expires, Dub.PrismaDateTime)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Dub.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end