defmodule Calcom.BookingReference do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          type: String.t(),
          uid: String.t(),
          meeting_id: String.t() | nil,
          third_party_recurring_event_id: String.t() | nil,
          meeting_password: String.t() | nil,
          meeting_url: String.t() | nil,
          external_calendar_id: String.t() | nil,
          deleted: boolean() | nil,
          booking: Calcom.Booking.t() | nil,
          credential: Calcom.Credential.t() | nil,
          delegation_credential: Calcom.DelegationCredential.t() | nil
        }

  schema "BookingReference" do
    field(:type, :string)
    field(:uid, :string)
    field(:meeting_id, :string, source: :meetingId)
    field(:third_party_recurring_event_id, :string, source: :thirdPartyRecurringEventId)
    field(:meeting_password, :string, source: :meetingPassword)
    field(:meeting_url, :string, source: :meetingUrl)
    field(:external_calendar_id, :string, source: :externalCalendarId)
    field(:deleted, :boolean)
    field(:booking_id, :id, source: :bookingId)
    field(:credential_id, :id, source: :credentialId)
    field(:delegation_credential_id, :binary_id, source: :delegationCredentialId)
    belongs_to(:booking, Calcom.Booking, foreign_key: :booking_id, define_field: false)
    belongs_to(:credential, Calcom.Credential, foreign_key: :credential_id, define_field: false)
    belongs_to(:delegation_credential, Calcom.DelegationCredential, foreign_key: :delegation_credential_id, define_field: false, type: :binary_id)
  end
end