class BookingInternalNote < ApplicationRecord
  self.table_name = "BookingInternalNote"

  alias_attribute :created_at, :createdAt

  validates :bookingId, uniqueness: { scope: :notePresetId, conditions: -> { where.not(notePresetId: nil) } }

  belongs_to :note_preset, class_name: "InternalNotePreset", foreign_key: "notePresetId", optional: true,
                           inverse_of: :booking_internal_note
  belongs_to :booking, foreign_key: "bookingId", inverse_of: :internal_note
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", inverse_of: :booking_internal_note
end
