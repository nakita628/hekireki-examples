class PinResource < ApplicationRecord
  self.table_name = "pin_resource"
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }

  validates :type, presence: true
  validates :resource_id, presence: true
  validates :created_by, presence: true, uniqueness: { scope: :resource_id }
  validates :order, presence: true
end
