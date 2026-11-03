defmodule Calcom.SelectedSlots do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          event_type_id: integer(),
          user_id: integer(),
          slot_utc_start_date: DateTime.t(),
          slot_utc_end_date: DateTime.t(),
          uid: String.t(),
          release_at: DateTime.t(),
          is_seat: boolean()
        }

  schema "SelectedSlots" do
    field(:event_type_id, :integer, source: :eventTypeId)
    field(:user_id, :integer, source: :userId)
    field(:slot_utc_start_date, Calcom.PrismaDateTime, source: :slotUtcStartDate)
    field(:slot_utc_end_date, Calcom.PrismaDateTime, source: :slotUtcEndDate)
    field(:uid, :string)
    field(:release_at, Calcom.PrismaDateTime, source: :releaseAt)
    field(:is_seat, :boolean, default: false, source: :isSeat)
  end
end