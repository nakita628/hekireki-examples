defmodule Calcom.BookingAudit do
  use Ecto.Schema

  @primary_key {:id, Ecto.UUID, autogenerate: [version: 7]}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          booking_uid: String.t(),
          type: atom(),
          action: atom(),
          timestamp: DateTime.t(),
          source: atom(),
          operation_id: String.t(),
          data: map() | nil,
          context: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          actor: Calcom.AuditActor.t() | nil
        }

  schema "BookingAudit" do
    field(:booking_uid, :string, source: :bookingUid)
    field(:type, Ecto.Enum, values: [RECORD_CREATED: "record_created", RECORD_UPDATED: "record_updated", RECORD_DELETED: "record_deleted"])
    field(:action, Ecto.Enum, values: [CREATED: "created", CANCELLED: "cancelled", ACCEPTED: "accepted", REJECTED: "rejected", PENDING: "pending", AWAITING_HOST: "awaiting_host", RESCHEDULED: "rescheduled", ATTENDEE_ADDED: "attendee_added", ATTENDEE_REMOVED: "attendee_removed", REASSIGNMENT: "reassignment", LOCATION_CHANGED: "location_changed", NO_SHOW_UPDATED: "no_show_updated", RESCHEDULE_REQUESTED: "reschedule_requested", SEAT_BOOKED: "seat_booked", SEAT_RESCHEDULED: "seat_rescheduled"])
    field(:timestamp, Calcom.PrismaDateTime)
    field(:source, Ecto.Enum, values: [API_V1: "api_v1", API_V2: "api_v2", WEBAPP: "webapp", WEBHOOK: "webhook", SYSTEM: "system", MAGIC_LINK: "magic_link", UNKNOWN: "unknown"])
    field(:operation_id, :string, source: :operationId)
    field(:data, :map)
    field(:context, :map)
    field(:actor_id, :binary_id, source: :actorId)
    belongs_to(:actor, Calcom.AuditActor, foreign_key: :actor_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end