class Comment < ApplicationRecord
  self.table_name = "comment"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :table_id, presence: true
  validates :record_id, presence: true
  validates :created_by, presence: true
end
