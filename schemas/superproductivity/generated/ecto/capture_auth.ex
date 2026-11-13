defmodule SuperProductivity.CaptureAuth do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          client_id: String.t(),
          client_name: String.t(),
          redirect_uri: String.t(),
          code_challenge: String.t(),
          resource: String.t(),
          state: String.t(),
          scope: String.t(),
          code_hash: String.t() | nil,
          token_version: integer() | nil,
          expires_at: integer(),
          user: SuperProductivity.User.t() | nil
        }

  schema "capture_auth" do
    field(:id, :string, primary_key: true)
    field(:client_id, :string)
    field(:client_name, :string)
    field(:redirect_uri, :string)
    field(:code_challenge, :string)
    field(:resource, :string)
    field(:state, :string)
    field(:scope, :string)
    field(:code_hash, :string)
    field(:token_version, :integer)
    field(:expires_at, :integer)
    belongs_to(:user, SuperProductivity.User, foreign_key: :user_id)
  end
end