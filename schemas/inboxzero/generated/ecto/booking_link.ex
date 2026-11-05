defmodule InboxZero.BookingLink do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          slug: String.t(),
          title: String.t(),
          description: String.t() | nil,
          is_active: boolean(),
          duration_minutes: integer(),
          slot_interval_minutes: integer(),
          location_type: atom(),
          location_value: String.t() | nil,
          minimum_notice_minutes: integer(),
          max_days_ahead: integer(),
          availability_schedule_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          destination_calendar: InboxZero.Calendar.t() | nil,
          bookings: [InboxZero.Booking.t()]
        }

  schema "BookingLink" do
    field(:id, :string, primary_key: true)
    field(:slug, :string)
    field(:title, :string)
    field(:description, :string)
    field(:is_active, :boolean, default: true, source: :isActive)
    field(:duration_minutes, :integer, source: :durationMinutes)
    field(:slot_interval_minutes, :integer, source: :slotIntervalMinutes)
    field(:location_type, Ecto.Enum, values: [:IN_PERSON, :GOOGLE_MEET, :MICROSOFT_TEAMS, :PHONE, :CUSTOM], default: :CUSTOM, source: :locationType)
    field(:location_value, :string, source: :locationValue)
    field(:minimum_notice_minutes, :integer, default: 120, source: :minimumNoticeMinutes)
    field(:max_days_ahead, :integer, default: 90, source: :maxDaysAhead)
    field(:availability_schedule_id, :string, source: :availabilityScheduleId)
    field(:email_account_id, :string, source: :emailAccountId)
    field(:destination_calendar_id, :string, source: :destinationCalendarId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    belongs_to(:destination_calendar, InboxZero.Calendar, foreign_key: :destination_calendar_id, define_field: false, type: :string)
    has_many(:bookings, InboxZero.Booking, foreign_key: :booking_link_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end