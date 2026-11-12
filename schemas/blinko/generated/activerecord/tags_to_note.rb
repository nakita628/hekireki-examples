class TagsToNote < ApplicationRecord
  self.table_name = "tagsToNote"
  self.primary_key = %w[noteId tagId]

  attribute :noteId, default: 0
  attribute :tagId, default: 0

  belongs_to :note, class_name: "Notes", foreign_key: "noteId", inverse_of: :tags
  belongs_to :tag, foreign_key: "tagId", inverse_of: :tags_to_note
end
