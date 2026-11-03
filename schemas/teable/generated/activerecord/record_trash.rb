class RecordTrash < ApplicationRecord
  self.table_name = "record_trash"

  attribute :id, default: -> { Cuid.generate }
  attribute :reason, default: "deleted"

  validates :table_id, presence: true
  validates :record_id, presence: true
  validates :snapshot, presence: true
  validates :created_by, presence: true
end
