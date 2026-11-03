defmodule Documenso.Passkey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          updated_at: DateTime.t(),
          last_used_at: DateTime.t() | nil,
          credential_id: binary(),
          credential_public_key: binary(),
          counter: integer(),
          credential_device_type: String.t(),
          credential_backed_up: boolean(),
          transports: [String.t()],
          inserted_at: DateTime.t(),
          user: Documenso.User.t() | nil
        }

  schema "Passkey" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:updated_at, Documenso.PrismaDateTime, autogenerate: true, source: :updatedAt)
    field(:last_used_at, Documenso.PrismaDateTime, source: :lastUsedAt)
    field(:credential_id, :binary, source: :credentialId)
    field(:credential_public_key, :binary, source: :credentialPublicKey)
    field(:counter, :integer)
    field(:credential_device_type, :string, source: :credentialDeviceType)
    field(:credential_backed_up, :boolean, source: :credentialBackedUp)
    field(:transports, {:array, :string})
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Documenso.User, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end