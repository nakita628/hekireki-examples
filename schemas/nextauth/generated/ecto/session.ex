defmodule NextAuth.Session do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          session_token: String.t(),
          expires: DateTime.t(),
          user: NextAuth.User.t() | nil
        }

  schema "Session" do
    field(:session_token, :string, primary_key: true, source: :sessionToken)
    field(:expires, NextAuth.PrismaDateTime)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, NextAuth.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end