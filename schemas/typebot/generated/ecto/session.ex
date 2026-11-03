defmodule Typebot.Session do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          session_token: String.t(),
          expires: DateTime.t(),
          user: Typebot.User.t() | nil
        }

  schema "Session" do
    field(:id, :string, primary_key: true)
    field(:session_token, :string, source: :sessionToken)
    field(:expires, Typebot.PrismaDateTime)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Typebot.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end