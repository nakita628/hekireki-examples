class Trash < ApplicationRecord
  self.table_name = "trash"

  attribute :id, default: -> { Cuid.generate }

  validates :resource_type, presence: true, uniqueness: { scope: :resource_id }
  validates :resource_id, presence: true
  validates :deleted_by, presence: true
end
