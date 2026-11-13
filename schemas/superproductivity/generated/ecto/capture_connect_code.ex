defmodule SuperProductivity.CaptureConnectCode do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          code_hash: String.t(),
          token_version: integer(),
          expires_at: integer(),
          user: SuperProductivity.User.t() | nil
        }

  schema "capture_connect_codes" do
    field(:code_hash, :string)
    field(:token_version, :integer)
    field(:expires_at, :integer)
    belongs_to(:user, SuperProductivity.User, foreign_key: :user_id)
  end
end