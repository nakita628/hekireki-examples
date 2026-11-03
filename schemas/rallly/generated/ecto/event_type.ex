defmodule Rallly.EventType do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          duration: integer(),
          capacity: integer() | nil,
          description: String.t() | nil,
          location: map() | nil,
          deleted: boolean(),
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          space: Rallly.Space.t() | nil,
          host: Rallly.User.t() | nil,
          slots: [Rallly.SheetSlot.t()],
          scheduled_events: [Rallly.ScheduledEvent.t()]
        }

  schema "event_types" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:duration, :integer, source: :duration_minutes)
    field(:capacity, :integer)
    field(:description, :string)
    field(:location, :map)
    field(:deleted, :boolean, default: false)
    field(:deleted_at, Rallly.PrismaDateTime)
    belongs_to(:space, Rallly.Space, foreign_key: :space_id, type: :binary_id)
    belongs_to(:host, Rallly.User, foreign_key: :host_id, type: :string)
    has_many(:slots, Rallly.SheetSlot, foreign_key: :event_type_id)
    has_many(:scheduled_events, Rallly.ScheduledEvent, foreign_key: :event_type_id)
    timestamps(inserted_at_source: :created_at)
  end
end