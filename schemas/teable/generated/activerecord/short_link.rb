class ShortLink < ApplicationRecord
  self.table_name = "short_link"
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }

  validates :code, presence: true, length: { maximum: 32 }, uniqueness: true
  validates :type, presence: true, length: { maximum: 32 }, uniqueness: { scope: :resource_id }
  validates :resource_id, presence: true
  validates :created_by, presence: true
end
