defmodule Taxonomy.Session do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          session_token: String.t(),
          expires: DateTime.t(),
          user: Taxonomy.User.t() | nil
        }

  schema "sessions" do
    field(:id, :string, primary_key: true)
    field(:session_token, :string, source: :sessionToken)
    field(:expires, Taxonomy.PrismaDateTime)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Taxonomy.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end