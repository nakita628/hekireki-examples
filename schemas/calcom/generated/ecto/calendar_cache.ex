defmodule Calcom.CalendarCache do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          credential_id: term(),
          id: String.t() | nil,
          key: String.t(),
          value: map(),
          expires_at: DateTime.t(),
          user_id: integer() | nil,
          updated_at: DateTime.t(),
          credential: Calcom.Credential.t() | nil
        }

  schema "CalendarCache" do
    field(:id, :string, autogenerate: {Ecto.UUID, :generate, []})
    field(:key, :string, primary_key: true)
    field(:value, :map)
    field(:expires_at, Calcom.PrismaDateTime, source: :expiresAt)
    field(:user_id, :integer, source: :userId)
    field(:credential_id, :id, primary_key: true, source: :credentialId)
    belongs_to(:credential, Calcom.Credential, foreign_key: :credential_id, define_field: false)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end