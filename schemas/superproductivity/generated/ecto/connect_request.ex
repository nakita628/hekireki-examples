defmodule SuperProductivity.ConnectRequest do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          poll_secret_hash: String.t(),
          match_code: String.t(),
          device_name: String.t(),
          platform: String.t(),
          status: String.t(),
          token_version: integer() | nil,
          expires_at: integer(),
          user: SuperProductivity.User.t() | nil
        }

  schema "connect_requests" do
    field(:id, :string, primary_key: true)
    field(:poll_secret_hash, :string)
    field(:match_code, :string)
    field(:device_name, :string)
    field(:platform, :string)
    field(:status, :string, default: "pending")
    field(:token_version, :integer)
    field(:expires_at, :integer)
    belongs_to(:user, SuperProductivity.User, foreign_key: :user_id)
  end
end