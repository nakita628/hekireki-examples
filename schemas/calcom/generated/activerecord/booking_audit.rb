class BookingAudit < ApplicationRecord
  self.table_name = "BookingAudit"
  self.inheritance_column = nil

  attribute :id, default: -> { SecureRandom.uuid_v7 }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { record_created: "record_created", record_updated: "record_updated", record_deleted: "record_deleted" },
       validate: true
  enum :action, { created: "created", cancelled: "cancelled", accepted: "accepted", rejected: "rejected", pending: "pending", awaiting_host: "awaiting_host", rescheduled: "rescheduled", attendee_added: "attendee_added", attendee_removed: "attendee_removed", reassignment: "reassignment", location_changed: "location_changed", no_show_updated: "no_show_updated", reschedule_requested: "reschedule_requested", seat_booked: "seat_booked", seat_rescheduled: "seat_rescheduled" },
       validate: true
  enum :source, { api_v1: "api_v1", api_v2: "api_v2", webapp: "webapp", webhook: "webhook", system: "system", magic_link: "magic_link", unknown: "unknown" },
       prefix: true, validate: true

  validates :bookingUid, presence: true
  validates :timestamp, presence: true
  validates :operationId, presence: true

  belongs_to :actor, class_name: "AuditActor", foreign_key: "actorId", inverse_of: :booking_audits
end
