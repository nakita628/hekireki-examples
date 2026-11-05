defmodule InboxZero.Booking do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          guest_name: String.t(),
          guest_email: String.t(),
          guest_note: String.t() | nil,
          start_time: DateTime.t(),
          end_time: DateTime.t(),
          status: atom(),
          provider: String.t() | nil,
          provider_connection_id: String.t() | nil,
          provider_calendar_id: String.t() | nil,
          provider_event_id: String.t() | nil,
          video_conference_link: String.t() | nil,
          cancel_token_hash: String.t(),
          cancellation_reason: String.t() | nil,
          idempotency_token: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          booking_link: InboxZero.BookingLink.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "Booking" do
    field(:id, :string, primary_key: true)
    field(:guest_name, :string, source: :guestName)
    field(:guest_email, :string, source: :guestEmail)
    field(:guest_note, :string, source: :guestNote)
    field(:start_time, InboxZero.PrismaDateTime, source: :startTime)
    field(:end_time, InboxZero.PrismaDateTime, source: :endTime)
    field(:status, Ecto.Enum, values: [:PENDING_PROVIDER_EVENT, :CONFIRMED, :CANCELED, :FAILED], default: :PENDING_PROVIDER_EVENT)
    field(:provider, :string)
    field(:provider_connection_id, :string, source: :providerConnectionId)
    field(:provider_calendar_id, :string, source: :providerCalendarId)
    field(:provider_event_id, :string, source: :providerEventId)
    field(:video_conference_link, :string, source: :videoConferenceLink)
    field(:cancel_token_hash, :string, source: :cancelTokenHash)
    field(:cancellation_reason, :string, source: :cancellationReason)
    field(:idempotency_token, :string, source: :idempotencyToken)
    field(:booking_link_id, :string, source: :bookingLinkId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:booking_link, InboxZero.BookingLink, foreign_key: :booking_link_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end