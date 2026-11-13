defmodule SuperProductivity.CaptureGrant do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          kind: String.t(),
          client_name: String.t(),
          secret_hash: String.t(),
          created_at: integer(),
          expires_at: integer(),
          last_used_at: integer() | nil,
          revoked_at: integer() | nil,
          client_id: String.t() | nil,
          resource: String.t() | nil,
          refresh_hash: String.t() | nil,
          prev_refresh_hash: String.t() | nil,
          rotated_at: integer() | nil,
          refresh_expires_at: integer() | nil,
          user: SuperProductivity.User.t() | nil
        }

  schema "capture_grants" do
    field(:id, :string, primary_key: true)
    field(:kind, :string)
    field(:client_name, :string)
    field(:secret_hash, :string)
    field(:created_at, :integer)
    field(:expires_at, :integer)
    field(:last_used_at, :integer)
    field(:revoked_at, :integer)
    field(:client_id, :string)
    field(:resource, :string)
    field(:refresh_hash, :string)
    field(:prev_refresh_hash, :string)
    field(:rotated_at, :integer)
    field(:refresh_expires_at, :integer)
    belongs_to(:user, SuperProductivity.User, foreign_key: :user_id)
  end
end