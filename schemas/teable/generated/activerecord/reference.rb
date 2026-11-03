class Reference < ApplicationRecord
  self.table_name = "reference"

  attribute :id, default: -> { Cuid.generate }

  validates :from_field_id, presence: true
  validates :to_field_id, presence: true, uniqueness: { scope: :from_field_id }
end
