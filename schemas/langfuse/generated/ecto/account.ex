defmodule Langfuse.Account do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          provider: String.t(),
          provider_account_id: String.t(),
          refresh_token: String.t() | nil,
          access_token: String.t() | nil,
          expires_at: integer() | nil,
          expires_in: integer() | nil,
          ext_expires_in: integer() | nil,
          token_type: String.t() | nil,
          scope: String.t() | nil,
          id_token: String.t() | nil,
          session_state: String.t() | nil,
          refresh_token_expires_in: integer() | nil,
          created_at: integer() | nil,
          user: Langfuse.User.t() | nil
        }

  schema "Account" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:provider, :string)
    field(:provider_account_id, :string, source: :providerAccountId)
    field(:refresh_token, :string)
    field(:access_token, :string)
    field(:expires_at, :integer)
    field(:expires_in, :integer)
    field(:ext_expires_in, :integer)
    field(:token_type, :string)
    field(:scope, :string)
    field(:id_token, :string)
    field(:session_state, :string)
    field(:refresh_token_expires_in, :integer)
    field(:created_at, :integer)
    belongs_to(:user, Langfuse.User, foreign_key: :user_id, type: :string)
  end
end