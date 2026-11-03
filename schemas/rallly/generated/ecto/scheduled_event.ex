defmodule Rallly.ScheduledEvent do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t(),
          description: String.t() | nil,
          location: map() | nil,
          conferencing: map() | nil,
          capacity: integer() | nil,
          status: atom(),
          time_zone: String.t() | nil,
          start: DateTime.t(),
          end: DateTime.t(),
          all_day: boolean(),
          deleted_at: DateTime.t() | nil,
          sequence: integer(),
          uid: String.t(),
          hide_attendees: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Rallly.User.t() | nil,
          space: Rallly.Space.t() | nil,
          event_type: Rallly.EventType.t() | nil,
          sheet_slot: Rallly.SheetSlot.t() | nil,
          rescheduled_dates: [Rallly.RescheduledEventDate.t()],
          invites: [Rallly.ScheduledEventInvite.t()],
          polls: [Rallly.Poll.t()]
        }

  schema "scheduled_events" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:description, :string)
    field(:location, :map)
    field(:conferencing, :map)
    field(:capacity, :integer)
    field(:status, Ecto.Enum, values: [:confirmed, :canceled, :unconfirmed], default: :confirmed)
    field(:time_zone, :string)
    field(:start, Rallly.PrismaDateTime)
    field(:end, Rallly.PrismaDateTime)
    field(:all_day, :boolean, default: false)
    field(:deleted_at, Rallly.PrismaDateTime)
    field(:sequence, :integer, default: 0)
    field(:uid, :string)
    field(:hide_attendees, :boolean, default: false)
    belongs_to(:user, Rallly.User, foreign_key: :user_id, type: :string)
    belongs_to(:space, Rallly.Space, foreign_key: :space_id, type: :binary_id)
    belongs_to(:event_type, Rallly.EventType, foreign_key: :event_type_id, type: :string)
    belongs_to(:sheet_slot, Rallly.SheetSlot, foreign_key: :sheet_slot_id, type: :string)
    has_many(:rescheduled_dates, Rallly.RescheduledEventDate, foreign_key: :scheduled_event_id)
    has_many(:invites, Rallly.ScheduledEventInvite, foreign_key: :scheduled_event_id)
    has_many(:polls, Rallly.Poll, foreign_key: :scheduled_event_id)
    timestamps(inserted_at_source: :created_at)
  end
end