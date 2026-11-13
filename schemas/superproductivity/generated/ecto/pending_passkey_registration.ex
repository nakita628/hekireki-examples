defmodule SuperProductivity.PendingPasskeyRegistration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: SuperProductivity.PrismaDateTime, autogenerate: {SuperProductivity.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          verification_token: String.t(),
          verification_token_expires_at: integer(),
          credential_id: binary(),
          public_key: binary(),
          counter: integer(),
          transports: String.t() | nil,
          inserted_at: DateTime.t(),
          user: SuperProductivity.User.t() | nil
        }

  schema "pending_passkey_registrations" do
    field(:id, :string, primary_key: true)
    field(:verification_token, :string)
    field(:verification_token_expires_at, :integer)
    field(:credential_id, :binary)
    field(:public_key, :binary)
    field(:counter, :integer, default: 0)
    field(:transports, :string)
    belongs_to(:user, SuperProductivity.User, foreign_key: :user_id)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end