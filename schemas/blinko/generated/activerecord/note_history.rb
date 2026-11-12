class NoteHistory < ApplicationRecord
  self.table_name = "noteHistory"

  alias_attribute :created_at, :createdAt

  validates :content, presence: true
  validates :version, presence: true

  belongs_to :note, class_name: "Notes", foreign_key: "noteId", inverse_of: :histories
end
