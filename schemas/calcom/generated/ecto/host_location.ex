defmodule Calcom.HostLocation do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          user_id: integer(),
          event_type_id: integer(),
          type: String.t(),
          link: String.t() | nil,
          address: String.t() | nil,
          phone_number: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          credential: Calcom.Credential.t() | nil
        }

  schema "HostLocation" do
    field(:user_id, :integer, source: :userId)
    field(:event_type_id, :integer, source: :eventTypeId)
    field(:type, :string)
    field(:link, :string)
    field(:address, :string)
    field(:phone_number, :string, source: :phoneNumber)
    field(:credential_id, :id, source: :credentialId)
    belongs_to(:credential, Calcom.Credential, foreign_key: :credential_id, define_field: false, type: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end