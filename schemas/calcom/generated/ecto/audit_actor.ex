defmodule Calcom.AuditActor do
  use Ecto.Schema

  @primary_key {:id, Ecto.UUID, autogenerate: [version: 7]}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          type: atom(),
          user_uuid: String.t() | nil,
          attendee_id: integer() | nil,
          credential_id: integer() | nil,
          email: String.t() | nil,
          phone: String.t() | nil,
          name: String.t() | nil,
          inserted_at: DateTime.t(),
          booking_audits: [Calcom.BookingAudit.t()]
        }

  schema "AuditActor" do
    field(:type, Ecto.Enum, values: [USER: "user", GUEST: "guest", ATTENDEE: "attendee", SYSTEM: "system", APP: "app"])
    field(:user_uuid, :string, source: :userUuid)
    field(:attendee_id, :integer, source: :attendeeId)
    field(:credential_id, :integer, source: :credentialId)
    field(:email, :string)
    field(:phone, :string)
    field(:name, :string)
    has_many(:booking_audits, Calcom.BookingAudit, foreign_key: :actor_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end