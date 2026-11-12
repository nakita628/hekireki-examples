class NoteReference < ApplicationRecord
  self.table_name = "noteReference"

  alias_attribute :created_at, :createdAt

  validates :fromNoteId, uniqueness: { scope: :toNoteId }

  belongs_to :from_note, class_name: "Notes", foreign_key: "fromNoteId", inverse_of: :references
  belongs_to :to_note, class_name: "Notes", foreign_key: "toNoteId", inverse_of: :referenced_by
end
