defmodule NextAuth.Authenticator do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          user_id: String.t(),
          credential_id: String.t(),
          provider_account_id: String.t(),
          credential_public_key: String.t(),
          counter: integer(),
          credential_device_type: String.t(),
          credential_backed_up: boolean(),
          transports: String.t() | nil,
          user: NextAuth.User.t() | nil
        }

  schema "Authenticator" do
    field(:credential_id, :string, primary_key: true, source: :credentialID)
    field(:provider_account_id, :string, source: :providerAccountId)
    field(:credential_public_key, :string, source: :credentialPublicKey)
    field(:counter, :integer)
    field(:credential_device_type, :string, source: :credentialDeviceType)
    field(:credential_backed_up, :boolean, source: :credentialBackedUp)
    field(:transports, :string)
    field(:user_id, :string, primary_key: true, source: :userId)
    belongs_to(:user, NextAuth.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end