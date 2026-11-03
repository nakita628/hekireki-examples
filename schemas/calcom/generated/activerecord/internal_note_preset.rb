class InternalNotePreset < ApplicationRecord
  self.table_name = "InternalNotePreset"

  alias_attribute :created_at, :createdAt

  validates :name, presence: true, uniqueness: { scope: :teamId }

  belongs_to :team, foreign_key: "teamId", inverse_of: :internal_note_presets
  has_many :booking_internal_note, class_name: "BookingInternalNote", foreign_key: "notePresetId",
                                   inverse_of: :note_preset, dependent: :destroy
end
