defmodule Calcom.DestinationCalendar do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          integration: String.t(),
          external_id: String.t(),
          primary_email: String.t() | nil,
          custom_calendar_reminder: integer() | nil,
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          user: Calcom.User.t() | nil,
          event_type: Calcom.EventType.t() | nil,
          credential: Calcom.Credential.t() | nil,
          delegation_credential: Calcom.DelegationCredential.t() | nil,
          booking: [Calcom.Booking.t()]
        }

  schema "DestinationCalendar" do
    field(:integration, :string)
    field(:external_id, :string, source: :externalId)
    field(:primary_email, :string, source: :primaryEmail)
    field(:custom_calendar_reminder, :integer, source: :customCalendarReminder)
    field(:user_id, :id, source: :userId)
    field(:event_type_id, :id, source: :eventTypeId)
    field(:credential_id, :id, source: :credentialId)
    field(:delegation_credential_id, :binary_id, source: :delegationCredentialId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:event_type, Calcom.EventType, foreign_key: :event_type_id, define_field: false)
    belongs_to(:credential, Calcom.Credential, foreign_key: :credential_id, define_field: false)
    belongs_to(:delegation_credential, Calcom.DelegationCredential, foreign_key: :delegation_credential_id, define_field: false, type: :binary_id)
    has_many(:booking, Calcom.Booking, foreign_key: :destination_calendar_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end