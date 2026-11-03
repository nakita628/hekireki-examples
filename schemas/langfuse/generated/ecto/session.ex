defmodule Langfuse.Session do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          session_token: String.t(),
          expires: DateTime.t(),
          user: Langfuse.User.t() | nil
        }

  schema "Session" do
    field(:id, :string, primary_key: true)
    field(:session_token, :string)
    field(:expires, Langfuse.PrismaDateTime)
    belongs_to(:user, Langfuse.User, foreign_key: :user_id, type: :string)
  end
end