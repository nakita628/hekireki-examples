defmodule Calcom.CalendarCacheEvent do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          external_id: String.t(),
          external_etag: String.t(),
          i_cal_uid: String.t() | nil,
          i_cal_sequence: integer(),
          summary: String.t() | nil,
          description: String.t() | nil,
          location: String.t() | nil,
          start: DateTime.t(),
          end: DateTime.t(),
          is_all_day: boolean(),
          time_zone: String.t() | nil,
          status: atom(),
          recurring_event_id: String.t() | nil,
          original_start_time: DateTime.t() | nil,
          external_created_at: DateTime.t() | nil,
          external_updated_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          selected_calendar: Calcom.SelectedCalendar.t() | nil
        }

  schema "CalendarCacheEvent" do
    field(:external_id, :string, source: :externalId)
    field(:external_etag, :string, source: :externalEtag)
    field(:i_cal_uid, :string, source: :iCalUID)
    field(:i_cal_sequence, :integer, default: 0, source: :iCalSequence)
    field(:summary, :string)
    field(:description, :string)
    field(:location, :string)
    field(:start, Calcom.PrismaDateTime)
    field(:end, Calcom.PrismaDateTime)
    field(:is_all_day, :boolean, default: false, source: :isAllDay)
    field(:time_zone, :string, source: :timeZone)
    field(:status, Ecto.Enum, values: [:confirmed, :tentative, :cancelled], default: :confirmed)
    field(:recurring_event_id, :string, source: :recurringEventId)
    field(:original_start_time, Calcom.PrismaDateTime, source: :originalStartTime)
    field(:external_created_at, Calcom.PrismaDateTime, source: :externalCreatedAt)
    field(:external_updated_at, Calcom.PrismaDateTime, source: :externalUpdatedAt)
    field(:selected_calendar_id, :binary_id, source: :selectedCalendarId)
    belongs_to(:selected_calendar, Calcom.SelectedCalendar, foreign_key: :selected_calendar_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end