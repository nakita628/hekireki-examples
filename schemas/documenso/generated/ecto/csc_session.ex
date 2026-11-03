defmodule Documenso.CscSession do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          envelope_id: String.t(),
          signing_time: DateTime.t(),
          items_json: map(),
          encrypted_sad: binary() | nil,
          sad_expires_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          recipient: Documenso.Recipient.t() | nil
        }

  schema "CscSession" do
    field(:id, :string, primary_key: true)
    field(:envelope_id, :string, source: :envelopeId)
    field(:signing_time, Documenso.PrismaDateTime, source: :signingTime)
    field(:items_json, :map, source: :itemsJson)
    field(:encrypted_sad, :binary, source: :encryptedSad)
    field(:sad_expires_at, Documenso.PrismaDateTime, source: :sadExpiresAt)
    field(:recipient_id, :id, source: :recipientId)
    belongs_to(:recipient, Documenso.Recipient, foreign_key: :recipient_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end