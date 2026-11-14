defmodule LangWatch.Passkey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          public_key: String.t(),
          credential_id: String.t(),
          counter: integer(),
          device_type: String.t(),
          backed_up: boolean(),
          transports: String.t() | nil,
          aaguid: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: LangWatch.User.t() | nil
        }

  schema "Passkey" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:public_key, :string, source: :publicKey)
    field(:credential_id, :string, source: :credentialID)
    field(:counter, :integer, default: 0)
    field(:device_type, :string, source: :deviceType)
    field(:backed_up, :boolean, default: false, source: :backedUp)
    field(:transports, :string)
    field(:aaguid, :string)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end