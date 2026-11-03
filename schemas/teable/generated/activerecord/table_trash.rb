class TableTrash < ApplicationRecord
  self.table_name = "table_trash"

  attribute :id, default: -> { Cuid.generate }

  validates :table_id, presence: true
  validates :resource_type, presence: true
  validates :snapshot, presence: true
  validates :created_by, presence: true
end
