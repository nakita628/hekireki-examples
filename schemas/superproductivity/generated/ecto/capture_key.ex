defmodule SuperProductivity.CaptureKey do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          public_key: String.t(),
          fingerprint: String.t(),
          created_at: integer(),
          disabled_at: integer() | nil,
          user: SuperProductivity.User.t() | nil
        }

  schema "capture_keys" do
    field(:public_key, :string)
    field(:fingerprint, :string)
    field(:created_at, :integer)
    field(:disabled_at, :integer)
    belongs_to(:user, SuperProductivity.User, foreign_key: :user_id)
  end
end